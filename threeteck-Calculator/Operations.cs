﻿using System;

namespace threeteck_Calculator
{
    public static class Operations
    {
        public static double Add(double a, double b)
            => a + b;

        public static double Multiply(double a, double b)
            => a * b;

        public static double Subtract(double a, double b)
            => a - b;

        public static double Divide(double a, double b)
        {
            if (Math.Abs(b) <= 1e-9) throw new DivideByZeroException();
            return a / b;
        }
    }
}

