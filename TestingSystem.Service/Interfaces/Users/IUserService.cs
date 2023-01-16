using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestingSystem.Domain.Configurations;
using TestingSystem.Domain.Entities.Users;
using TestingSystem.Domain.Enums;
using TestingSystem.Service.DTOs.Users;

namespace TestingSystem.Service.Interfaces.Users
{
    public interface IUserService
    {
        ValueTask<UserForViewDTO> CreateAsync(UserForCreationDTO userForCreationDTO);

        ValueTask<UserForViewDTO> UpdateAsync(int id, UserForUpdateDTO userForUpdateDTO);

        ValueTask<bool> DeleteAsync(int id);

        ValueTask<IEnumerable<UserForViewDTO>> GetAllAsync(
            PaginationParams @params,
            Expression<Func<User, bool>> expression = null);

        ValueTask<IEnumerable<UserForViewDTO>> GetAllByDegreeAndFullNameAsync(PaginationParams paginationParams, string degree, string fullName);

        ValueTask<UserForViewDTO> GetAsync(Expression<Func<User, bool>> expression);

        ValueTask<bool> ChangeRoleAsync(int id, UserRole userRole);

        ValueTask<bool> ChangePasswordAsync(UserForChangePasswordDTO userForChangePasswordDTO);
    }
}
