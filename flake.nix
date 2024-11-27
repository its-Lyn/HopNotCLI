{
  description = "HopNot build for nix!";

  inputs = {
    nixpkgs.url = "nixpkgs/nixpkgs-unstable";

    nix-github-actions = {
      url = "github:nix-community/nix-github-actions";
      inputs.nixpkgs.follows = "nixpkgs";
    };
  };

  outputs =
    {
      self,
      nixpkgs,
      nix-github-actions,
    }:
    let
      systems = [ "x86_64-linux" ];
      legacy = nixpkgs.legacyPackages;
    in
    {
      packages = nixpkgs.lib.genAttrs systems (system: {
        default = legacy.${system}.callPackage ./nix/package.nix { };
        hopnot = self.packages.${system}.default;
      });

      githubActions = nix-github-actions.lib.mkGithubMatrix {
        checks = nixpkgs.lib.getAttrs [ "x86_64-linux" ] self.packages; # only check compiling, so passing packages is fine
      };
    };
}
