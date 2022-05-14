using System;
using System.Linq;

using Ardalis.SmartEnum.JsonNet;

using Newtonsoft.Json;

namespace SmartEnumNote
{
    class Program
    {
        static void Main(string[] args)
        {
            // Original
            Console.WriteLine(CryptoEnum.Bitcoin.GetShortName());
            Console.WriteLine(CryptoEnum.Bitcoin.GetDisplayName());

            // Smart Enum
            Console.WriteLine(SmartCryptoEnum.Bitcoin.ShortName);
            Console.WriteLine(SmartCryptoEnum.Bitcoin.Name);

            Console.WriteLine(SmartCryptoEnum.Bitcoin.WebsiteURL);
            Console.WriteLine(SmartCryptoEnum.Bitcoin == SmartCryptoEnum.Dogecoin);
            
            // Get all registered coins
            SmartCryptoEnum.List.OrderBy(x=> x.Value).ToList().ForEach(x => Console.WriteLine($"{x.Name} : {x.Value}"));


            Console.WriteLine($"Dogecoin {SmartCryptoEnum.FromName("Dogecoin").Founder}");
            Console.WriteLine($"Value 1 {SmartCryptoEnum.FromValue(1).Founder}");
            Console.WriteLine("----------------------------------------------------");

            Vehicles vehicle = Vehicles.CAR;
            Console.WriteLine($"Price for Car {vehicle.Price}");
            Console.WriteLine($"Price for Bike {Vehicles.BIKE.Price}");
            Console.WriteLine($"Price for Van {Vehicles.FromName("VAN").Price}");
            Console.WriteLine($"Seat Capacity for Car {vehicle.SeatCapacity}");
            Console.WriteLine($"Seat Capacity for Bike {Vehicles.BIKE.SeatCapacity}");
            Console.WriteLine($"Seat Capacity for Van {Vehicles.FromName("VAN").SeatCapacity}");

            Console.WriteLine("----------------------------------------------------");
            var a = new Demo2{ Dev = Vehicles.BIKE };

            string json = JsonConvert.SerializeObject(a);
            Console.WriteLine(json);

            var d = JsonConvert.DeserializeObject<Demo2>(json);

            Console.WriteLine(d.Dev);


            Console.ReadKey();
        }
    }

    public class Demo1
    {
        public string Dev { get; set; }
    }

    public class Demo2
    {
        //注意 SmartEnumNameConverter 跟 SmartEnumValueConverter
        [JsonConverter(typeof(SmartEnumNameConverter<Vehicles,int>))]
        public Vehicles Dev { get; set; }
    }

}
