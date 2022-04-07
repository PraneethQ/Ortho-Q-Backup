using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Common;

namespace UserManagement.Domain.Entities
{
    public class UserProfileEntity
    {
        //Bson ID : To represent the id as Mongo DB ID. 
        [BsonId]
        //BsonType represents the objectId-> means the objectId is
        //generated in database.
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        
        public string MobileNumber { get; set; }

        public string AlternateMobileNumber { get; set; }

        public string Gender { get; set; }

        [BsonElement]
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }

        public string ZipCode { get; set; }
        public bool IsPrimary  { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }






    }
}
