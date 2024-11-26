namespace HopNotCLI;

public static class OSAssets
{
	public static string C(int ansi)
		=> $"\x1b[38;5;{ansi}m";

	public static string E()
		=> "\x1b[0m";

	/// <summary>
	/// This is for use for printing out the data. For the ASCII any colours can be used.
	/// </summary>
	public static readonly Dictionary<string, int> ANSIAccents = new Dictionary<string, int>
	{
		{"nixos", 31},
		{"not_known", 41}
	};

	public static readonly Dictionary<string, List<string>> ASCII = new Dictionary<string, List<string>> {
		{
			"nixos",
			[
				$@"{C(31)}   \\     \\   //  {E()}",
				$@"{C(31)}  ==\\_____\\  /   {E()}",
				$@"{C(31)}    //      \\//   {E()}",
				$@"{C(31)} ==//       //==   {E()}",
				$@"{C(31)}  //\\_____//      {E()}",
				$@"{C(31)} // /\\\   \\==    {E()}",
				$@"{C(31)}   // \\     \\    {E()}"
			]
		},

		{
			"not_known",
			[
				@"    ____     ",
				@"   /..  \    ",
				@"   (<>  |    ",
				@"  / __  \\   ",
				@" ( /  \\ /|  ",
				@" |/\\ __)/|  ",
				@" \/-____\/   "
			]
		}
	};
}
