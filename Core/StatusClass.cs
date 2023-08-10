public static class StatusClass {
    public static bool IsADir(Status status) {
        if ((int)status >= 0 && (int)status < 4) {
            return true;
        } else {
            return false;
        }
    }
    public static Status GetNextDir(Status dir) {
        return (Status)(((int)dir + 1) % 4);
    }
    public static Status GetOppositeDir(Status dir) {
        return (Status)(((int)dir + 2) % 4);
    }
    public static Status GetPreviousDir(Status dir) {
        return (Status)(((int)dir + 3) % 4);
    }
}
