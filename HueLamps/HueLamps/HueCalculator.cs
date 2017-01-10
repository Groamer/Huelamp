using System;
using System.Collections.Generic;

namespace HueLamps
{
    class HueCalculator
    {
        public HueCalculator()
        {
        }

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

            //check color values
            if (r == g && r == b)
            {
                hue = 0;
            }
            else if (max == r)
            {
                hue = (g - b) / (max - min);
            }
            else if (max == g)
            {
                hue = 2 + ((b - r) / (max - min));
            }
            else if (max == b)
            {
                hue = 4 + ((r - g) / (max - min));
            }

            //set hue value
            hue = hue * 60;

            if (hue < 0)
            {
                hue = hue + 360;
            }

            //convert hue
            if (hue > 0 && hue <= 120)
            {
                double multiplier = (double)25500 / 120;
                hue = hue * multiplier;
                hue = Math.Round(hue);
            }
            if (hue > 120 && hue <= 240)
            {
                double multiplier = (double)46920 / 240;
                hue = hue * multiplier;
                hue = Math.Round(hue);
            }
            if (hue > 240 && hue <= 360)
            {
                double multiplier = (double)65280 / 360;
                hue = hue * multiplier;
                hue = Math.Round(hue);
            }

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
            }
            else
            {
                int lum = CalculateLum(red, green, blue);

                if (lum <= 127)
                {
                    sat = Math.Round((max - min) / (max + min) * 255);
                }
                if (lum > 127)
                {
                    sat = Math.Round((max - min) / (2 - max - min) * 255);
                }
            }

            return (int)sat;
        }
    }
}
