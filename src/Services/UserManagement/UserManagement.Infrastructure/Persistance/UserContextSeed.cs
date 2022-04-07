using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Persistance
{
    public class UserContextSeed
    {
        public static void SeedData(IMongoCollection<UserProfileEntity> userCollection)
        {
            bool existUser = userCollection.Find(p => true).Any();
            if (!existUser)
            {
                userCollection.InsertManyAsync(GetPreconfiguredUsers());
            }
        }

        private static IEnumerable<UserProfileEntity> GetPreconfiguredUsers()
        {
            return new List<UserProfileEntity>()
            {
                new UserProfileEntity()
                {
                    Id = "62301c36b877ce54b188d1cd",
                    FirstName ="bharath",
                    MiddleName="Kumar",
                    LastName =" kosuri",
                    DateOfBirth= DateTime.Now,
                    Age=30,
                    Email="bharath.kosuri@qentelli.com",
                    MobileNumber = "88675983409",
                    AlternateMobileNumber="88675983409",
                    Gender ="Male",
                    ZipCode ="124324",
                    IsPrimary=true,
                    IsActive=true,
                    IsDelete=false,
                },

                new UserProfileEntity()
                {
                    Id = "62301c36b877ce54b188d1ce",
                    FirstName ="Mohana",
                    MiddleName="Kumar",
                    LastName =" Tulasi",
                    DateOfBirth= DateTime.Now,
                    Age=28,
                    Email="bharath.kosuri@qentelli.com",
                    MobileNumber = "88675983409",
                    AlternateMobileNumber="88675983409",
                    Gender ="Male",
                    ZipCode ="124324",
                    IsPrimary=true,
                    IsActive=true,
                    IsDelete=false,
                }

               /*new UserProfileEntity()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Email="swaroop.kavali@qentelli.com",
                    FullName="swaroop.kavali",
                    UserType="admin",
                    Password ="",
                    PasswordSalt=""
                },
                new UserProfileEntity()
                {
                    Id = "602d2149e773f2a3990b47f7",
                    Email="hari.tamada@qentelli.com",
                    FullName="hari.tamada",
                    UserType="user",
                    Password ="",
                    PasswordSalt=""
                },
                new UserProfileEntity()
                {
                    Id = "602d2149e773f2a3990b47f8",
                    Email="praneeth.p@qentelli.com",
                    FullName="praneeth.p",
                    UserType="admin",
                    Password ="",
                    PasswordSalt=""
                },
                new UserProfileEntity()
                {
                    Id = "602d2149e773f2a3990b47f9",
                    Email="mohana.tulasi@qentelli.com",
                    FullName="mohana.tulasi",
                    UserType="practitioner",
                    Password ="",
                    PasswordSalt=""
                },
                new UserProfileEntity()
                {
                    Id = "602d2149e773f2a3990b47fa",
                    Email="naresh.g@qentelli.com",
                    FullName="naresh.g",
                    UserType="user",
                    Password ="",
                    PasswordSalt=""
                }*/
            };

        }
    }
}
