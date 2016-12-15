using System;
using System.Collections.Generic;

namespace HueLamps
{
    class HueCalculator
    {
        public HueCalculator()
        {
        }

        /*hue 
        value:
        red = 0 
        yellow = 12750
        green = 25500
        blue = 46920
        purple = 56100
        red = 65280*/

        public int CalculateHue(int red, int green, int blue)
        {
            double r = (double)red / 255;
            double g = (double)green / 255;
            double b = (double)blue / 255;
            var colors = new List<double>() { r, g, b };

            double max = 0;
            double min = 1;

            double hue = 0;

            //find max and min value
            for (int i = 0; i < colors.Count; i ++)
            {
                if (colors[i] > max)
                {
                    max = colors[i];
                }
                if (colors[i] < min)
                {
                    min = colors[i]; 
                }
            }
            
            //check color values
            if (max == r)
            {
                hue = (double)(g - b) / (max - min);
            }
            else if (max == g)
            {
                hue = (double)2 + ((b - r) / (max - min));
            }
            else
            {
                hue = (double)4 + ((r - g) / (max - min));
            }

            //set hue value
            hue = hue * 60;

            if (hue < 0)
            {
                hue = hue + 360;
            }

            hue = hue * 182;

            return (int)(hue);
        }

        public int CalculateLum(int red, int green, int blue)
        {
            double r = (double)red / 255;
            double g = (double)green / 255;
            double b = (double)blue / 255;
            var colors = new List<double>() { r, g, b };

            double max = 0;
            double min = 1;

            double lum = 0;

            //find max and min value
            for (int i = 0; i < colors.Count; i++)
            {
                if (colors[i] > max)
                {
                    max = colors[i];
                }
                if (colors[i] < min)
                {
                    min = colors[i];
                }
            }

            //calculate luminace
            lum = Math.Round(((max + min) / 2) * 254);

            System.Diagnostics.Debug.WriteLine("LUM VAL: " + lum);
            return (int)(lum);
        }

        public int CalculateSat(int red, int green, int blue)
        {
            double r = (double)red / 255;
            double g = (double)green / 255;
            double b = (double)blue / 255;
            var colors = new List<double>() { r, g, b };

            double max = 0;
            double min = 1;

            double sat = 0;

            //find max and min value
            for (int i = 0; i < colors.Count; i++)
            {
                if (colors[i] > max)
                {
                    max = colors[i];
                }
                if (colors[i] < min)
                {
                    min = colors[i];
                }
            }

            if (min == max)
            {
                sat = 0;
                System.Diagnostics.Debug.WriteLine("KUTFUCKSHIT");
            }
            else
            {
                if ((CalculateLum(red, green, blue) < 127))
                {
                    sat = Math.Round((max - min) / (max + min) * 255);
                }
                if ((CalculateLum(red, green, blue) > 127))
                {
                    sat = Math.Round((max - min) / (2 - max - min) * 255);
                }
            }

            System.Diagnostics.Debug.WriteLine("SAT VAL: " + sat);
            return (int)sat;
        }
    }
}
