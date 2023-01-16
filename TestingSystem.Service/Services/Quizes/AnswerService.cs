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
using TestingSystem.Service.Interfaces.Quizes;

namespace TestingSystem.Service.Services.Quizes
{
    public class AnswerService : IAnswerService
    {
        private readonly IGenericRepository<Answer> answerRepository;
        private readonly IGenericRepository<Question> questionRepository;
        private readonly IMapper mapper;
        public AnswerService(IGenericRepository<Answer> answerRepository, IGenericRepository<Question> questionRepository, IMapper mapper)
        {
            this.answerRepository = answerRepository;
            this.questionRepository = questionRepository;
            this.mapper = mapper;
        }

        public async ValueTask<AnswerForViewDTO> CreateAsync(AnswerForCreationDTO answerForCreationDTO)
        {
            var question = await questionRepository.GetAsync(q => q.Id == answerForCreationDTO.QuestionId);

            if (question is null)
            {
                throw new TestingSystemException(404, "Question not found");
            }

            var answer = await answerRepository.CreateAsync(mapper.Map<Answer>(answerForCreationDTO));
            await answerRepository.SaveChangesAsync();
            return mapper.Map<AnswerForViewDTO>(answer);
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
            var answers = answerRepository.GetAll(expression: expression, isTracking: false);
            return mapper.Map<List<AnswerForViewDTO>>(await answers.ToPagedList(@params).ToListAsync());
        }

        public async ValueTask<AnswerForViewDTO> GetAsync(Expression<Func<Answer, bool>> expression)
        {
            var answer = await answerRepository.GetAsync(expression);
            if (answer is null)
                throw new TestingSystemException(404, "Answer Not Found");

            return mapper.Map<AnswerForViewDTO>(answer);
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
            existAnswer = answerRepository.Update(mapper.Map(answerForCreationDTO, existAnswer));
            await answerRepository.SaveChangesAsync();

            return mapper.Map<AnswerForViewDTO>(existAnswer);
        }
    }
}
