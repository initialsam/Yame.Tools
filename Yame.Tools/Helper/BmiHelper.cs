using System;
using System.Collections.Generic;
using System.Text;

namespace Yame.Tools.Helper
{
    public static class BmiHelper
    {
        public static float GetBmi(int height, int weight)
        {
            //先把公分轉公尺：1m=100cm
            double Height_M = (Convert.ToDouble(height) / 100);
            //BMI（體格指數）＝體重(kg)／身高(m)2
            var myBmi = (weight) / (Height_M * Height_M);
            var bmi = Convert.ToSingle(Math.Round(myBmi, 1, MidpointRounding.AwayFromZero));
            return bmi;
        }
    }
}
