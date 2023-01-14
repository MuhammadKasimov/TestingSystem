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
using TestingSystem.Service.DTOs.Courses;
using TestingSystem.Service.Exceptions;
using TestingSystem.Service.Extensions;
using TestingSystem.Service.Interfaces;

namespace TestingSystem.Service.Services.CourseServices
{
    public class CourseService : ICourseService
    {
        public IGenericRepository<Course> courseRepository;

        public CourseService(IGenericRepository<Course> courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public async ValueTask<CourseForViewDTO> CreateAsync(CourseForCreationDTO courseForCreationDTO)
        {
            var alreadyExistCourse = await courseRepository.GetAsync(c => c.Name == courseForCreationDTO.Name);

            if (alreadyExistCourse != null)
            {
                throw new TestingSystemException(400, "Course already exists");
            }

            var course = await courseRepository.CreateAsync(courseForCreationDTO.Adapt<Course>());
            await courseRepository.SaveChangesAsync();
            return course.Adapt<CourseForViewDTO>();
        }

        public async ValueTask<bool> DeleteAsync(int id)
        {
            var isDeleted = await courseRepository.DeleteAsync(c => c.Id == id);
            
            if(!isDeleted)
                throw new TestingSystemException(404, "Course not found");
            return true;
        }

        public async ValueTask<IEnumerable<CourseForViewDTO>> GetAllAsync(PaginationParams @params, Expression<Func<Course, bool>> expression = null)
        {
            var courses = courseRepository.GetAll(expression: expression, isTracking: false);
            return (await courses.ToPagedList(@params).ToListAsync()).Adapt<List<CourseForViewDTO>>();
        }

        public async ValueTask<CourseForViewDTO> GetAsync(Expression<Func<Course, bool>> expression)
        {
            var course = await courseRepository.GetAsync(expression);
            if (course is null)
                throw new TestingSystemException(404, "Course not found");
            return course.Adapt<CourseForViewDTO>();
        }

        public async ValueTask<CourseForViewDTO> UpdateAsync(int id, CourseForCreationDTO courseForCreationDTO)
        {
            var existCourse = await courseRepository.GetAsync(c => c.Id == id);
            if (existCourse is null)
                throw new TestingSystemException(404, "Course not found");

            var course = existCourse.Adapt<Course>();

            course.UpdatedAt = DateTime.UtcNow;
            course = courseRepository.Update(course = courseForCreationDTO.Adapt(course));
            await courseRepository.SaveChangesAsync();

            return course.Adapt<CourseForViewDTO>();
        }
    }
}
