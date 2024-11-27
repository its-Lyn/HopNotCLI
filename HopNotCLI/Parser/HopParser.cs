using CommandLine;
using HopNotCLI.Utilities.OS;

namespace HopNotCLI.Parser;

public class HopParser(string[] args)
{
	private class Options
	{
		[Option('n', "no-logo", Required = false, HelpText = "Only show the data, skipping the logo")]
		public bool NoLogo { get; set; }

		[Option('l', "logo", Required = false, HelpText = "Which logo to show.")]
		public string? Logo { get; set; }
	}

	public void Parse()
	{
		CommandLine.Parser.Default.ParseArguments<Options>(args)
			.WithParsed(opt =>
			{
				if (opt.NoLogo) HopNot.ParserData.NoLogo = true;

				if (opt.Logo is not null)
				{
					HopNot.Data.ID = opt.Logo;
					if (!OSAssets.ASCII.ContainsKey(HopNot.Data.ID))
						HopNot.Data.ID = "not_known";
				}
			})
			.WithNotParsed(errors =>
			{
				if (errors.IsVersion()) Environment.Exit(0);
				if (errors.IsHelp()) Environment.Exit(0);
			});
	}
}
