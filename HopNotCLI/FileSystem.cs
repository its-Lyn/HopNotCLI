namespace HopNotCLI;

public class FileSystem
{
	public DateTime RootDate = Directory.GetCreationTime("/");

	public string GetTimeDifference(DateTime? time = null)
	{
		time ??= DateTime.Now;
		TimeSpan difference = time.Value - RootDate;

		return (difference.Days > 0 && HopNot.ShowDays ? $"{difference.Days} days " : "") +
			(difference.Hours > 0 && HopNot.ShowHours ? $"{difference.Hours} hours " : "") +
			(difference.Minutes > 0 && HopNot.ShowMinutes ? $"{difference.Minutes} minutes " : "") +
			(difference.Seconds > 0 && HopNot.ShowSeconds ? $"{difference.Seconds} seconds " : "") +
			"since installation";
	}

	public OSData GetDistroData()
	{
		if (!File.Exists("/etc/os-release"))
		{
			Console.Error.WriteLine(
				"hopnot: Cannot find /etc/os-release! Are you sure you're using linux?"
			);

			Environment.Exit(1);
		}

		OSData data = new OSData("Unknown", "not_known");

		string[] releaseData = File.ReadAllLines("/etc/os-release");
		foreach (string line in releaseData)
		{
			// 0 -> Identifier
			// 1 -> Data
			string[] tokens = line.Split('=');
			switch (tokens[0])
			{
				case "PRETTY_NAME":
					data.PrettyName = tokens[1].Trim('"');
					break;
				case "ID":
					data.ID = tokens[1].Trim('"');
					break;
			}
		}

		return data;
	}
}
