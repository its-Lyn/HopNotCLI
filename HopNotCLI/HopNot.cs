namespace HopNotCLI;

public static class HopNot
{
	public static bool ShowDays    = true;
	public static bool ShowHours   = true;
	public static bool ShowMinutes = true;
	public static bool ShowSeconds = false;

	public static void Main()
	{
		FileSystem fs = new FileSystem();
		OSData data = fs.GetDistroData();

		if (!OSAssets.ASCII.ContainsKey(data.ID)) data.ID = "not_known";

		List<string> output = [
			$"Currently on {OSAssets.C(OSAssets.ANSIAccents[data.ID])}{data.PrettyName}{OSAssets.E()}",
			$"\u2514\u2500\u2500Installed on {OSAssets.C(OSAssets.ANSIAccents[data.ID])}{fs.RootDate}{OSAssets.E()}",
			$"   \u2514\u2500\u2500{fs.GetTimeDifference()}."
		];


		int max = Math.Max(OSAssets.ASCII[data.ID].Count, output.Count);
		for (int i = 0; i < max; i++)
		{
			string first = i < OSAssets.ASCII[data.ID].Count ? OSAssets.ASCII[data.ID][i] : "";
			string second = i < output.Count ? output[i] : "";

			Console.WriteLine($"{first}{second}");
		}
	}
}

