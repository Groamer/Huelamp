using HueLamps;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;



namespace HueLamps
{

    public sealed partial class MainPage : Page
    {


        public static ApplicationDataContainer LOCAL_SETTINGS = ApplicationData.Current.LocalSettings;
        public static API api = null;
        private ObservableCollection<Bulb> totalBulbs = new ObservableCollection<Bulb>();
        private bool appStarted = false;
        public static Bulb currentBulb;

        public MainPage()
        {
            this.InitializeComponent();
            api = new API(new Network());
        }



        private async void button_Click(object sender, RoutedEventArgs e)
        {
            //TEMP CODE FOR RGB. RGB MUST BE SET BY USER EVENTUALLY
            int red = 255;
            int green = 0;
            int blue = 1;
            //END TEMP CODE


            api.Register();

            ObservableCollection<Bulb> bulbs = await api.GetAllLights(totalBulbs);
            HueCalculator hue = new HueCalculator();
            listBox.Items.Clear();
            foreach (Bulb bulb in bulbs)
            {
                listBox.Items.Add("Lamp " + bulb.id);
                bulb.on = true;
                api.SetLightState(bulb);
               // bulb.hue = hue.CalculateHue(red, green, blue);
                //bulb.bri = hue.CalculateLum(red, green, blue);
                //bulb.sat = hue.CalculateSat(red, green, blue);

                bulb.hue = 0;
                bulb.bri = 254; //brightness 0 - 254
                bulb.sat = 255; //saturation 0 - 255
                

                api.SetLightValues(bulb);
            }
            if (totalBulbs.Count > 0)
                button.IsEnabled = false;

        }





        private async void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentBulb = totalBulbs.ElementAt(listBox.SelectedIndex);
            Frame.Navigate(typeof(BulbPage), currentBulb);

        }

        //toggles all lights on 
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            foreach (Bulb b in totalBulbs)
            {
                if (!b.@on)
                {
                    b.@on = true;
                    api.SetLightState(b);
                }

            }
        }

        //turns all lights off
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            foreach (Bulb b in totalBulbs)
            {
                if (b.on)
                {
                    b.@on = false;
                    api.SetLightState(b);
                }

            }
        }

        //sets all lights to max brightness & with colour white for productivity
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            foreach (Bulb b in totalBulbs)
            {
                if (!b.@on)
                {
                    b.@on = true;
                    api.SetLightState(b);
                    b.hue = 20000;
                    b.bri = 254; //brightness 0 - 254
                    b.sat = 0; //saturation 0 - 254
                    api.SetLightValues(b);
                }
            }
        }

        
    }
}
