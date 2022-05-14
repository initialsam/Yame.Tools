using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartEnumNote
{
    enum CryptoEnum
    {
        [Display(Name="Bitcoin", ShortName = "BTC")]
        Bitcoin,
        [Display(Name = "Litecoin", ShortName = "LTC")]
        Litecoin
    }
}
