public class Start : GenericTile {
    private readonly Status[] dirs;
    public Status[] Dirs {get => dirs;}
    public Start(Status[] dirs) {
        this.dirs = dirs;
    }
    public Start(string maybeDirs) {
        List<Status> dirs = new();
        foreach (string maybeDir in maybeDirs.Split("-")) {
            if (int.TryParse(maybeDir, out int dir)) {
                if (dir >= 0 && dir <= 4) {
                    dirs.Add((Status)dir);
                } else {
                    throw new ArgumentException($"Invalid direction in parameter \"maybeDirs\" (given : {dir}, expected : 0, 1, 2 or 3)");
                }
            } else {
                throw new ArgumentException($"Invalid direction in parameter \"maybeDirs\" (given : {maybeDir}, expected : 0, 1, 2 or 3)");
            }
        }
        this.dirs = dirs.ToArray();
    }
    public override int GetId() {
        return 3;
    }
    public override bool Equals(object? obj) {
        if (obj is null || GetType() != obj.GetType() || Dirs.Length != ((Start)obj).Dirs.Length) {
            return false;
        } else {
            for (int i = 0; i < dirs.Length; i++) {
                if (dirs[i] != ((Start)obj).dirs[i]) {
                    return false;
                }
            }
            return true;
        }
    }
    public override int GetHashCode() {
        int hash = 17 + GetId();
        foreach (Status dir in dirs) {
            hash = hash * 23 + dir.GetHashCode();
        }
        // hash = (int)(hash * Math.Pow(23, 4 - dirs.Length));
        return hash;
    }
    public override Start Clone() {
        return new Start(dirs);
    }
    public override string ToString() {
        return " ";
    }
}
