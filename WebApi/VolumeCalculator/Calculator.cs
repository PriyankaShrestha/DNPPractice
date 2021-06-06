using System;

namespace WebApi.VolumeCalculator
{
    public class Calculator
    {
        /*
        private double radius;
        private double height;
        private double pi;
        private double volume;

        public Calculator(double height, double radius)
        {
            this.height = height;
            this.radius = radius;
            pi = Math.PI;
        }
        */
        
        public double CalculateVolumeCylinder(double h, double r)
        {
            return Math.PI * (r * r) * h;
        }

        public double CalculateVolumeCone(double r, double h)
        {
            return (Math.PI * (r * r) * h) / 3;
        }
    }
}