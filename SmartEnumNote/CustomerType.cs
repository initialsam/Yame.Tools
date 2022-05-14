using Ardalis.SmartEnum;

using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnumNote
{
    public abstract class CustomerType : SmartEnum<CustomerType,int>
    {
        private CustomerType(string name, int value) : base(name, value) { }


        public static readonly CustomerType Regular = new RegularType();
        public static readonly CustomerType Gold = new GoldType();
        public static readonly CustomerType Premium = new PremiumType();

        public abstract double Calculate(int amount);

        private sealed class RegularType : CustomerType
        {
            public RegularType() : base("Regular", 1) { }

            public override double Calculate(int amount) => amount * 0.10;
        }

        private sealed class GoldType : CustomerType
        {
            public GoldType() : base("Gold", 2) { }

            public override double Calculate(int amount) => amount * 0.20;
        }

        private sealed class PremiumType : CustomerType
        {
            public PremiumType() : base("Premium", 3) { }

            public override double Calculate(int amount) => amount * 0.30;
        }
    }
}
