using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Entities
{
    public class UserGeoLocationEntity
    {
        //Bson ID : To represent the id as Mongo DB ID. 
        [BsonId]
        //BsonType represents the objectId-> means the objectId is
        //generated in database.
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string StateId { get; set; }
        public string StateName { get; set; }
        public string TimeZone { get; set; }
        public string Location { get; set; }
        public Enum LocationType { get; set; }
        public Array LocationCoordinates { get; set; }
    }
}
