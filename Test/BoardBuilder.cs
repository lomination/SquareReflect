public static class BoardBuilder {
    public static Board<Tile> Create(string grid) {
        return BoardCoDec.Parse(
            $"{{title:{{unknown}};author:{{unknown}};date:{{unknown}};difficulty:{{unknown}};version:{{unknown}};grid:{{{grid}}};defaultTile:{{5}};}}"
        );
    }
}
