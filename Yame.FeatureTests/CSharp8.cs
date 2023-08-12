using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Yame.FeatureTests.Dto;

namespace Yame.FeatureTests
{
    /// <summary>
    /// Ref 
    /// </summary>
    [TestClass]
    public class CSharp8
    {
        [TestMethod]
        public void Test_CSharp8_PositionalPattern()
        {
            var m = new Member(null, null, new Leader(null, null));
            var act = m is Member(_, _, Leader(_, _));
            act.Should().BeTrue();

            m.LastName = "Sam";
            m.Manager = new Leader("AA", "BB");
            act = m is Member(_, "Sam", Leader("AA", "BB"));
            act.Should().BeTrue();

            act = m is Member(_, "Sam", Leader("AA", "zz"));
            act.Should().BeFalse();
        }


        [TestMethod]
        public void Test_CSharp8_PropertyPattern()
        {
            var m = new Member("AA", "BB", new Leader("CC", "DD"));

            var act = m is { FirstName: "AA", Manager: { LastName: "DD" } };
            act.Should().BeTrue();

            act = m is { FirstName: "XX", Manager: { LastName: "DD" } };
            act.Should().BeFalse();

        }

        [TestMethod]
        public void Test_CSharp8_TuplePattern()
        {
            var first = String.Empty;
            var second = String.Empty;
            var act =
            (first, second) switch
            {
                ("T", "T") => "T",
                ("T", "F") => "F",
                ("F", "T") => "F",
                ("F", "F") => "F",
                (_, _) => "invalid value"
            };
            act.Should().Be("invalid value");
        }
        [TestMethod]
        public void Test_CSharp8_SwitchExpressions()
        {
            var m = new Member("AA", "BB", new Leader("CC", "DD"));
            var o = (object)m;
            var act = GetAct(o);
            act.Should().Be("BB");

            m = new Member("AA", "XX", new Leader("DD", "DD"));
            o = (object)m.Manager;
            act = GetAct(o);
            act.Should().Be("D4");

            m = new Member("AA", "XX", new Leader("CC", "DD"));
            o = (object)m;
            act = GetAct(o);
            act.Should().Be("T");

           
            o = (object)1;
            act = GetAct(o);
            act.Should().Be("invalid");


            var a = "b";
            var b = "a";
            switch (b)
            {
                //case a: 不能這樣寫 可以改寫
                case string b1 when b1 == a:
                    Console.WriteLine(b);
                    break;
                default:
                    Console.WriteLine("default");
                    break;
            }
            var c = b switch
            {
                "a" => "gg",
                //a =>不能這樣寫 可以改寫
                string b1 when b1 == a => "ba",
                _ => "default"
            };

            static string GetAct(object o)
            {
                return o switch
                {
                    Member { LastName: "BB" } => "BB",
                    Leader r => r switch
                    {
                        _ when r.FirstName == r.LastName => "D4",
                        _ => "foo"
                    },
                    Member me => "T",
                    _ => "invalid"
                };
            }
        }


        [TestMethod]
        public void Test_CSharp8_RangeAndIndices()
        {
            int[] myarr = new int[] { 34, 56, 77, 88 };
            // Old Method
            var olastval = myarr[myarr.Length - 1];
            Console.WriteLine(myarr[0]);//34
            Console.WriteLine(myarr[1]);//56
            Console.WriteLine(myarr[2]);//77
            Console.WriteLine(myarr[3]);//88

            // New Method
            Index i = ^1;
            //var nlastval = myarr[^1];
            var nlastval = myarr[i];
            Console.WriteLine(myarr[^1]);//88
            Console.WriteLine(myarr[^2]);//77
            Console.WriteLine(myarr[^3]);//56
            Console.WriteLine(myarr[^4]);//34


            var list1 = myarr[..];//34, 56, 77, 88
            var list2 = myarr[..2];//34, 56
            var list3 = myarr[2..];//77, 88
            var list4 = myarr[0..2];//34, 56
            var list5 = myarr[0..^2];//34, 56
            var list6 = myarr[^3..^1];//56,77
            Range num = 1..3;
            var list7 = myarr[num];//56,77
        }
    }

    public class Leader
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Leader(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void Deconstruct(out string firstName, out string lastName)
        {
            firstName = FirstName;
            lastName = LastName;
        }
    }

    public class Member
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Leader Manager { get; set; }

        public Member(string firstName, string lastName, Leader manager)
        {
            FirstName = firstName;
            LastName = lastName;
            Manager = manager;
        }

        public void Deconstruct(out string firstName,
            out string lastName,
            out Leader manager)
        {
            firstName = FirstName;
            lastName = LastName;
            manager = Manager;
        }
    }
}
