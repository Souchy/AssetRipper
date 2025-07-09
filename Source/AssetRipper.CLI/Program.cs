using AssetRipper.Export.Modules.Textures;
using AssetRipper.Export.UnityProjects.Configuration;
using AssetRipper.GUI.Web;
using AssetRipper.Primitives;
using CommandLine;
using System.Diagnostics;

namespace AssetRipper.CLI;

internal class Program
{
	static void Main(string[] args)
	{
		Parser.Default.ParseArguments<Settings>(args)
			.WithParsed(Run)
			.WithNotParsed(errors => Console.WriteLine("Failed to parse arguments."));
	}

	static void Run(Settings settings)
	{
		if (settings.PathOutputDir is null || !Directory.Exists(settings.PathOutputDir))
		{
			Console.WriteLine("Invalid export path specified.");
			return;
		}

		string[] inputs = settings.PathInput.Split(",");
		List<string> inputBundles = new();
		foreach (string input in inputs)
		{
			if (File.Exists(input))
			{
				inputBundles.Add(input);
			}
			else if (Directory.Exists(input))
			{
				string[] files = Directory.GetFiles(input, "*.bundle", SearchOption.AllDirectories);
				if (files.Length > 0)
				{
					inputBundles.AddRange(files);
				}
				else
				{
					Console.WriteLine($"No .bundle files found in directory '{input}'.");
				}
			}
			else
			{
				Console.WriteLine($"Input file '{input}' does not exist.");
			}
		}
		if (inputBundles.Count == 0)
		{
			Console.WriteLine("No valid input files found.");
			return;
		}

		try
		{
			UnityVersion version = UnityVersion.Parse(settings.UnityVersion);
			GameFileLoader.Settings.SetProjectSettings(version);
			// import settings
			GameFileLoader.Settings.ImportSettings.TargetVersion = version;
			GameFileLoader.Settings.ImportSettings.DefaultVersion = version;
			GameFileLoader.Settings.ImportSettings.ScriptContentLevel = Import.Configuration.ScriptContentLevel.Level0;
			// export settings
			GameFileLoader.Settings.ExportRootPath = settings.PathOutputDir;
			GameFileLoader.Settings.ExportSettings.CollectionsToExport = settings.Types.Split(",");

			// process and export
			GameFileLoader.LoadAndProcess(inputBundles);
			GameFileLoader.ExportPrimaryContent(settings.PathOutputDir);
			Console.WriteLine("Export completed successfully.");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An error occurred: {ex.Message}");
			Debug.WriteLine(ex);
		}

	}
}
