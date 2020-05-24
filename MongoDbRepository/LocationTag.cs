using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbRepository
{

    [BsonCollection("locationTag")]
    public class LocationTag : Document
    {
        public int Sequence { get; set; }

        public string LocationName { get; set; }
    }
}
