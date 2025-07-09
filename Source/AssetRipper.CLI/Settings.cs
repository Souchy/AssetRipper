using CommandLine;
using Ookii.CommandLine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AssetRipper.CLI;

public class Settings
{
	[Option('v', "version", Required = false, HelpText = "Unity version")]
	public string UnityVersion { get; set; } = "2020.3.0f1";

	[Option('i', "input", Required = false, HelpText = "Input file (*.bundle)")]
	public string PathInput { get; set; }

	[Option('o', "output", Required = false, HelpText = "Output directory")]
	public string PathOutputDir { get; set; }

	[Option('t', "types", Required = false, HelpText = "Types to output, separated by ','")]
	public string Types { get; set; }

}
