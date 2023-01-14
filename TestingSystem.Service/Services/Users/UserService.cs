﻿using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestingSystem.Data.IRepositories;
using TestingSystem.Domain.Configurations;
using TestingSystem.Domain.Entities.Users;
using TestingSystem.Domain.Enums;
using TestingSystem.Service.DTOs.Users;
using TestingSystem.Service.Exceptions;
using TestingSystem.Service.Extensions;
using TestingSystem.Service.Helpers;
using TestingSystem.Service.Interfaces.Users;

namespace TestingSystem.Service.Services
{
    public class UserService : IUserService
    {
        public IGenericRepository<User> userRepository;

        public UserService(IGenericRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async ValueTask<UserForViewDTO> CreateAsync(UserForCreationDTO userForCreationDTO)
        {
            var alreadyExistUser = await userRepository.GetAsync(u => u.Username == userForCreationDTO.Username);

            if (alreadyExistUser != null)
                throw new TestingSystemException(400, "User with such username already exist");

            userForCreationDTO.Password = userForCreationDTO.Password.Encrypt();

            var user = await userRepository.CreateAsync(userForCreationDTO.Adapt<User>());
            await userRepository.SaveChangesAsync();
            return user.Adapt<UserForViewDTO>();
        }

        public async ValueTask<bool> DeleteAsync(int id)
        {
            var isDeleted = await userRepository.DeleteAsync(u => u.Id == id);
            if (!isDeleted)
                throw new TestingSystemException(404, "User not found");
            return true;
        }

        public async ValueTask<IEnumerable<UserForViewDTO>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null)
        {
            var users = userRepository.GetAll(expression: expression, isTracking: false);

            return (await users.ToPagedList(@params).ToListAsync()).Adapt<List<UserForViewDTO>>();
        }

        public async ValueTask<IEnumerable<UserForViewDTO>> GetAllByDegreeAndFullNameAsync(PaginationParams paginationParams, string degree, string fullName) => 
            await GetAllAsync(paginationParams, u => (u.Degree == degree ||
                          string.IsNullOrEmpty(degree)) &&
                          (u.FirstName.Contains(fullName) ||
                          u.LastName.Contains(fullName) ||
                          "{u.FirstName} {u.LastName}".Contains(u.FirstName) 
                          || string.IsNullOrEmpty(fullName)));

        public async ValueTask<UserForViewDTO> GetAsync(Expression<Func<User, bool>> expression)
        {
            var user = await userRepository.GetAsync(expression);

            if (user is null)
                throw new TestingSystemException(404, "User not found");

            return user.Adapt<UserForViewDTO>();
        }

        public async ValueTask<UserForViewDTO> UpdateAsync(int id, UserForUpdateDTO userForUpdateDTO)
        {
            var existUser = await GetAsync(
                u => u.Id == id);

            if (existUser == null)
                throw new TestingSystemException(404, "User not found");

            var alreadyExistUser = await userRepository.GetAsync(
                u => u.Username == userForUpdateDTO.Username && u.Id != HttpContextHelper.UserId);

            if (alreadyExistUser != null)
                throw new TestingSystemException(400, "User with such username already exists");

            var user = existUser.Adapt<User>();

            user.UpdatedAt = DateTime.UtcNow;
            user = userRepository.Update(user = userForUpdateDTO.Adapt(user));
            await userRepository.SaveChangesAsync();

            return user.Adapt<UserForViewDTO>();
        }

        public async ValueTask<bool> ChangeRoleAsync(int id, UserRole userRole)
        {
            var existUser = await userRepository.GetAsync(u => u.Id == id);
            if (existUser == null)
                throw new TestingSystemException(404, "User not found");

            existUser.Role = userRole;

            userRepository.Update(existUser);
            await userRepository.SaveChangesAsync();

            return true;
        }

        public async ValueTask<bool> ChangePasswordAsync(UserForChangePasswordDTO userForChangePasswordDTO)
        {
            var user = await userRepository.GetAsync(u => u.Id == HttpContextHelper.UserId);

            if (user == null)
                throw new TestingSystemException(404, "User not found");

            if (user.Password != userForChangePasswordDTO.OldPassword.Encrypt())
                throw new TestingSystemException(400, "Password is Incorrect");


            user.Password = userForChangePasswordDTO.NewPassword.Encrypt();

            userRepository.Update(user);
            await userRepository.SaveChangesAsync();
            return true;
        }
    }
}