{
  lib,
  buildDotnetModule,
  dotnetCorePackages,
}:

buildDotnetModule {
  pname = "HopNot";
  version = "0.9.0";

  dotnet-sdk = dotnetCorePackages.dotnet_8.sdk;
  dotnet-runtime = dotnetCorePackages.dotnet_8.runtime;

  src = lib.cleanSource ../.;
  projectFile = "HopNotCLI/HopNotCLI.csproj";

  nugetDeps = ./deps.nix;
  selfContainedBuild = true;

  executables = [ "hopnot" ];

  meta = {
    homepage = "https://github.com/its-Lyn/HopNotCLI";
    description = "Simple CLI app for distrohoppers.";
    license = lib.licenses.mit;
    mainProgram = "hopnot";
    platforms = lib.platforms.linux;
  };
}
