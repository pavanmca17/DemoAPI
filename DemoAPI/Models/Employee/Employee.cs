using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Models
{
    public class Employee
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("EmpID")]
        public int EmpID { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
    }
}
