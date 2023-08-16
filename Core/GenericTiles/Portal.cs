public class Portal : GenericTile {
    private readonly char portalId;
    public  char PortalId {get => portalId;}
    private readonly Position pairPos;
    public  Position PairPos {get => pairPos;}
    public Portal(char portalId, Position pairPos) {
        this.portalId = portalId;
        this.pairPos = pairPos;
    }
    public Portal(string maybeParameters) {
        string[] parameters = maybeParameters.Split('-');
        if (parameters[0].Length == 1) {
            portalId = parameters[0][0];
        } else {
            throw new ArgumentException($"Unexpected parameter portalId in \"maybeParameters\". Given {parameters[0]}, expected : one single char");
        }
        if (int.TryParse(parameters[1], out int x) && int.TryParse(parameters[2], out int y) && x >= 0 && y >= 0) {
            pairPos = new Position(x, y);
        } else {
            throw new Exception($"Invalid parameter pairPos in \"maybeParameters\". Given {parameters[0]} and {parameters[1]}, expected : 2 positive or null integers");
        }
    }
    public override int GetId() {
        return 7;
    }
    public override Portal Clone() {
        return new Portal(portalId, pairPos);
    }
    public override string ToString() {
        return portalId.ToString();
    }
    public override bool Equals(object? obj) {
        if (obj is null || GetType() != obj.GetType()) {
            return false;
        } else {
            return portalId == ((Portal)obj).portalId && pairPos == ((Portal)obj).pairPos;
        }
    }
    public override int GetHashCode() {
        int hash = 17 + GetId();
        hash = hash * 23 + portalId.GetHashCode();
        hash = hash * 23 + pairPos.GetHashCode();
        return hash;
    }
    public override Player WhenApproching(Player player) {
        return player.SetProp(PlayerProp.exitingPortalId, ' ');
    }
    public override Player WhenColliding(Player player) {
        if (player.GetProp(PlayerProp.exitingPortalId) == portalId) {
            return player.Continue();
        } else {
            return new Player(player, newPos : pairPos).SetProp(PlayerProp.exitingPortalId, portalId);
        }
    }
}
