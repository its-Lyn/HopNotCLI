{
  lib,
  buildDotnetModule,
  dotnetCorePackages,
	coreutils
}:

buildDotnetModule {
  pname = "HopNot";
  version = "0.9.0-rc1";

  dotnet-sdk = dotnetCorePackages.dotnet_8.sdk;
  dotnet-runtime = dotnetCorePackages.dotnet_8.runtime;

  src = lib.cleanSource ../.;
  projectFile = "HopNotCLI/HopNotCLI.csproj";

  nugetDeps = ./deps.nix;
  selfContainedBuild = true;

  executables = [ "hopnot" ];

	# Find stat.
	makeWrapperArgs = [
		"--prefix PATH : ${lib.makeBinPath [ coreutils ]}"
	];

  meta = {
    homepage = "https://github.com/its-Lyn/HopNotCLI";
    description = "Simple CLI app for distrohoppers.";
    license = lib.licenses.mit;
    mainProgram = "hopnot";
    platforms = lib.platforms.linux;
  };
}
