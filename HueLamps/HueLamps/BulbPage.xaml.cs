using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HueLamps
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BulbPage : Page
    {
        private Bulb currentBulb = MainPage.currentBulb;
        private HueCalculator calc = new HueCalculator();
        private int Red = 0 ;
        private int green = 0;
        private int blue = 0;

        public BulbPage()
        {
            this.InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        private void toggleButton_Click(object sender, RoutedEventArgs e)
        { 
            if (currentBulb.@on)
                currentBulb.@on = false;
            else
                currentBulb.@on = true;
                MainPage.api.SetLightState(currentBulb);
        }

        //brightness slider
        private void slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
           // currentBulb.bri = (int)sliderRed.Value;
           // MainPage.api.SetLightValues(currentBulb);
        }

        private void sliderRed_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Red = (int)sliderRed.Value;
            currentBulb.hue = calc.CalculateHue(Red, green, blue);
            currentBulb.bri = calc.CalculateLum(Red, green, blue);
            currentBulb.sat = calc.CalculateSat(Red, green, blue);
            MainPage.api.SetLightValues(currentBulb);
        }

        private void sliderBlue_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            blue= (int)sliderBlue.Value;
            currentBulb.hue = calc.CalculateHue(Red, green, blue);
            currentBulb.bri = calc.CalculateLum(Red, green, blue);
            currentBulb.sat = calc.CalculateSat(Red, green, blue);
            MainPage.api.SetLightValues(currentBulb);
        }

        private void sliderGreen_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            green = (int)sliderGreen.Value;
            currentBulb.hue = calc.CalculateHue(Red, green, blue);
            currentBulb.bri = calc.CalculateLum(Red, green, blue);
            currentBulb.sat = calc.CalculateSat(Red, green, blue);
            MainPage.api.SetLightValues(currentBulb);
        }

        private void image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (currentBulb.@on)
                currentBulb.@on = false;
            else
                currentBulb.@on = true;
            MainPage.api.SetLightState(currentBulb);

        }

        private void buttonColor1_Click(object sender, RoutedEventArgs e)
        {
            SetColorSliders(255, 0, 255);
        }
        private void buttonColor2_Click(object sender, RoutedEventArgs e)
        {
            SetColorSliders(128, 0, 255);
        }
        private void buttonColor3_Click(object sender, RoutedEventArgs e)
        {
            SetColorSliders(0, 0, 255);
        }
        private void buttonColor4_Click(object sender, RoutedEventArgs e)
        {
            SetColorSliders(0, 128, 255);
        }
        private void buttonColor5_Click(object sender, RoutedEventArgs e)
        {
            SetColorSliders(0, 255, 255);
        }
        private void buttonColor6_Click(object sender, RoutedEventArgs e)
        {
            SetColorSliders(0, 255, 128);
        }
        private void buttonColor7_Click(object sender, RoutedEventArgs e)
        {
            SetColorSliders(0, 255, 0);
        }
        private void buttonColor8_Click(object sender, RoutedEventArgs e)
        {
            SetColorSliders(128, 255, 0);
        }
        private void buttonColor9_Click(object sender, RoutedEventArgs e)
        {
            SetColorSliders(255, 255, 0);
        }
        private void buttonColor10_Click(object sender, RoutedEventArgs e)
        {
            SetColorSliders(255, 128, 0);
        }
        private void buttonColor11_Click(object sender, RoutedEventArgs e)
        {
            SetColorSliders(255, 0, 0);
        }
        private void buttonColor12_Click(object sender, RoutedEventArgs e)
        {
            SetColorSliders(255, 0, 128);
        }
        private void buttonColor13_Click(object sender, RoutedEventArgs e)
        {
            SetColorSliders(255, 255, 255);
        }
        private void buttonColor14_Click(object sender, RoutedEventArgs e)
        {
            SetColorSliders(128, 128, 128);
        }
        private void buttonColor15_Click(object sender, RoutedEventArgs e)
        {
            SetColorSliders(0, 0, 0);
        }

        private void SetColorSliders(int r, int g, int b)
        {
            currentBulb.hue = calc.CalculateHue(r, g, b);
            currentBulb.bri = calc.CalculateLum(r, g, b);
            currentBulb.sat = calc.CalculateSat(r, g, b);
            MainPage.api.SetLightValues(currentBulb);

            textBlock.Text = r + "";
            textBlock_Copy1.Text = b + "";
            textBlock_Copy.Text = g + "";

            sliderRed.Value = r;
            sliderGreen.Value = g;
            sliderBlue.Value = b;
        }
    }
}
