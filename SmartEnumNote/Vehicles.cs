using Ardalis.SmartEnum;

using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnumNote
{
    public abstract class Vehicles : SmartEnum<Vehicles>
    {
        public static readonly Vehicles CAR = new CARVehicle();
        public static readonly Vehicles BIKE = new BIKEVehicle();
        public static readonly Vehicles VAN = new VANVehicle();
        public decimal Price { get; private set; }
        public int SeatCapacity { get; private set; }

        public Vehicles(string name, int value, decimal price, int seatCapacity) : base(name, value)
        {
            Price = price;
            SeatCapacity = seatCapacity;
        }
    }
    public sealed class CARVehicle : Vehicles
    {
        //Additional functionality can be implemented here
        public CARVehicle() : base("CAR", 1, 10000, 5) { }
    }
    public sealed class BIKEVehicle : Vehicles
    {
        //Additional functionality can be implemented here
        public BIKEVehicle() : base("BIKE", 2, 20000, 2) { }
    }
    public sealed class VANVehicle : Vehicles
    {
        //Additional functionality can be implemented here
        public VANVehicle() : base("VAN", 3, 30000, 12) { }
    }
}
