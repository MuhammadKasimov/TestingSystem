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
    public interface IQuizResultService
    {

        ValueTask<QuizResultForViewDTO> CreateAsync(QuizResultForCreationDTO QuizResultForCreationDTO);

        ValueTask<IEnumerable<QuizResultForViewDTO>> GetAllAsync(
            PaginationParams @params,
            Expression<Func<QuizResult, bool>> expression = null);

        ValueTask<QuizResultForViewDTO> GetAsync(Expression<Func<QuizResult, bool>> expression);
    }
}
