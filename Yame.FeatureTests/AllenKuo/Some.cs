using System;
using System.Collections.Generic;
using System.Text;

namespace Yame.FeatureTests.AllenKuo
{
    /// <summary>
    /// 格子樑 FB 分享的 FP - 將物件抽象化
    /// https://vimeo.com/431136046/c0ba10ceac?fbclid=IwAR0DBagVovLCl12-rIin6yVYJY0VXdrX-0U1lq7OKGnJhwm1qth1mbXOfzs
    /// </summary>
    public class Some
    {
        private int number;
        public Some(int number)
        {
            this.number = number;
        }

        public static implicit operator Some(int value) => new Some(value);
        public static implicit operator int(Some source) => source.number;

        public static Some DoC(int num1, int num2) => num1 + num2;

    }

    public static class SomeExt
    {
        public static Some DoB(this Some source) => source + 2;

        public static Some DoA(this Some source) => source + 1;

        public static int Sum(this Some source) => source;
    }

    public class TestSome
    {
        public TestSome()
        {
            int result = Some.DoC(9, 10)
                .DoB()
                .DoA()
                .Sum();

        }
    }
}
