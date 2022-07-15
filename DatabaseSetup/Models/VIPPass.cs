using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseSetup.Models
{
    [BsonIgnoreExtraElements]
    public class VIPPass
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Month monthId { get; set; }
        public List<VIPLoginTask> loginTasks { get; set; }
    
        public VIPPass(Month monthId, List<VIPLoginTask> loginTasks)
        {
            this.monthId = monthId;
            this.loginTasks = loginTasks;
        }
    }
}
