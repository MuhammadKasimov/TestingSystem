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
using TestingSystem.Domain.Entities.Quizes;
using TestingSystem.Service.DTOs.Quizes;
using TestingSystem.Service.Exceptions;
using TestingSystem.Service.Extensions;
using TestingSystem.Service.Interfaces.Quizes;

namespace TestingSystem.Service.Services.Quizes
{
    public class AnswerService : IAnswerService
    {
        private readonly IGenericRepository<Answer> answerRepository;
        private readonly IGenericRepository<Question> questionRepository;

        public AnswerService(IGenericRepository<Answer> answerRepository, IGenericRepository<Question> questionRepository)
        {
            this.answerRepository = answerRepository;
            this.questionRepository = questionRepository;
        }

        public async ValueTask<AnswerForViewDTO> CreateAsync(AnswerForCreationDTO answerForCreationDTO)
        {
            var question = await questionRepository.GetAsync(q => q.Id == answerForCreationDTO.QuestionId);

            if (question is null)
            {
                throw new TestingSystemException(404, "Question not found");
            }

            var answer = await answerRepository.CreateAsync(answerForCreationDTO.Adapt<Answer>());
            await answerRepository.SaveChangesAsync();
            return answer.Adapt<AnswerForViewDTO>();
        }

        public async ValueTask<bool> DeleteAsync(int id)
        {
            var isDeleted = await answerRepository.DeleteAsync(a => a.Id == id);

            if (!isDeleted)
                throw new TestingSystemException(404, "Answer not found");
            await answerRepository.SaveChangesAsync();
            return true;
        }

        public async ValueTask<IEnumerable<AnswerForViewDTO>> GetAllAsync(PaginationParams @params, Expression<Func<Answer, bool>> expression = null)
        {
            var answers = answerRepository.GetAll(expression: expression, isTracking: false, includes: new string[] { "Question" });
            return (await answers.ToPagedList(@params).ToListAsync()).Adapt<List<AnswerForViewDTO>>();
        }

        public async ValueTask<AnswerForViewDTO> GetAsync(Expression<Func<Answer, bool>> expression)
        {
            var answer = await answerRepository.GetAsync(expression, new string[] { "Question" });
            if (answer is null)
                throw new TestingSystemException(404, "Answer Not Found");

            return answer.Adapt<AnswerForViewDTO>();
        }

        public async ValueTask<AnswerForViewDTO> UpdateAsync(int id, AnswerForCreationDTO answerForCreationDTO)
        {
            var existAnswer = await answerRepository.GetAsync(a => a.Id == id);
            if (existAnswer is null)
                throw new TestingSystemException(404, "Answer not found");

            var question = await questionRepository.GetAsync(q => q.Id == answerForCreationDTO.QuestionId);
            if (question is null)
                throw new TestingSystemException(404, "Question not found");

            existAnswer.UpdatedAt = DateTime.UtcNow;
            existAnswer = answerRepository.Update(answerForCreationDTO.Adapt(existAnswer));
            await answerRepository.SaveChangesAsync();

            return existAnswer.Adapt<AnswerForViewDTO>();
        }
    }
}
