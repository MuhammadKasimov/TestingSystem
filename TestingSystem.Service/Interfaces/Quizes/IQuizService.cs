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
    public interface IQuizService 
    {
        ValueTask<QuizForViewDTO> CreateAsync(QuizForCreationDTO QuizForCreationDTO);

        ValueTask<QuizForViewDTO> UpdateAsync(int id, QuizForCreationDTO QuizForCreationDTO);

        ValueTask<bool> DeleteAsync(int id);

        ValueTask<IEnumerable<QuizForViewDTO>> GetAllAsync(
            PaginationParams @params,
            Expression<Func<Quiz, bool>> expression = null);

        ValueTask<QuizForViewDTO> GetAsync(Expression<Func<Quiz, bool>> expression);
    }
}
