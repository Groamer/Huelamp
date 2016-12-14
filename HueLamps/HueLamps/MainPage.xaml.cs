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
            api.Register();
            
            ObservableCollection<Bulb> bulbs = await api.GetAllLights(totalBulbs);
            listBox.Items.Clear();
            foreach (Bulb bulb in bulbs)
            {
                listBox.Items.Add("Lamp " + bulb.id);
                bulb.on = true;
                api.SetLightState(bulb);
                bulb.hue = 65000; //hue 0 - 65280
                bulb.bri = 254; //brightness 0 - 254
                bulb.sat = 254; //saturation 0 - 254
                api.SetLightValues(bulb);
            }
        }

        private async void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bulb bulb = totalBulbs.ElementAt(listBox.SelectedIndex);
            bulb.@on = false;
            api.SetLightState(bulb);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            foreach (Bulb b in totalBulbs)
            {
                b.@on = !b.@on;
                api.SetLightState(b);
            }
        }
    }

   
}
