using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Models;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Contracts.Persistance
{
    public interface IUserManagementRepository
    {
        Task<ActionReturnType> GetUsers();
        Task<ActionReturnType> GetUserById(string Id);
        Task<ActionReturnType> GetUserByName(string userName);
        Task<ActionReturnType> GetUsertByEmail(string email);

        Task<ActionReturnType> CreateUser(UserProfileEntity userProfile, UserAccountEntity userAccount);
        Task<ActionReturnType> UpdateUser(UserProfileEntity user);
        Task<ActionReturnType> DeleteUser(string Id);
        Task<ActionReturnType> UserSubscription(string email, string zipcode,string token);

        Task<ActionReturnType> VerifyUserSubscription(string email, string token);

        Task<ActionReturnType> VerifyUserRegistration(string email, string token);

        Task<ActionReturnType> ForgotPassword(string email, string token);

        Task<ActionReturnType> ResetPassword(string email, string confirmationToken,string password, string passwordsalt, string passwordHashAlg);

        Task<ActionReturnType> LoginAuth(string email, string encriptedText);

    }
}
