using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestingSystem.Data.IRepositories;
using TestingSystem.Domain.Configurations;
using TestingSystem.Domain.Entities.Quizes;
using TestingSystem.Service.DTOs.Quizes;
using TestingSystem.Service.Exceptions;
using TestingSystem.Service.Extensions;
using TestingSystem.Service.Interfaces.Questiones;

namespace TestingSystem.Service.Services.Quizes
{
    public class QuestionService : IQuestionService
    {
        private readonly IGenericRepository<Quiz> quizRepository;
        private readonly IGenericRepository<Question> questionRepository;
        private readonly IMapper mapper;
        public QuestionService(IGenericRepository<Quiz> quizRepository, IGenericRepository<Question> questionRepository, IMapper mapper)
        {
            this.quizRepository = quizRepository;
            this.questionRepository = questionRepository;
            this.mapper = mapper;
        }

        public async ValueTask<QuestionForViewDTO> CreateAsync(QuestionForCreationDTO questionForCreationDTO)
        {
            var quiz = await quizRepository.GetAsync(q => q.Id == questionForCreationDTO.QuizId);

            if (quiz is null)
            {
                throw new TestingSystemException(404, "Quiz not found");
            }

            quiz.NumberOfQuestions++;
            quizRepository.Update(quiz);
            var question = await questionRepository.CreateAsync(mapper.Map<Question>(questionForCreationDTO));
            await questionRepository.SaveChangesAsync();
            return  mapper.Map<QuestionForViewDTO>(question);
        }

        public async ValueTask<bool> DeleteAsync(int id)
        {
            var quizId = (await questionRepository.GetAsync(q => q.Id == id)).QuizId;
            var isDeleted = await questionRepository.DeleteAsync(q => q.Id == id);

            if (!isDeleted)
                throw new TestingSystemException(404, "Question not found");

            var quiz = await quizRepository.GetAsync(q => q.Id == quizId);
            quiz.NumberOfQuestions--;
            quizRepository.Update(quiz);
            await questionRepository.SaveChangesAsync();
            return true;
        }

        public async ValueTask<IEnumerable<QuestionForViewDTO>> GetAllAsync(PaginationParams @params, Expression<Func<Question, bool>> expression = null)
        {
            var questions = questionRepository.GetAll(expression: expression, isTracking: false, includes: new string[] { "Answers", "Attachments" });
            return mapper.Map<List<QuestionForViewDTO>>(await questions.ToPagedList(@params).ToListAsync());
        }

        public async ValueTask<QuestionForViewDTO> GetAsync(Expression<Func<Question, bool>> expression)
        {
            var question = await questionRepository.GetAsync(expression, new string[] { "Answers", "Attachments" });
            if (question is null)
                throw new TestingSystemException(404, "Question Not Found");

            return mapper.Map<QuestionForViewDTO>(question);
        }

        public async ValueTask<QuestionForViewDTO> UpdateAsync(int id, QuestionForCreationDTO questionForCreationDTO)
        {
            var existQuestion = await questionRepository.GetAsync(q => q.Id == id);
            if (existQuestion is null)
                throw new TestingSystemException(404, "Question not found");

            var quiz = await quizRepository.GetAsync(q => q.Id == questionForCreationDTO.QuizId);
            if (quiz is null)
                throw new TestingSystemException(404, "Quiz not found");

            existQuestion.UpdatedAt = DateTime.UtcNow;
            existQuestion = questionRepository.Update(mapper.Map(questionForCreationDTO, existQuestion));
            await questionRepository.SaveChangesAsync();

            return mapper.Map<QuestionForViewDTO>(existQuestion);
        }
    }
}
