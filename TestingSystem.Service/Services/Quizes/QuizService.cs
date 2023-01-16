using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestingSystem.Data.IRepositories;
using TestingSystem.Domain.Configurations;
using TestingSystem.Domain.Entities.Courses;
using TestingSystem.Domain.Entities.Quizes;
using TestingSystem.Service.DTOs.Quizes;
using TestingSystem.Service.Exceptions;
using TestingSystem.Service.Extensions;
using TestingSystem.Service.Interfaces.Quizes;

namespace TestingSystem.Service.Services.Quizes
{
    public class QuizService : IQuizService
    {
        private readonly IGenericRepository<Quiz> quizRepository;
        private readonly IGenericRepository<Course> courseRepository;
        private readonly IMapper mapper;

        public QuizService(IGenericRepository<Quiz> quizRepository, IGenericRepository<Course> courseRepository, IMapper mapper)
        {
            this.quizRepository = quizRepository;
            this.courseRepository = courseRepository;
            this.mapper = mapper;
        }

        public async ValueTask<QuizForViewDTO> CreateAsync(QuizForCreationDTO quizForCreationDTO)
        {
            var course = await courseRepository.GetAsync(c => c.Id == quizForCreationDTO.CourseId);

            if (course is null)
            {
                throw new TestingSystemException(404, "Course not found");
            }

            var quiz = await quizRepository.CreateAsync( mapper.Map<Quiz>(quizForCreationDTO));
            await quizRepository.SaveChangesAsync();
            return mapper.Map<QuizForViewDTO>(quiz);
        }

        public async ValueTask<bool> DeleteAsync(int id)
        {
            var isDeleted = await quizRepository.DeleteAsync(q => q.Id == id);

            if (!isDeleted)
                throw new TestingSystemException(404, "Quiz not found");
            await quizRepository.SaveChangesAsync();
            return true;
        }

        public async ValueTask<IEnumerable<QuizForViewDTO>> GetAllAsync(PaginationParams @params, Expression<Func<Quiz, bool>> expression = null)
        {
            var quizes = quizRepository.GetAll(expression: expression, isTracking: false, includes: new string[] { "Questions", "Questions.Answers", "Questions.Attachments"});
            return mapper.Map<List<QuizForViewDTO>>(await quizes.ToPagedList(@params).ToListAsync());
        }

        public async ValueTask<QuizForViewDTO> GetAsync(Expression<Func<Quiz, bool>> expression)
        {
            var quiz = await quizRepository.GetAsync(expression, new string[] { "Questions", "Questions.Answers", "Questions.Attachments" });
            if (quiz is null)
                throw new TestingSystemException(404, "Quiz not found");
            return mapper.Map<QuizForViewDTO>(quiz);
        }

        public async ValueTask<QuizForViewDTO> UpdateAsync(int id, QuizForCreationDTO quizForCreationDTO)
        {
            var existQuiz = await quizRepository.GetAsync(c => c.Id == id);
            if (existQuiz is null)
                throw new TestingSystemException(404, "Quiz not found");

            var course = await courseRepository.GetAsync(c => c.Id == quizForCreationDTO.CourseId);

            if (course is null)
                throw new TestingSystemException(404, "Course not found");


            existQuiz.UpdatedAt = DateTime.UtcNow;
            existQuiz = quizRepository.Update(mapper.Map( quizForCreationDTO, existQuiz));
            await quizRepository.SaveChangesAsync();

            return mapper.Map<QuizForViewDTO>(existQuiz);
        }
    }
}
