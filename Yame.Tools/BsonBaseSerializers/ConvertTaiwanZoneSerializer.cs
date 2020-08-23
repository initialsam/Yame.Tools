using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Text;
using Yame.Tools.Extensions;

namespace Yame.Tools.NetCore.BsonBaseSerializers
{
    public sealed class ConvertTaiwanZoneSerializer : SerializerBase<DateTime>

    {
        public override DateTime Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonType = context.Reader.CurrentBsonType;
            switch (bsonType)
            {
                case BsonType.DateTime:
                    var unixDate = context.Reader.ReadDateTime();
                    DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    var utcDate = start.AddMilliseconds(unixDate);
                    var twDate = utcDate.UtcToTaipeiStandardTime();
                    return twDate;
                default:
                    var message = string.Format("Cannot deserialize BsonDateTime from BsonType {0}.", bsonType);
                    throw new BsonSerializationException(message);
            }
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTime value)
        {
            var bsonDateTime = (BsonDateTime)value;
            context.Writer.WriteDateTime(bsonDateTime.MillisecondsSinceEpoch);
        }
    }
}
