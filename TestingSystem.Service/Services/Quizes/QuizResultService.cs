using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestingSystem.Data.IRepositories;
using TestingSystem.Domain.Configurations;
using TestingSystem.Domain.Entities.Quizes;
using TestingSystem.Domain.Entities.Users;
using TestingSystem.Service.DTOs.Quizes;
using TestingSystem.Service.Exceptions;
using TestingSystem.Service.Extensions;
using TestingSystem.Service.Helpers;
using TestingSystem.Service.Interfaces.Quizes;

namespace TestingSystem.Service.Services.Quizes
{
    public class QuizResultService : IQuizResultService
    {
        private readonly IGenericRepository<Quiz> quizRepository;
        private readonly IGenericRepository<QuizResult> quizResultRepository;
        private readonly IGenericRepository<User> userRepository;
        private readonly IGenericRepository<Answer> answerRepository;
        private readonly IGenericRepository<Question> questionRepository;
        private readonly IMapper mapper;

        public QuizResultService(IGenericRepository<Quiz> quizRepository, IGenericRepository<QuizResult> quizResultRepository, IGenericRepository<User> userRepository, IMapper mapper, IGenericRepository<Answer> answerRepository, IGenericRepository<Question> questionRepository)
        {
            this.quizRepository = quizRepository;
            this.quizResultRepository = quizResultRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.answerRepository = answerRepository;
            this.questionRepository = questionRepository;
        }

        public async ValueTask<QuizResultForViewDTO> StartSolvingQuizAsync(int quizId)
        {
            var quiz = await quizRepository.GetAsync(q => q.Id == quizId);

            if (quiz is null)
                throw new TestingSystemException(404, "Quiz not found");
           
            var user = await userRepository.GetAsync(u => u.Id == HttpContextHelper.UserId);
            if (user is null)
                throw new TestingSystemException(404, "User not found");

            var quizResult = new QuizResult()
            {
                QuizId = quizId,
                UserId = user.Id,
            };

            var alreadySolved = await quizResultRepository.GetAsync(
                                    q => q.UserId == HttpContextHelper.UserId &&
                                    q.QuizId == quizResult.QuizId);

            if (alreadySolved != null)
                throw new TestingSystemException(400, "Already solved this test");

            quizResult = await quizResultRepository.CreateAsync(quizResult);
            await quizResultRepository.SaveChangesAsync();
            return mapper.Map<QuizResultForViewDTO>(quizRepository);

        }
        public async ValueTask<QuizResultForViewDTO> CreateAsync(QuizResultForCreationDTO quizResultForCreationDTO)
        {
            var quizResult = await quizResultRepository.GetAsync(q => q.Id == quizResultForCreationDTO.StartedQuizResultId);

            if (quizResult is null)
                throw new TestingSystemException(404,"quizResult not found");
            quizResult = mapper.Map(quizResultForCreationDTO, quizResult);

            var quiz = await quizRepository.GetAsync(q => q.Id == quizResult.Id);

            if (quiz.CreatedAt + TimeSpan.FromMinutes(quiz.TimeToSolveInMinutes) <= DateTime.UtcNow)
            {
                throw new TestingSystemException(403,"No access to solve this test");
            }
            foreach (var i in quizResultForCreationDTO.SolvedQuestions)
            {
                var question = await questionRepository.GetAsync(q => q.Id == i.QuestionId);
                var answer = await answerRepository.GetAsync(a => a.Id == i.AnswerId);

                if (answer is null)
                    throw new TestingSystemException(404,"answer not found");

                if (question is null)
                    throw new TestingSystemException(404,"quesion not found");


                if (question.QuizId != quizResult.QuizId)
                    throw new TestingSystemException(404, "quiz does not have such question");

                if (answer.QuestionId != question.Id)
                    throw new TestingSystemException(404,"question does not have such answer");

                if (answer.IsCorrect)
                    quizResult.CorrectAnswers++;
            }

            quizResult = await quizResultRepository.CreateAsync(quizResult);
            await quizResultRepository.SaveChangesAsync();
            return mapper.Map<QuizResultForViewDTO>(quizResult);
        }

        public async ValueTask<string> GetAllInExcel(int quizId)
        {
            var quizResults = mapper.Map<List<QuizResultForViewDTO>>( await quizResultRepository.
                GetAll(qr => qr.QuizId == quizId, isTracking: false, includes: new string[] { "User", "Quiz" }).ToListAsync());

            var quizResultsForExcel = new List<QuizResultForViewInExcelDTO>();


            foreach (var i in quizResults)
            {
                quizResultsForExcel.Add(new QuizResultForViewInExcelDTO()
                { 
                    FirstName = i.User.FirstName,
                    LastName = i.User.LastName,
                    CorrectAnswers = i.CorrectAnswers
                });
            }


            string fileName = $"{Guid.NewGuid():N}_{quizResults.FirstOrDefault().Quiz.Title}.xlsx";
            string rootPath = EnvironmentHelper.ExcelRootPath;

            string filePath = Path.Combine(EnvironmentHelper.ExcelRootPath, fileName);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var workSheet = package.Workbook.Worksheets.Add(quizResults.FirstOrDefault().Quiz.Title);

                for (int i = 0; i < typeof(QuizResultForViewInExcelDTO).GetProperties().Length; i++)
                {
                    workSheet.Cells[1, i + 1].Value = typeof(QuizResultForViewInExcelDTO).GetProperties()[i].Name;
                }

                for (int i = 0; i < quizResultsForExcel.Count(); i++)
                {
                    for (int j = 0; j < typeof(QuizResultForViewInExcelDTO).GetProperties().Length; j++)
                    {
                        workSheet.Cells[i + 2, j + 1].Value = typeof(QuizResultForViewInExcelDTO).GetProperties()[j].GetValue(quizResultsForExcel[i]);
                    }
                }

                if (!Directory.Exists(EnvironmentHelper.ExcelRootPath))
                    Directory.CreateDirectory(EnvironmentHelper.ExcelRootPath);

                File.WriteAllBytes(filePath, package.GetAsByteArray());
            }
            return Path.Combine(EnvironmentHelper.ExcelPath, fileName);

        }

        public async ValueTask<IEnumerable<QuizResultForViewDTO>> GetAllAsync(PaginationParams @params, Expression<Func<QuizResult, bool>> expression = null)
        {
            var quizResults = quizResultRepository.GetAll(expression: expression, isTracking: false, includes: new string[] { "User", "Quiz" });
            return mapper.Map<List<QuizResultForViewDTO>>(await quizResults.ToPagedList(@params).ToListAsync());
        }

        public async ValueTask<QuizResultForViewDTO> GetAsync(Expression<Func<QuizResult, bool>> expression)
        {
            var quizResult = await quizResultRepository.GetAsync(expression, new string[] { "User", "Quiz" });
            if (quizResult is null)
                throw new TestingSystemException(404, "QuizResult Not Found");

            return mapper.Map<QuizResultForViewDTO>(quizResult);
        }
    }
}
