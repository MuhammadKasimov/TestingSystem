using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestingSystem.Domain.Configurations;
using TestingSystem.Domain.Entities.Quizes;
using TestingSystem.Service.DTOs.Quizes;

namespace TestingSystem.Service.Interfaces.Questiones
{
    public interface IQuestionService
    {
        ValueTask<QuestionForViewDTO> CreateAsync(QuestionForCreationDTO questionForCreationDTO);

        ValueTask<QuestionForViewDTO> UpdateAsync(int id, QuestionForCreationDTO questionForCreationDTO);

        ValueTask<bool> DeleteAsync(int id);

        ValueTask<IEnumerable<QuestionForViewDTO>> GetAllAsync(
            PaginationParams @params,
            Expression<Func<Question, bool>> expression = null);

        ValueTask<QuestionForViewDTO> GetAsync(Expression<Func<Question, bool>> expression);
    }
}
