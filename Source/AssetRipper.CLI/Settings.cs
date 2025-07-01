using Ookii.CommandLine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AssetRipper.CLI;

[GeneratedParser]
[ParseOptions(IsPosix = true)]
internal partial class Settings
{
	public string UnityVersion { get; set; } = "2020.3.0f1";

	[CommandLineArgument]
	[Description("Path to input file or directory")]
	public string PathInput { get; set; }

	[CommandLineArgument]
	[Description("Path to output directory.")]
	public string PathOutputDir { get; set; }

}
