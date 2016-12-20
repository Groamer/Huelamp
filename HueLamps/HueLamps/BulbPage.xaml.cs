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
        int red = 0;
        int green = 0;
        int blue = 1;

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
            red = (int)sliderRed.Value;
            currentBulb.hue = calc.CalculateHue(red, green, blue);
            currentBulb.bri = calc.CalculateLum(red, green, blue);
            currentBulb.sat = calc.CalculateSat(red, green, blue);
            MainPage.api.SetLightValues(currentBulb);
        }

        private void sliderBlue_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            blue= (int)sliderBlue.Value;
            currentBulb.hue = calc.CalculateHue(red, green, blue);
            currentBulb.bri = calc.CalculateLum(red, green, blue);
            currentBulb.sat = calc.CalculateSat(red, green, blue);
            MainPage.api.SetLightValues(currentBulb);
        }

        private void sliderGreen_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            green = (int)sliderGreen.Value;
            currentBulb.hue = calc.CalculateHue(red, green, blue);
            currentBulb.bri = calc.CalculateLum(red, green, blue);
            currentBulb.sat = calc.CalculateSat(red, green, blue);
            MainPage.api.SetLightValues(currentBulb);
        }
    }
}
