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
        private readonly IMapper mapper;

        public QuizResultService(IGenericRepository<Quiz> quizRepository, IGenericRepository<QuizResult> quizResultRepository, IGenericRepository<User> userRepository, IMapper mapper)
        {
            this.quizRepository = quizRepository;
            this.quizResultRepository = quizResultRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async ValueTask<QuizResultForViewDTO> CreateAsync(QuizResultForCreationDTO quizResultForCreationDTO)
        {
            var quiz = await quizRepository.GetAsync(q => q.Id == quizResultForCreationDTO.QuizId);

            if (quiz is null)
                throw new TestingSystemException(404, "Quiz not found");

            var user = await userRepository.GetAsync(u => u.Id == HttpContextHelper.UserId);
            if (user is null)
                throw new TestingSystemException(404, "User not found");

            var quizResult = mapper.Map<QuizResult>(quizResultForCreationDTO);
            quizResult.UserId = user.Id;
            quizResult = await quizResultRepository.CreateAsync(quizResult);
            await quizResultRepository.SaveChangesAsync();
            return mapper.Map<QuizResultForViewDTO>(quizResult);
        }

        public async ValueTask<string> GetAllInExcel(int quizId)
        {
            var quizResults = mapper.Map<List<QuizResultForViewDTO>>( await quizResultRepository.
                GetAll(qr => qr.QuizId == quizId, isTracking: false, includes: new string[] { "User", "Quiz" }).ToListAsync());

            string fileName = $"{Guid.NewGuid():N}.xlsx";
            string rootPath = EnvironmentHelper.ExcelRootPath;

            string filePath = Path.Combine(EnvironmentHelper.ExcelRootPath, fileName);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var workSheet = package.Workbook.Worksheets.Add("Quiz results");

                for (int i = 0; i < typeof(QuizResultForViewDTO).GetProperties().Length; i++)
                {
                    workSheet.Cells[1, i + 1].Value = typeof(QuizResultForViewDTO).GetProperties()[i].Name;
                }

                for (int i = 0; i < quizResults.Count(); i++)
                {
                    for (int j = 0; j < typeof(QuizResultForViewDTO).GetProperties().Length; j++)
                    {
                        workSheet.Cells[i + 2, j + 1].Value = typeof(QuizResultForViewDTO).GetProperties()[j].GetValue(quizResults[i]);
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
