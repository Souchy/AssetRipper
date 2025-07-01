using AssetRipper.Export.UnityProjects.Configuration;
using AssetRipper.GUI.Web;

namespace AssetRipper.CLI;

internal class Program
{
	static void Main(string[] args)
	{
		Console.WriteLine("Hello, World!");

		//Settings? settings = Settings.Parse(args);


		GameFileLoader.Settings.LoadFromDefaultPath();
		GameFileLoader.Settings.SaveToDefaultPath();
		//SerializedSettings.Load("");

		GameFileLoader.LoadAndProcess([settings.PathInput]);
	}
}
