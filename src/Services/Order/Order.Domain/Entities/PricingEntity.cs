using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Entities
{
    public class PricingEntity
    {
        //Bson ID : To represent the id as Mongo DB ID. 
        [BsonId]
        //BsonType represents the objectId-> means the objectId is
        //generated in database.
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public string Price { get; set; }
    }
}
