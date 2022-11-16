using Application.Features.Auths.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Rules
{
    public class AuthBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public AuthBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public  Task UserShouldBeExist(User user)
        {
            if (user == null) throw new BusinessException(AuthMessages.UserDontExists);
            return Task.CompletedTask;
        }

        public async Task EmailCanNotBeDublicatedWhenRegistered(string email)
        {
            User? user =await _userRepository.GetAsync(u=>u.Email== email); 
            if(user != null) throw new BusinessException("Mail Already exist.");
        }

        internal async Task UserShouldBeMatch(int id, string password)
        {
            User? user=await _userRepository.GetAsync(u=>u.Id==id);
            if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) ;
                 throw new BusinessException(AuthMessages.PasswordDontMatch);
        }
    }
}
