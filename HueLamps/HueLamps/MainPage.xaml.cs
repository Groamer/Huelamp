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
        private API api = null;
        private ObservableCollection<Bulb> totalBulbs = new ObservableCollection<Bulb>();

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
                bulb.hue = hue.CalculateHue(red, green, blue);
                bulb.bri = hue.CalculateLum(red, green, blue);
                bulb.sat = hue.CalculateSat(red, green, blue);

                /*OLD CODE
                bulb.bri = 254; //brightness 0 - 254
                bulb.sat = 255; //saturation 0 - 255
                */

                api.SetLightValues(bulb);
            }
        }

        private async void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bulb bulb = (Bulb) listBox.SelectedItem;
            bulb.@on = false;
            api.SetLightState(bulb);
        }
    }
}
