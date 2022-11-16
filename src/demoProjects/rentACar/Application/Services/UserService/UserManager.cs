using Application.Services.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserService
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepoistory;

        public UserManager(IUserRepository userRepoistory)
        {
            _userRepoistory = userRepoistory;
        }

        public async Task<User> GetByEmail(string email)
        {
            User? user = await _userRepoistory.GetAsync(u => u.Email == email);
            return user;
        }

        public async Task<User> GetById(int id)
        {
            User? user = await _userRepoistory.GetAsync(u => u.Id == id);
            return user;
        }

        public async Task<User> Update(User user)
        {
            User? Updateduser = await _userRepoistory.UpdateAsync(user);
            return Updateduser;
        }
    }
}
