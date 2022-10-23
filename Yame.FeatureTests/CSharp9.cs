using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yame.FeatureTests
{
    [TestClass]
    public class CSharp9
    {
        public class Demo
        {
            public string Name { get; init; }
            public Demo(string name)
            {
                Name = name;    
            }
        }
        [TestMethod]
        public void Test_CSharp9_Init()
        {
            Demo d = new("aa");
            //d.Name = "bb"; 編譯失敗
        }
        [TestMethod]
        public void Test_CSharp9_Record()
        {
            Product p1 = new(1, "AA", new Titie("ff", "tt"));
            var act = p1.ToString();
            act.Should().Be("Product { Id = 1, Name = AA, Tit = Titie { Ft = ff, Lt = tt } }");

            Product p2 = new(1, "AA", new Titie("ff", "tt"));
            var act2 = p1 == p2;
            //自動實作IEquatable overload == 和 != 運算子
            act2.Should().BeTrue();
            //自動實作GetHashCode
            var act3 = p1.GetHashCode() == p2.GetHashCode();
            act3.Should().BeTrue();
            //record 是class 只是實作了很多事情 語法糖 
            var act4 = ReferenceEquals(p1, p2);
            act4.Should().BeFalse();
            //自動實作 解構子
            var (a,b,c)=p1;
            a.Should().Be(1);
            b.Should().Be("AA");
            //with 可以改變特定參數 然後建立一個新record
            var t3 = new Titie("ff3", "tt3");
            var p3 = p1 with { Name = "Aaaa",Tit = t3 };
            var act6 = p3.ToString();
            act6.Should().Be("Product { Id = 1, Name = Aaaa, Tit = Titie { Ft = ff3, Lt = tt3 } }");

            var t1 = new Titie("ff", "tt");
            var t2 = new Titie("ff", "tt");
            var act5 = t1 == t2;
            //自動實作IEquatable overload == 和 != 運算子
            act5.Should().BeTrue();
        }
        public record  Product(int Id,string Name, Titie Tit);
        public record  Titie(string Ft,string Lt);

    }
}
