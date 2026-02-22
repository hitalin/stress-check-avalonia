{
  description = "A Nix-flake-based C# development environment";

  inputs.nixpkgs.url = "github:nixos/nixpkgs/nixos-unstable";
  inputs.flake-utils.url = "github:numtide/flake-utils";

  outputs = { self, nixpkgs, flake-utils }:
    flake-utils.lib.eachDefaultSystem (system:
      let
        pkgs = import nixpkgs { inherit system; };
      in
      {
        devShell = pkgs.mkShell {
          packages = with pkgs; [
            dotnet-sdk_9
          ];

          LD_LIBRARY_PATH = pkgs.lib.makeLibraryPath (with pkgs; [
            fontconfig
            freetype
            libx11
            libxrandr
            libxcursor
            libxi
            libxext
            libxrender
            libGL
            libxkbcommon
            libICE
            libSM
          ]);
        };
      }
    );
}
