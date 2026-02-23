using System;

namespace Nuodami4.PhiPlayer.ChartTools.Utils.Mathematics
{
    public static class EasingFunctions
    {
        public static double Linear(double x) => x;
        public static double OutSine(double x) => Math.Sin((x * Math.PI) / 2);
        public static double InSine(double x) => 1 - Math.Cos((x * Math.PI) / 2);
        public static double OutQuad(double x) => 1 - (1 - x) * (1 - x);
        public static double InQuad(double x) => x * x;
        public static double InOutSine(double x) => (-(Math.Cos(Math.PI * x) - 1) / 2.0);
        public static double InOutQuad(double x) => x < 0.5 ? 2 * x * x : 1 - Math.Pow(-2 * x + 2, 2) / 2.0;
        public static double OutCubic(double x) => 1 - (Math.Pow(1 - x, 3));
        public static double InCubic(double x) => (x * x * x);
        public static double OutQuart(double x) => 1 - Math.Pow(1 - x, 4);
        public static double InQuart(double x) => (x * x * x * x);
        public static double InOutCubic(double x) => x < 0.5 ? 4 * x * x * x : 1 - Math.Pow(-2 * x + 2, 3) / 2.0;
        public static double InOutQuart(double x) => x < 0.5 ? 8 * x * x * x * x : 1 - Math.Pow(-2 * x + 2, 4) / 2.0;
        public static double InOutQuint(double x) => x < 0.5 ? 16 * x * x * x * x * x : 1 - Math.Pow(-2 * x + 2, 5) / 2.0;
        public static double InQuint(double x) => x * x * x * x * x;
        public static double OutQuint(double x) => 1 - Math.Pow(1 - x, 5);
        public static double OutExpo(double x) => x == 1 ? 1 : 1 - Math.Pow(2, -10 * x);
        public static double InOutExpo(double x) => x == 0.0 ? 0.0 : x == 1.0 ? 1.0 : x < 0.5 ? Math.Pow(2, 20 * x - 10) / 2.0 : (2 - Math.Pow(2, -20 * x + 10)) / 2.0;
        public static double InExpo(double x) => x == 0 ? 0 : Math.Pow(2, 10 * x - 10);
        public static double OutCirc(double x) => Math.Sqrt(1 - Math.Pow(x - 1, 2));
        public static double InCirc(double x) => 1 - Math.Sqrt(1 - Math.Pow(x, 2));
        public static double OutBack(double x) => 1 + (1.70158f + 1) * Math.Pow(x - 1, 3) + 1.70158f * Math.Pow(x - 1, 2);
        public static double InBack(double x) => (1.70158f + 1) * x * x * x - 1.70158f * x * x;
        public static double InOutCirc(double x) => x < 0.5 ? (1 - Math.Sqrt(1 - Math.Pow(2 * x, 2))) / 2 : (Math.Sqrt(1 - Math.Pow(-2 * x + 2, 2)) + 1) / 2.0;
        public static double InOutBack(double x) => x < 0.5 ? (Math.Pow(2 * x, 2) * (((1.70158 * 1.525) + 1) * 2 * x - (1.70158 * 1.525))) / 2.0 : (Math.Pow(2 * x - 2, 2) * (((1.70158 * 1.525) + 1) * (x * 2 - 2) + (1.70158 * 1.525)) + 2) / 2.0;
        public static double OutElastic(double x) => x == 0 ? 0 : x == 1 ? 1 : Math.Pow(2, -10 * x) * Math.Sin((x * 10 - 0.75) * ((2 * Math.PI) / 3)) + 1;
        public static double InOutElastic(double x) => x == 0.0 ? 0.0 : x == 1.0 ? 1.0 : x < 0.5 ? -(Math.Pow(2, 20 * x - 10) * Math.Sin((20 * x - 11.125) * (2 * Math.PI / 4.5))) / 2 : (Math.Pow(2, -20 * x + 10) * Math.Sin((20 * x - 11.125) * (2 * Math.PI / 4.5))) / 2 + 1;
        public static double InElastic(double x) => x == 0 ? 0 : x == 1 ? 1 : -Math.Pow(2, 10 * x - 10) * Math.Sin((x * 10 - 10.75) * ((2 * Math.PI) / 3));
        public static double OutBounce(double x) => x < 1.0 / 2.75 ? 7.5625 * x * x : x < 2.0 / 2.75 ? 7.5625 * (x - 1.5 / 2.75) * (x - 1.5 / 2.75) + 0.75 : x < 2.5 / 2.75 ? 7.5625 * (x - 2.25 / 2.75) * (x - 2.25 / 2.75) + 0.9375 : 7.5625 * (x - 2.625 / 2.75) * (x - 2.625 / 2.75) + 0.984375;
        public static double InBounce(double x) => 1 - OutBounce(1 - x);
        public static double InOutBounce(double x) => x < 0.5 ? (1 - OutBounce(1 - 2 * x)) / 2.0 : (1 + OutBounce(2 * x - 1)) / 2.0;
    }
}
