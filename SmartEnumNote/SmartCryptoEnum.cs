using Ardalis.SmartEnum;

namespace SmartEnumNote
{
    public class SmartCryptoEnum : SmartEnum<SmartCryptoEnum, int>
    {
        public static SmartCryptoEnum Bitcoin = new SmartCryptoEnum(nameof(Bitcoin), 0, 
            "BTC", "https://bitcoin.org/", "Satoshi Nakamoto", "Bitcoin is an innovative payment network and a new kind of money. Find all you need to know and get started with Bitcoin on bitcoin.org." );
        public static SmartCryptoEnum Litecoin = new SmartCryptoEnum(nameof(Litecoin), 1,
            "LTC", "https://litecoin.com/", "Charlie Lee", "Litecoin is a peer-to-peer cryptocurrency and open source software project released under the MIT/X11 license. Creation and transfer of coins is based on an open source cryptographic protocol and is not managed by any central authority.");
        public static SmartCryptoEnum Dogecoin = new SmartCryptoEnum(nameof(Dogecoin), 2,
            "DOGE", "https://dogecoin.com/", "Jackson Palmer", "Dogecoin is a cryptocurrency featuring a likeness of the Shiba Inu dog from the 'Doge' Internet meme as its logo.");

        private SmartCryptoEnum(string name, int value, string shortName, string url, string founder, string desc ) : base(name, value)
        {
            this.ShortName = shortName;
            this.WebsiteURL = url;
            this.Founder = founder;
            this.Description = desc;
        }

        public string ShortName { get; }
        public string WebsiteURL { get; }
        public string Founder { get; }
        public string Description { get; }
    }
}
