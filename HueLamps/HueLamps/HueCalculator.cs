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
            double hue = 0;
            double max = 0;
            double min = 1;
            var colors = new List<double>(){r, g, b};

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

            //set hue values
            hue = hue * 60;

            if (hue < 0)
            {
                hue = hue + 360;
            }

            hue = hue * 182;

            return (int)(hue);
        }
    }
}
