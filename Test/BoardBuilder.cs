public static class BoardBuilder {
    public static Board Create(string grid) {
        return Board.Parse($"{{title:{{unknown}};author:{{unknown}};date:{{unknown}};difficulty:{{unknown}};version:{{unknown}};grid:{{{grid}}};}}");
    }
}
