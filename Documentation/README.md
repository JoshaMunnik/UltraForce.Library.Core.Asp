# Documentation usage

## Installation

Run:
`dotnet tool update -g docfx`

## Build documentation

The output parameter in the configuration is set to `../docs`; this will create
a folder in the solution root called `docs` with the documentation. This folder can then
be used by github pages.

Run:
`docfx`

Or run to get a temporary local server:
`docfx --serve`
