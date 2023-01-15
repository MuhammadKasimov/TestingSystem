using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data.IRepositories;
using TestingSystem.Domain.Configurations;
using TestingSystem.Domain.Entities.Courses;
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

        public QuestionService(IGenericRepository<Quiz> quizRepository, IGenericRepository<Question> questionRepository)
        {
            this.quizRepository = quizRepository;
            this.questionRepository = questionRepository;
        }

        public async ValueTask<QuestionForViewDTO> CreateAsync(QuestionForCreationDTO questionForCreationDTO)
        {
            var quiz = await quizRepository.GetAsync(q => q.Id == questionForCreationDTO.QuizId);

            if (quiz is null)
            {
                throw new TestingSystemException(404, "Quiz not found");
            }

            var question = await questionRepository.CreateAsync(questionForCreationDTO.Adapt<Question>());
            await questionRepository.SaveChangesAsync();
            return question.Adapt<QuestionForViewDTO>();
        }

        public async ValueTask<bool> DeleteAsync(int id)
        {
            var isDeleted = await questionRepository.DeleteAsync(q => q.Id == id);

            if (!isDeleted)
                throw new TestingSystemException(404,"Question not found");
            await questionRepository.SaveChangesAsync();
            return true;
        }

        public async ValueTask<IEnumerable<QuestionForViewDTO>> GetAllAsync(PaginationParams @params, Expression<Func<Question, bool>> expression = null)
        {
            var questions =  questionRepository.GetAll(expression: expression, isTracking: false, includes: new string[] { "Answers", "Attachments" });
            return (await questions.ToPagedList(@params).ToListAsync()).Adapt<List<QuestionForViewDTO>>();
        }

        public async ValueTask<QuestionForViewDTO> GetAsync(Expression<Func<Question, bool>> expression)
        {
            var question = await questionRepository.GetAsync(expression, new string[] { "Answers", "Attachments" });
            if (question is null)
                throw new TestingSystemException(404,"Question Not Found");

            return question.Adapt<QuestionForViewDTO>();
        }

        public async ValueTask<QuestionForViewDTO> UpdateAsync(int id, QuestionForCreationDTO questionForCreationDTO)
        {
            var existQuestion = await questionRepository.GetAsync(q => q.Id == id);
            if (existQuestion is null)
                throw new TestingSystemException(404, "Question not found");

            var quiz = await quizRepository.GetAsync(q => q.Id == questionForCreationDTO.QuizId);
            if (quiz is null)
                throw new TestingSystemException(404, "Quiz not found");

            existQuestion.UpdatedAt= DateTime.UtcNow;
            existQuestion = questionRepository.Update(questionForCreationDTO.Adapt(existQuestion));
            await questionRepository.SaveChangesAsync();

            return existQuestion.Adapt<QuestionForViewDTO>();
        }
    }
}
