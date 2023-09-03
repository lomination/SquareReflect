public class Player {
    private readonly Position pos;
    public Position Pos {get => pos;}
    private readonly Status status;
    public Status Status {get => status;}
    private readonly Tile behaviour;
    public Tile Behaviour {get => behaviour.Clone();}
    private readonly int numOfMoves;
    public int NumOfMoves {get => numOfMoves;}
    private readonly PropMap properties;
    public PropMap Properties {get => properties;}
    public Player(Position pos, Status status, Tile behaviour, int numOfMoves, PropMap properties) {
        this.pos = pos;
        this.status = status;
        this.behaviour = behaviour;
        this.numOfMoves = numOfMoves;
        this.properties = properties;
    }
    public Player(Position pos, Status status) : this(pos, status, new Empty(), 0, new PropMap()) {}
    public Player(Player oldPlayer, Position? newPos = null, Status? newStatus = null, Tile? newBehaviour = null, int? newNumOfMoves = null, PropMap? newPorperties = null) {
        if (newPos is Position pos) {
            this.pos = pos;
        } else {
            this.pos = oldPlayer.Pos;
        }
        if (newStatus is Status status) {
            this.status = status;
        } else {
            this.status = oldPlayer.Status;
        }
        if (newBehaviour is Tile behaviour) {
            this.behaviour = behaviour;
        } else {
            this.behaviour = oldPlayer.Behaviour;
        }
        if (newNumOfMoves is int numOfMoves) {
            this.numOfMoves = numOfMoves;
        } else {
            this.numOfMoves = oldPlayer.NumOfMoves;
        }
        if (newPorperties is PropMap properties) {
            this.properties = properties;
        } else {
            this.properties = oldPlayer.Properties;
        }
    }
    public override string ToString() {
        return $"{pos};{status};{behaviour.GetId()};{numOfMoves};{{{string.Join(", ", from pair in properties select $"{pair.Key}:{pair.Value}")}}};";
    }
    public override bool Equals(object? obj) {
        if (obj is null || GetType() != obj.GetType()) {
            return false;
        } else {
            Player other = (Player)obj;
            return pos.Equals(other.pos) &&
                status == other.status &&
                behaviour.Equals(other.behaviour) &&
                numOfMoves == other.numOfMoves &&
                properties.Equals(other.properties);
        }
    }
    public override int GetHashCode() {
        int hash = 17;
        hash = hash * 23 + pos.GetHashCode();
        hash = hash * 23 + (int)status;
        hash = hash * 23 + behaviour.GetHashCode();
        hash = hash * 23 + numOfMoves;
        hash = hash * 23 + properties.GetHashCode();
        return hash;
    }
    public Player SetProp<T>(Prop<T> property, T value) {
        return new Player(this, newPorperties : properties.SetProp(property, value));
    }
    public T? GetProp<T>(Prop<T> property) {
        return properties.GetProp(property);
    }
    public Player Continue() {
        return new Player(this, newPos : pos.Move(status));
    }
}
