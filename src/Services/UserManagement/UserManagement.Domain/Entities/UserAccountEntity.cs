using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UserManagement.Domain.Common;

namespace UserManagement.Domain.Entities
{
    public class UserAccountEntity: EntityBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Email { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserProfileId { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public bool EmailVerified { get; set; }
        public object Confirmation { get; set; }
        public string ConfirmationToken { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [BsonRepresentation(BsonType.String)]
        public ConfirmationType ConfirmationType { get; set; }
        [BsonElement]
        public DateTime ConfirmationExpiresAt { get; set; }
        [BsonElement]
        public DateTime ConfirmationVerifiedAt { get; set; }
        public List<string> DependentProfiles { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }


    }

    public enum ConfirmationType
    {
        Email,
        ResetPassword
    }
}
