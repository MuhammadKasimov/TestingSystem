using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public QuizResultService(IGenericRepository<Quiz> quizRepository, IGenericRepository<QuizResult> quizResultRepository, IGenericRepository<User> userRepository)
        {
            this.quizRepository = quizRepository;
            this.quizResultRepository = quizResultRepository;
            this.userRepository = userRepository;
        }

        public async ValueTask<QuizResultForViewDTO> CreateAsync(QuizResultForCreationDTO quizResultForCreationDTO)
        {
            var quiz = await quizRepository.GetAsync(q => q.Id == quizResultForCreationDTO.QuizId);

            if (quiz is null)
                throw new TestingSystemException(404, "Quiz not found");

            var user = await userRepository.GetAsync(u => u.Id == HttpContextHelper.UserId);
            if (user is null)
                throw new TestingSystemException(404,"User not found");

            var QuizResult = await quizResultRepository.CreateAsync(quizResultForCreationDTO.Adapt<QuizResult>());
            await quizResultRepository.SaveChangesAsync();
            return QuizResult.Adapt<QuizResultForViewDTO>();
        }

        public async ValueTask<IEnumerable<QuizResultForViewDTO>> GetAllAsync(PaginationParams @params, Expression<Func<QuizResult, bool>> expression = null)
        {
            var QuizResults = quizResultRepository.GetAll(expression: expression, isTracking: false, includes: new string[] { "User", "Quiz" });
            return (await QuizResults.ToPagedList(@params).ToListAsync()).Adapt<List<QuizResultForViewDTO>>();
        }

        public async ValueTask<QuizResultForViewDTO> GetAsync(Expression<Func<QuizResult, bool>> expression)
        {
            var QuizResult = await quizResultRepository.GetAsync(expression, new string[] { "User", "Quiz" });
            if (QuizResult is null)
                throw new TestingSystemException(404, "QuizResult Not Found");

            return QuizResult.Adapt<QuizResultForViewDTO>();
        }
    }
}
