public static class BoardBuilder {
    public static Board<Tile> Create(string grid) {
        int[] size = new int[] {
            grid.Split(';')[0].Count(c => c == ',') + 1,
            grid.Count(c => c == ';') + 1
        };
        List<Tile[]> newGrid = new();
        foreach (string line in grid.Replace(" ", null).Split(';', StringSplitOptions.RemoveEmptyEntries)) {
            List<Tile> newLine = new();
            foreach (string tile in line.Split(',', StringSplitOptions.RemoveEmptyEntries)) {
                newLine.Add(TileCoDec.Read(tile));
            }
            newGrid.Add(newLine.ToArray());
        }
        return new Board<Tile>("1.0", size, new Empty(), new Death(), newGrid.ToArray());
    }
}
