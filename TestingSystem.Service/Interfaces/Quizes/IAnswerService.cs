using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Domain.Configurations;
using TestingSystem.Domain.Entities.Quizes;
using TestingSystem.Service.DTOs.Quizes;

namespace TestingSystem.Service.Interfaces.Quizes
{
    public interface IAnswerService
    {
        ValueTask<AnswerForViewDTO> CreateAsync(AnswerForCreationDTO answerForCreationDTO);

        ValueTask<AnswerForViewDTO> UpdateAsync(int id, AnswerForCreationDTO answerForCreationDTO);

        ValueTask<bool> DeleteAsync(int id);

        ValueTask<IEnumerable<AnswerForViewDTO>> GetAllAsync(
            PaginationParams @params,
            Expression<Func<Answer, bool>> expression = null);

        ValueTask<AnswerForViewDTO> GetAsync(Expression<Func<Answer, bool>> expression);

    }
}
