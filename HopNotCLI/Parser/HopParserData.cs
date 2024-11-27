namespace HopNotCLI.Parser;

public record struct HopParserData(
	bool ShowDays,
	bool ShowHours,
	bool ShowMinutes,
	bool ShowSeconds,

	bool NoLogo
);
