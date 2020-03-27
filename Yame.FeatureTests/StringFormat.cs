using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yame.FeatureTests
{
    [TestClass]
    public class StringFormat
    {
        [TestMethod]
        public void Test_StringFormat_value()
        {
            var dateTime = new DateTime(2020, 12, 31, 23, 59, 59);
            
            Console.WriteLine($"{250:C}");//NT$250.00
            Console.WriteLine($"{123:D}");//123
            Console.WriteLine($"{1234.4567:F}");//1234.46
            Console.WriteLine($"{1234.4567:G}");//1234.4567
            Console.WriteLine($"{120000000:N}");//120,000,000.00
            Console.WriteLine($"{0.2575:P}");//25.75%
            Console.WriteLine($"{0.75:R}");//0.75
            Console.WriteLine($"{15:X}");//F
            //-------------------------------------------
            Console.WriteLine($"{13.5:000.00}");//013.50
            Console.WriteLine($"{8869121234:(###) ### – ####}");//(886) 912 – 1234
            Console.WriteLine($"{9813.563:###.##}");//9813.56
            Console.WriteLine($"{1234.4567:0.0}");//1234.5
            Console.WriteLine($"{1234.4567:0.00}");//1234.46
            Console.WriteLine($"{120000000:0,0}");//120,000,000
            Console.WriteLine($"{0.2575:0%}");// 26 %
            Console.WriteLine($"{0.2575:0.00%}");// 25.75%
            //-------------------------------------------
            
            Console.WriteLine($"{dateTime:d}");//2020/12/31
            Console.WriteLine($"{dateTime:D}");//2020年12月31日
            Console.WriteLine($"{dateTime:f}");//2020年12月31日 下午 11:59
            Console.WriteLine($"{dateTime:F}");//2020年12月31日 下午 11:59:59
            Console.WriteLine($"{dateTime:g}");//2020/12/31 下午 11:59
            Console.WriteLine($"{dateTime:G}");//2020/12/31 下午 11:59:59
            Console.WriteLine($"{dateTime:m}");//12月31日
            Console.WriteLine($"{dateTime:o}");//2020-12-31T23:59:59.0000000
            Console.WriteLine($"{dateTime:R}");//Thu, 31 Dec 2020 23:59:59 GMT
            Console.WriteLine($"{dateTime:s}");//2020-12-31T23:59:59
            Console.WriteLine($"{dateTime:t}");//2020-12-31T23:59:59
            Console.WriteLine($"{dateTime:T}");//下午 11:59:59
            Console.WriteLine($"{dateTime:u}");//2020-12-31 23:59:59Z
            Console.WriteLine($"{dateTime:U}");//2020年12月31日 下午 03:59:59
            Console.WriteLine($"{dateTime:y}");//2020年12月
          
            //-------------------------------------------
            Console.WriteLine($"{dateTime:dd}");//31
            Console.WriteLine($"{dateTime:ddd}");//週四     (Thu)
            Console.WriteLine($"{dateTime:dddd}");//星期四  (Thursday)
            Console.WriteLine($"{dateTime:fff}");//000
            Console.WriteLine($"{dateTime:gg}");//西元
            Console.WriteLine($"{dateTime:hh}");//11        (12小時)
            Console.WriteLine($"{dateTime:HH}");//23        (24小時)
            Console.WriteLine($"{dateTime:mm}");//59
            Console.WriteLine($"{dateTime:MM}");//12
            Console.WriteLine($"{dateTime:MMM}");//十二月   (Dec)
            Console.WriteLine($"{dateTime:MMMM}");//十二月  (December)
            Console.WriteLine($"{dateTime:ss}");//59
            Console.WriteLine($"{dateTime:tt}");//下午
            Console.WriteLine($"{dateTime:yy}");//20
            Console.WriteLine($"{dateTime:yyy}");//2020
            Console.WriteLine($"{dateTime:yyyy}");//2020
            Console.WriteLine($"{dateTime:zz}");//+08
            Console.WriteLine($"{dateTime:zzz}");//+08:00
            Console.WriteLine($"{dateTime:yyyy-MM-dd hh:mm:ss}");//2020-12-31 11:59:59

        }

        [TestMethod]
        public void Test_ToString_ThreeParkFormat()
        {
            //Arrange
            var a = 1; //大於0
            var b = "AAA #;BBB -#;ccc";
            var expected = "AAA 1";
            //Act
            var actual = a.ToString(b);
            //Assert
            actual.Should().Be(expected);

            a = -1;//小於0
            expected = "BBB -1";
            actual = a.ToString(b);
            actual.Should().Be(expected);

            a = 0;//等於0
            expected = "ccc";
            actual = a.ToString(b);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Test_ToString_Format()
        {
            string[] names = { "Adam", "Bridgette", "Carla", "Daniel" };
            decimal[] hours = { 40, 6.667m, 40.39m, 82 };

            Console.WriteLine("{0,-20} {1,5}\n", "Name", "Hours");
            for (int ctr = 0; ctr < names.Length; ctr++)
                Console.WriteLine("{0,-20} {1,5:N1}", names[ctr], hours[ctr]);

            // The example displays the following output:
            //       Name                 Hours
            //
            //       Adam                  40.0
            //       Bridgette              6.7
            //       Carla                 40.4
            //       Daniel                82.0
        }
    }
}
