# SquareReflect

[![build project](https://github.com/lomination/SquareReflect/actions/workflows/build.yaml/badge.svg)](https://github.com/lomination/SquareReflect/actions/workflows/build.yaml)
[![test project](https://github.com/lomination/SquareReflect/actions/workflows/test.yaml/badge.svg)](https://github.com/lomination/SquareReflect/actions/workflows/test.yaml)
[![test coverage](https://github.com/lomination/SquareReflect/actions/workflows/test-cov.yaml/badge.svg)](https://github.com/lomination/SquareReflect/actions/workflows/test-cov.yaml)

## Project Description

SquareReflect is a 2D maze game with open mechanics thanks to infinite possibilities of blocks/tiles.

### Rules of the game

This game allows you to play a little character lost in a level and your goal is to reach the end of the level. No gravity physics make you continue your way until hitting a tile which can allow you to choose a new direction to go.

### Levels

Only some boards are aviable in 'Boards' directory but more are coming soon. The goal is to make an editor so everyone can make his own level

## Technologies

This project requires .NET version 6.0.

## Launch

### Launch via sources

To run core project that gives you overview of the game in a console interface, run the following command after clonning:

```dotnet run --project Core```

You can add the name of the board you want to play using:

```dotnet run --project Core -- <BoardName>```

(the board has to be located in 'Boards' directory)

To run tests, run:

```dotnet test```
