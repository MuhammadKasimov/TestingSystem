using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestingSystem.Domain.Configurations;
using TestingSystem.Domain.Entities.Quizes;
using TestingSystem.Service.DTOs.Quizes;

namespace TestingSystem.Service.Interfaces.Quizes
{
    public interface IQuizResultService
    {

        ValueTask<QuizResultForViewDTO> CreateAsync(QuizResultForCreationDTO QuizResultForCreationDTO);
        ValueTask<QuizResultForViewDTO> StartSolvingQuizAsync(int quizId);
        ValueTask<IEnumerable<QuizResultForViewDTO>> GetAllAsync(
            PaginationParams @params,
            Expression<Func<QuizResult, bool>> expression = null);

        ValueTask<QuizResultForViewDTO> GetAsync(Expression<Func<QuizResult, bool>> expression);

        ValueTask<string> GetAllInExcel(int quizId);
    }
}
