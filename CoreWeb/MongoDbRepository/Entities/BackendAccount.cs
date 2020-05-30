using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWeb.MongoDbRepository.Entities
{
    [BsonCollection("BackendAccount")]
    public class BackendAccount : Document
    {
        public string Account { get; set; }

        public string Watchword { get; set; }
    }
}
