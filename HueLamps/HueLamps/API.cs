using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace HueLamps
{
	public class API
	{
		Network network;
		public API(Network network)
		{
			this.network = network;
            
		}

		public async Task Register()
		{
			try
			{
				var json = await network.RegisterName("Hue", "Groamer");
				json = json.Replace("[", "").Replace("]", "");
				JObject o = JObject.Parse(json);
				string id = o["success"]["username"].ToString();
				MainPage.LOCAL_SETTINGS.Values["id"] = id;
			}
			catch (Exception e)
			{
				Debug.WriteLine("Could not register: " + e);
			}
		}

		public async void SetLightState(Bulb l)
		{
			var json = await network.SetLightInfo(l.id, $"{{\"on\": {((l.on) ? "true" : "false")}}}");
			//Debug.WriteLine(json);
		}

		public async void SetLightValues(Bulb l)
		{
			if (l.on)
			{
				//Debug.WriteLine(l.hue);
				var json = await network.SetLightInfo(l.id, $"{{\"bri\": {l.bri},\"hue\": {(l.hue)},\"sat\": {l.sat}}}");
				//Debug.WriteLine(json);
			}

		}

		public async Task<ObservableCollection<Bulb>> GetAllLights(ObservableCollection<Bulb> allLights)
		{
			try
			{
				var json = await network.AllLights();
				JObject o = JObject.Parse(json);
				foreach (var i in o)
				{
					var light = o["" + i.Key];
					var state = light["state"];
					allLights.Add(new Bulb() { api = this, id = Int32.Parse(i.Key), bri = (int)state["bri"], on = ((string)state["on"]).ToLower() == "true" ? true : false, hue = (int)state["hue"], sat = (int)state["sat"], name = (string)light["name"], type = (string)light["type"] });
					//Debug.WriteLine("Added light number " + i + " " + state["on"]);
				}

				//lightlist = lightlist.OrderBy(q => q.Name).ToList();

				//lightlist.Sort(
				//    delegate (Light p1, Light p2)
				//   {
				//       return p1.Name.CompareTo(p2.Name);
				//   }
				// );

				//lightlist.ForEach(q => alllights.Add(q));
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.StackTrace);
				Debug.WriteLine("Could not get all lights.");
			}
            return allLights;
		}
	}
}
