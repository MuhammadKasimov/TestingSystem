using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestingSystem.Data.IRepositories;
using TestingSystem.Domain.Configurations;
using TestingSystem.Domain.Entities.Courses;
using TestingSystem.Service.DTOs.Courses;
using TestingSystem.Service.Exceptions;
using TestingSystem.Service.Extensions;
using TestingSystem.Service.Interfaces.Courses;

namespace TestingSystem.Service.Services.CourseServices
{
    public class CourseService : ICourseService
    {
        private readonly IGenericRepository<Course> courseRepository;
        private readonly IMapper mapper;

        public CourseService(IGenericRepository<Course> courseRepository, IMapper mapper)
        {
            this.courseRepository = courseRepository;
            this.mapper = mapper;
        }

        public async ValueTask<CourseForViewDTO> CreateAsync(CourseForCreationDTO courseForCreationDTO)
        {
            var alreadyExistCourse = await courseRepository.GetAsync(c => c.Name == courseForCreationDTO.Name);

            if (alreadyExistCourse != null)
            {
                throw new TestingSystemException(400, "Course already exists");
            }

            var course = await courseRepository.CreateAsync(mapper.Map<Course>(courseForCreationDTO));
            await courseRepository.SaveChangesAsync();
            return mapper.Map<CourseForViewDTO>(course);
        }

        public async ValueTask<bool> DeleteAsync(int id)
        {
            var isDeleted = await courseRepository.DeleteAsync(c => c.Id == id);

            if (!isDeleted)
                throw new TestingSystemException(404, "Course not found");

            await courseRepository.SaveChangesAsync();
            return true;
        }

        public async ValueTask<IEnumerable<CourseForViewDTO>> GetAllAsync(PaginationParams @params, Expression<Func<Course, bool>> expression = null)
        {
            var courses = courseRepository.GetAll(expression: expression, isTracking: false, includes: new string[] { "Quizes"}).ToPagedList(@params);

            var listCourses = mapper.Map<List<CourseForViewDTO>>(await courses.ToListAsync());
            return listCourses;
        }

        public async ValueTask<CourseForViewDTO> GetAsync(Expression<Func<Course, bool>> expression)
        {
            var course = await courseRepository.GetAsync(expression, includes: new string[] { "Quizes" });
            if (course is null)
                throw new TestingSystemException(404, "Course not found");
            return mapper.Map<CourseForViewDTO>(course);
        }

        public async ValueTask<CourseForViewDTO> UpdateAsync(int id, CourseForCreationDTO courseForCreationDTO)
        {
            var existCourse = await courseRepository.GetAsync(c => c.Id == id);
            if (existCourse is null)
                throw new TestingSystemException(404, "Course not found");


            existCourse.UpdatedAt = DateTime.UtcNow;
            existCourse = courseRepository.Update(mapper.Map(courseForCreationDTO, existCourse));
            await courseRepository.SaveChangesAsync();

            return mapper.Map<CourseForViewDTO>(existCourse);
        }
    }
}
