using HopNotCLI.Parser;
using HopNotCLI.Utilities;
using HopNotCLI.Utilities.OS;

namespace HopNotCLI;

public static class HopNot
{
	public static HopParserData ParserData  = new HopParserData(
		true,
		true,
		true,
		false,
		false
	);

	public static OSData Data;

	private static List<string> _output = [];

	private static void ShowOut()
	{
		foreach (string line in _output)
			Console.WriteLine(line);
	}

	public static void Main(string[] args)
	{
		FileSystem fs = new FileSystem();
		Data = fs.GetDistroData();

		HopParser parser = new HopParser(args);
		if (args.Length > 0)
			parser.Parse();

		_output = [
			$"Currently on {OSAssets.C(OSAssets.ANSIAccents[Data.ID])}{Data.PrettyName}{OSAssets.E()}",
			$"\u2514\u2500\u2500Installed on {OSAssets.C(OSAssets.ANSIAccents[Data.ID])}{fs.RootDate}{OSAssets.E()}",
			$"   \u2514\u2500\u2500{fs.GetTimeDifference()}."
		];

		if (!OSAssets.ASCII.ContainsKey(Data.ID)) Data.ID = "not_known";

		if (ParserData.NoLogo)
		{
			ShowOut();
			return;
		}

		int max = Math.Max(OSAssets.ASCII[Data.ID].Count, _output.Count);
		for (int i = 0; i < max; i++)
		{
			string first = i < OSAssets.ASCII[Data.ID].Count ? OSAssets.ASCII[Data.ID][i] : "";
			string second = i < _output.Count ? _output[i] : "";

			Console.WriteLine($"{first}{second}");
		}
	}
}

