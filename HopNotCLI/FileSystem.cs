using System.Diagnostics;

namespace HopNotCLI;

public class FileSystem
{
	public DateTime RootDate;

	public FileSystem()
	{
		try
		{
			using Process stat = new Process();
			stat.StartInfo.FileName = "stat";
			stat.StartInfo.Arguments = "-c %W /";
			stat.StartInfo.UseShellExecute = false;
			stat.StartInfo.RedirectStandardOutput = true;

			stat.Start();

			string output = stat.StandardOutput.ReadToEnd().Trim();
			bool success = long.TryParse(output, out long stamp);
			if (!success) throw new Exception();

			RootDate = DateTimeOffset.FromUnixTimeSeconds(stamp).UtcDateTime.ToLocalTime();

			stat.WaitForExit();
		}
		catch (Exception)
		{
			try
			{
				 RootDate = Directory.GetCreationTime("/");
			}
			catch (Exception)
			{
				Console.WriteLine("hopnot: Cannot get root creation date. (tried stat & GetCreationTime()");
				Environment.Exit(1);
			}
		}
	}

	public string GetTimeDifference(DateTime? time = null)
	{
		time ??= DateTime.Now;
		TimeSpan difference = time.Value - RootDate;

		return (difference.Days > 0 && HopNot.ParserData.ShowDays ? $"{difference.Days} days " : "") +
			(difference.Hours > 0 && HopNot.ParserData.ShowHours ? $"{difference.Hours} hours " : "") +
			(difference.Minutes > 0 && HopNot.ParserData.ShowMinutes ? $"{difference.Minutes} minutes " : "") +
			(difference.Seconds > 0 && HopNot.ParserData.ShowSeconds ? $"{difference.Seconds} seconds " : "") +
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
