using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Entities
{
  public  class UserEmailSubscriptionEntity
    {
        //Bson ID : To represent the id as Mongo DB ID. 
        [BsonId]
        //BsonType represents the objectId-> means the objectId is
        //generated in database.
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Email { get; set; }
        public string ZipCode { get; set; }
        public bool EmailVerified { get; set; }
        public string Views { get; set; }
        public string ConfirmationToken { get; set; }
        [BsonElement]
        public DateTime ConfirmationVerifiedAt { get; set; }
        [BsonElement]
        public DateTime LastView { get; set; }
        public bool IsMember { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
