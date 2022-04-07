using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Persistance;
using UserManagement.Application.Features.Login.Queries;
using UserManagement.Application.Models;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Persistance;
using BC = BCrypt.Net.BCrypt;

namespace UserManagement.Infrastructure.Repositories
{
    public class UserManagementRepository : IUserManagementRepository
    {
        private readonly IUserContext _dbContext;

        public UserManagementRepository(IUserContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        #region CREARE USER

        public async Task<ActionReturnType> CreateUser(UserProfileEntity userProfile, UserAccountEntity userAccount)
        {
            await _dbContext.UserProfileEntity.InsertOneAsync(userProfile);

            //to get the scope identity from mongo collection after insert.
            var profileId = userProfile.Id;

            //assign the profile id & other values to account collection
            userAccount.UserProfileId = profileId;
            userAccount.ConfirmationType = ConfirmationType.Email;

            //Setting up expiry time for the confirmation token.
            //User has to verify his account with in 3 hours of registration.
            //What if the user is not verified the account with in 3 hours? shall we delete?
            userAccount.ConfirmationExpiresAt = DateTime.UtcNow.AddHours(3);
            userAccount.DependentProfiles = new List<string> { profileId };

            await _dbContext.UserAccountEntity.InsertOneAsync(userAccount);
            ReturnMessage message = new ReturnMessage
            {
                Message = "User Created Successfully"
            };
            return ActionSet.ActionReturnType(System.Net.HttpStatusCode.Created, message);
        }
        #endregion

        #region UPDATE USER
        public async Task<ActionReturnType> UpdateUser(UserProfileEntity user)
        {
            //The whole json object in the DB is replaced based on the product id.
            // as we know that Mongo DB stroes the information in Json format.

            var updateResult = await _dbContext
                                        .UserProfileEntity
                                        .ReplaceOneAsync(filter: g => g.Id == user.Id, replacement: user);

            // To check whether the delete operation done properly or not using the IsAcknowledged & ModifiedCount 
            return ActionSet.ActionReturnType(System.Net.HttpStatusCode.OK, "User updated successfully");
        }

        #endregion

        #region DELETE USER
        public async Task<ActionReturnType> DeleteUser(string Id)
        {
            //Mongo DB filters
            FilterDefinition<UserProfileEntity> filter = Builders<UserProfileEntity>.Filter.Eq(p => p.Id, Id);

            DeleteResult deleteResult = await _dbContext
                                                .UserProfileEntity
                                                .DeleteOneAsync(filter);

            // To check whether the delete operation done properly or not using the IsAcknowledged & DeletedCount 
            return ActionSet.ActionReturnType(System.Net.HttpStatusCode.NoContent, "User deleted successfully");
        }

        #endregion

        #region GET USER [By ID, By Email, By Name, All Users]

        public async Task<ActionReturnType> GetUserById(string Id)
        {
            var userObj = await _dbContext.UserProfileEntity.Find(p => p.Id == Id).FirstOrDefaultAsync();
            if (userObj != null)
            {
                return ActionSet.ActionReturnType(System.Net.HttpStatusCode.OK, userObj);
            }
            return ActionSet.ActionReturnType(System.Net.HttpStatusCode.NotFound, "user not found");
        }

        public async Task<ActionReturnType> GetUserByName(string firstName)
        {
            //return await _context.Products.Find(p => p.Category.Equals(productCategory, StringComparison.OrdinalIgnoreCase)).ToListAsync();

            //mongo DB filters
            FilterDefinition<UserProfileEntity> filter = Builders<UserProfileEntity>.Filter.Eq(p => p.FirstName, firstName);

            var userObj = await _dbContext
                            .UserProfileEntity
                            .Find(filter)
                            .FirstOrDefaultAsync();
            if (userObj != null)
            {
                return ActionSet.ActionReturnType(System.Net.HttpStatusCode.OK, userObj);
            }
            return ActionSet.ActionReturnType(System.Net.HttpStatusCode.NotFound, "user not found");
        }

        public async Task<ActionReturnType> GetUsers()
        {
            var userList = await _dbContext.UserProfileEntity.Find(p => true).ToListAsync();
            if (userList.Count > 0)
            {
                return ActionSet.ActionReturnType(System.Net.HttpStatusCode.OK, userList);
            }
            return ActionSet.ActionReturnType(System.Net.HttpStatusCode.NotFound, "no users exist");
        }

        public async Task<ActionReturnType> GetUsertByEmail(string email)
        {
            //return await _context.Products.Find(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase)).ToListAsync();

            //mongo DB filters
            FilterDefinition<UserProfileEntity> filter = Builders<UserProfileEntity>.Filter.Eq(p => p.Email, email);

            var userObj= await _dbContext
                            .UserProfileEntity
                            .Find(filter)
                            .FirstOrDefaultAsync();
            if (userObj != null)
            {
                return ActionSet.ActionReturnType(System.Net.HttpStatusCode.OK, userObj);
            }
            return ActionSet.ActionReturnType(System.Net.HttpStatusCode.NotFound, "user not found");
        }

        #endregion

        #region USER SUBSCRIPTION
        //User Subscription
        public async Task<ActionReturnType> UserSubscription(string email, string zipcode, string confirmationToken)
        {

            UserEmailSubscriptionEntity subscriptionEntity = new UserEmailSubscriptionEntity
            {
                Email = email,
                ZipCode = zipcode,
                ConfirmationToken = confirmationToken,
                IsMember = false,
            };
            await _dbContext.UserEmailSubscriptionEntity.InsertOneAsync(subscriptionEntity);

            ReturnMessage message = new ReturnMessage
            {
                Message = "User Subscription Created"
            };
            return ActionSet.ActionReturnType(System.Net.HttpStatusCode.Created, message);
        }

        #endregion

        #region VERIFY USER SUBSCRIPTION
        public async Task<ActionReturnType> VerifyUserSubscription(string email, string confirmationToken)
        {
            FilterDefinition<UserEmailSubscriptionEntity> emailFilter = Builders<UserEmailSubscriptionEntity>.Filter.Eq(p => p.Email, email);
            FilterDefinition<UserEmailSubscriptionEntity> tokenFilter = Builders<UserEmailSubscriptionEntity>.Filter.Eq(p => p.ConfirmationToken, confirmationToken);
            FilterDefinition<UserEmailSubscriptionEntity> combineFilters = Builders<UserEmailSubscriptionEntity>.Filter.And(emailFilter, tokenFilter);

            var update = Builders<UserEmailSubscriptionEntity>.Update.Set("EmailVerified", true).Set("ConfirmationVerifiedAt", DateTime.UtcNow).Set("IsActive", true);

            await _dbContext.UserEmailSubscriptionEntity.UpdateOneAsync(combineFilters, update);
            return ActionSet.ActionReturnType(System.Net.HttpStatusCode.OK, "Email subscription verified successfully");

        }
        #endregion

        #region VERIFY USER REGISTRATION

        public async Task<ActionReturnType> VerifyUserRegistration(string email, string confirmationToken)
        {
            // todo : before making user active , need to check the confirmation time validity.


            //update user email subscription entity : isMember should be active
            FilterDefinition<UserEmailSubscriptionEntity> filter = Builders<UserEmailSubscriptionEntity>.Filter.Eq(p => p.Email, email);
            var updateSubcriptionEntity = Builders<UserEmailSubscriptionEntity>.Update.Set("IsMember", true);
            await _dbContext.UserEmailSubscriptionEntity.UpdateOneAsync(filter, updateSubcriptionEntity);

            //update UserAccountEntity
            FilterDefinition<UserAccountEntity> emailFilter = Builders<UserAccountEntity>.Filter.Eq(p => p.Email, email);
            FilterDefinition<UserAccountEntity> tokenFilter = Builders<UserAccountEntity>.Filter.Eq(p => p.ConfirmationToken, confirmationToken);
            FilterDefinition<UserAccountEntity> combineFilters = Builders<UserAccountEntity>.Filter.And(emailFilter, tokenFilter);

            var update = Builders<UserAccountEntity>.Update.Set("EmailVerified", true).Set("ConfirmationVerifiedAt", DateTime.UtcNow).Set("IsActive", true);
            await _dbContext.UserAccountEntity.UpdateOneAsync(combineFilters, update);

            return ActionSet.ActionReturnType(System.Net.HttpStatusCode.OK, "User registration verified successfully");
        }

        #endregion

        #region FORGOT PASSWORD
        //email will send to user for the link
        // 1. we will check the email exist or not
        // 2. if email exist then save the record with reset password flag
        // 3. send an email with token and email to reset password
        // 4. in this case we have to set the timer for password reset
        // 5. from controller we need to publish the event
        // 6. this event will be consumed by the notification api
        // 7. separate email template for the forgotpassword.
        // 8. upon clicking the link we navigate user to reset password
        // 9. using the parameters of email and confiramtion token , we will
        //    identify the user and reset the password for the user.


        public async Task<ActionReturnType> ForgotPassword(string email, string token)
        {
            //Linq type expression.[first convert into builder and then executes]
            var isUser = _dbContext.UserAccountEntity.Find(p => p.Email == email).AnyAsync();

            //Mongo builder expression [faster execution]
            FilterDefinition<UserAccountEntity> filter = Builders<UserAccountEntity>.Filter.Eq(p => p.Email, email);
            var isUserExist = await _dbContext
                            .UserAccountEntity
                            .Find(filter)
                            .AnyAsync();
            if (!isUserExist)
            {
                //return a message that user not exist
                return ActionSet.ActionReturnType(System.Net.HttpStatusCode.NotFound, "User not found");
            }

            FilterDefinition<UserAccountEntity> emailFilter = Builders<UserAccountEntity>.Filter.Eq(p => p.Email, email);

            string lastModifiedBy = email != null ? email.Split('@')[0] : "system";

            var update = Builders<UserAccountEntity>.Update.Set("ConfirmationToken", token).Set("ConfirmationType", ConfirmationType.ResetPassword).Set("LastModifiedBy", lastModifiedBy).Set("LastModifiedDate", DateTime.UtcNow);
            await _dbContext.UserAccountEntity.UpdateOneAsync(emailFilter, update);

            return ActionSet.ActionReturnType(System.Net.HttpStatusCode.NoContent);
        }

        #endregion

        #region RESET PASSWORD
        //password details will save into db
        // 1. need to get the user info based on the email and token
        // 2. update/ reset the password for the user.
        // 3. also need to update the other flags if required based on the conditions.
        public async Task<ActionReturnType> ResetPassword(string email, string confirmationToken, string password, string passwordsalt, string passwordHashAlg)
        {
            FilterDefinition<UserAccountEntity> emailFilter = Builders<UserAccountEntity>.Filter.Eq(p => p.Email, email);
            FilterDefinition<UserAccountEntity> tokenFilter = Builders<UserAccountEntity>.Filter.Eq(p => p.ConfirmationToken, confirmationToken);
            FilterDefinition<UserAccountEntity> combineFilters = Builders<UserAccountEntity>.Filter.And(emailFilter, tokenFilter);

            string lastModifiedBy = email != null ? email.Split('@')[0] : "system";

            var update = Builders<UserAccountEntity>.Update.Set("Password", password).Set("PasswordSalt", passwordsalt).Set("LastModifiedBy", lastModifiedBy).Set("LastModifiedDate", DateTime.UtcNow);
            await _dbContext.UserAccountEntity.UpdateOneAsync(combineFilters, update);

            return ActionSet.ActionReturnType(System.Net.HttpStatusCode.OK, "Password reset was successful");
        }
        #endregion

        public async Task<ActionReturnType> LoginAuth(string email, string encriptedText)
        {
            var SECERT_KEY = "jaiMahakal@12345678dfjdssldfhaj98765432";

            var keySize = 256;

            var iterations = 100;

            var retObj = new UserLoginProfile();

            var salt = Convert.FromHexString(encriptedText.Substring(0, 32));

            var iv = Convert.FromHexString(encriptedText.Substring(32, 32));

            var encryptedPassword = Convert.FromBase64String(encriptedText.Substring(64));

            var keyDeriver = new Rfc2898DeriveBytes(SECERT_KEY, salt, iterations);

            byte[] key = keyDeriver.GetBytes(keySize/8);

            var decryptedPassword = DecryptStringFromBytes(encryptedPassword, key, iv);

            FilterDefinition<UserAccountEntity> filterAccount = Builders<UserAccountEntity>.Filter.Eq(p => p.Email, email);

            var userAccountObj = await _dbContext
                            .UserAccountEntity
                            .Find(filterAccount)
                            .FirstOrDefaultAsync();

            if (userAccountObj != null)
            {
                if (BC.Verify(decryptedPassword, userAccountObj.Password))
                {
                    retObj.Id = userAccountObj.UserProfileId;
                    retObj.Message = "Login is Success";
                    return new ActionReturnType
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        ResultSet = retObj
                    };
                }
                else
                {
                    retObj.Id = userAccountObj.UserProfileId;
                    retObj.Message = "Login Failed";
                    return new ActionReturnType
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        ResultSet = retObj
                    };
                }
                    
            }
            retObj.Id = "";
            retObj.Message = "User Not Found";
            return new ActionReturnType
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                ResultSet = retObj
            };
        }

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold the decrypted text.
            string decryptedPassword = null;

            // Create an RijndaelManaged object with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                try
                {
                    // Create the streams used for decryption.
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {

                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                decryptedPassword = srDecrypt.ReadToEnd();

                            }
                        }
                    }
                }
                catch
                {
                    decryptedPassword = "keyError";
                }
            }

            return decryptedPassword;
        }
    }
}
