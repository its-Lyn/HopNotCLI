{
  description = "HopNot build for nix!";

  inputs = {
    nixpkgs.url = "nixpkgs/nixpkgs-unstable";
  };

  outputs =
    { self, nixpkgs }:
    let
      systems = [ "x86_64-linux" ];
      legacy = nixpkgs.legacyPackages;
    in
    {
      packages = nixpkgs.lib.genAttrs systems (system: {
        default = legacy.${system}.callPackage ./nix/package.nix { };
        hopnot = self.packages.${system}.default;
      });
    };
}
