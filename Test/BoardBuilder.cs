public static class BoardBuilder {
    public static Board<GenericTile> Create(string grid) {
        return Board<GenericTile>.Parse(
            $"{{title:{{unknown}};author:{{unknown}};date:{{unknown}};difficulty:{{unknown}};version:{{unknown}};grid:{{{grid}}};outOfBoardTile:{{5}};}}",
            new GenericTileCoDec()
        );
    }
}
