using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseSetup.Models
{
    [BsonIgnoreExtraElements]
    public class VIPLoginTask
    {
        public int day { get; set; }
        public Reward reward { get; set; }
        public int requiredAmount { get; set; }
        public uint timestamp { get; set; }
    }
}
