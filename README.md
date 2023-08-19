# SquareReflect

[![build project](https://github.com/lomination/SquareReflect/actions/workflows/build.yaml/badge.svg)](https://github.com/lomination/SquareReflect/actions/workflows/build.yaml)
[![test project](https://github.com/lomination/SquareReflect/actions/workflows/test.yaml/badge.svg)](https://github.com/lomination/SquareReflect/actions/workflows/test.yaml)
![badge](https://img.shields.io/endpoint?url=https://gist.githubusercontent.com/lomination/fb1427114448a5822f0b7b25a84cf527/raw/SquareReflect-coverage.json)

## Project Description

SquareReflect is a 2D maze game with open mechanics thanks to infinite possibilities of blocks/tiles.

This game allows you to play a little character lost in a level and your goal is to reach the end of the level. No gravity physics make you continue your way until hitting a tile which can allow you to choose a new direction to go.

The project is currently divided in different projects :
- Core/Core.csproj manage mechanics and global classes such as tiles, boards and games and cannot be launched,
- ConsoleUI/ConsoleUI.csproj allows the Core project to be displayed and to manage inputs through console,
- GraphicUI/GraphicUI.csproj launchs an application that makes the game more clear thanks to graphic labrary MonoGame based on Microsoft's XNA library (currently WIP)
- Test/Test.csproj contains all the written tests

Only some boards are aviable in 'Boards' directory but more are coming soon. The goal is to make an editor so everyone can make his own level.

## Technologies

This project requires .NET version 6.0 and Monogame library version 3.8.1.303.

## Launch

### Launch via sources

#### ConsoleUI

To run ConsoleUI project that gives you overview of the game in the console:

```dotnet run --project ConsoleUI/ConsoleUI.csproj```

You can optionaly add the name of the board you want to play using:

```dotnet run --project ConsoleUI/ConsoleUI.csproj -- <BoardName>```

(the board has to be located in 'Boards' directory with ".srboard" extension, as "Boards/ALevel.srboard")

Fonts have different renders, to maximaze the visibility you can set your terminal font to `Noto Sans CJK HK Bold`.

#### GraphicUI (WIP, not available yet)

To run GraphicUI prject, run:

```dotnet run --project GraphicUI/GraphicUI.csproj```

#### Test

To run tests, run:

```dotnet test```
