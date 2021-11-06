using System;
using System.Security.Principal;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace JsonNetNote
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Null時不要轉出Json()
        {
            Person person = new Person
            {
                Name = "Nigal Newborn",
                Age = 1
            };

            string jsonIncludeNullValues = JsonConvert.SerializeObject(person, Formatting.Indented);

            Console.WriteLine(jsonIncludeNullValues);
            // {
            //   "Name": "Nigal Newborn",
            //   "Age": 1,
            //   "Partner": null,
            //   "Salary": null
            // }

            string jsonIgnoreNullValues = JsonConvert.SerializeObject(person, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            Console.WriteLine(jsonIgnoreNullValues);
            // {
            //   "Name": "Nigal Newborn",
            //   "Age": 1
            // }
        }

        [TestMethod]
        public void Json字串有多的欄位class沒有出例外()
        {
            string json = @"
{
  'FullName': 'Dan Deleted',
  'Deleted': true,
  'DeletedDate': '2013-01-20T00:00:00'
}";
            try
            {
                JsonConvert.DeserializeObject<Account>(json, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                });
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine(ex.Message);
                // Could not find member 'DeletedDate' on object of type 'Account'. Path 'DeletedDate', line 4, position 23.
            }
        }

        [TestMethod]
        public void 物件忽略預設該型別值轉json()
        {
            Foo person = new Foo();

            string jsonIncludeDefaultValues = JsonConvert.SerializeObject(person, Formatting.Indented);

            Console.WriteLine(jsonIncludeDefaultValues);
            /*
                {
                    "Name": null,
                    "Age": 0,
                    "IsTrue": false,
                    "Today": "0001-01-01T00:00:00",
                    "TodayCanNull": null,
                    "point": 0.0,
                    "Partner": null
                }
             */

            string jsonIgnoreDefaultValues = JsonConvert.SerializeObject(person, Formatting.Indented, new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            Console.WriteLine(jsonIgnoreDefaultValues);
            // {}
        }
    }
}