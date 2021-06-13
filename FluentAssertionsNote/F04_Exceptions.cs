using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace FluentAssertionsNote
{
    /// <summary>
    /// ���e���O �x��Wiki�d�� https://fluentassertions.com/exceptions/
    /// </summary>
    [TestClass]
    public class F04_Exceptions
    {
        public class Calculator
        {
            public int Add(int a, int b)
            {
                throw new Exception("Test Exception");
            }
        }
        //public 
        [TestMethod]
        public void FluentAssertionsExceptions1()
        {
            var calculator = new Calculator();
            //act
            Action act = () => calculator.Add(1, 2);

            //assert
            act.Should()
               .Throw<Exception>()
               .WithMessage("Test Exception");

            //act.Should()
            //   .Throw<Exception>()
            //   .WithMessage("Exception");
            //���~�T��
            //Expected exception message to match the equivalent of
            //"Exception", but
            //"Test Exception" does not.

        }


    }
}
