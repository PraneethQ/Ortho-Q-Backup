using Member.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Member.Infrastructure.Persistance
{
    public class MemberContextSeed
    {
        public static void SeedData(IMongoCollection<DentalEntity> userCollection)
        {
            bool existUser = userCollection.Find(p => true).Any();
            if (!existUser)
            {
                userCollection.InsertManyAsync(GetPreconfiguredUsers());
            }
        }

        private static IEnumerable<DentalEntity> GetPreconfiguredUsers()
        {
            return new List<DentalEntity>()
            {
                new DentalEntity()
                {
                    Id="62301c36b877ce54b188d1cd",
                    Problem="Cavity Problems",
                    CreatedBy="system",
                    CreatedOn=DateTime.UtcNow,
                    UpdatedBy=null,
                    UpdatedDate=null,
                    DeletedBy=null,
                    DeletedOn=null
                }
            };
        }
    }
}




