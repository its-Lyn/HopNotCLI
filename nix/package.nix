{
  lib,
  buildDotnetModule,
  dotnetCorePackages,
}:
buildDotnetModule rec {
  pname = "HopNot";
  version = "0.5.0";

  dotnet-sdk = dotnetCorePackages.dotnet_8.sdk;
  dotnet-runtime = dotnetCorePackages.dotnet_8.runtime;

  src = ../.;
  projectFile = "HopNotCLI/HopNotCLI.csproj";

  nugetDeps = ./deps.nix;
  selfContainedBuild = true;

  executables = [ "hopnot" ];

  meta = with lib; {
    homepage = "https://github.com/its-Lyn/HopNotCLI";
    description = "Simple CLI app for distrohoppers.";

    license = licenses.mit;
  };
}
