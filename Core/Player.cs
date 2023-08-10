public class Player {
    private readonly Position pos;
    public Position Pos {get => this.pos;}
    private readonly Status status;
    public Status Status {get => this.status;}
    private readonly Tile behaviour;
    public Tile Behaviour {get => this.behaviour.Clone();}
    private readonly int numOfMoves;
    public int NumOfMoves {get => this.numOfMoves;}
    private readonly PropMap properties;
    public PropMap Properties {get => this.properties;}
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
        if (newNumOfMoves is int moves) {
            this.numOfMoves = moves;
        } else {
            this.numOfMoves = oldPlayer.NumOfMoves;
        }
        if (newPorperties is PropMap properties) {
            this.properties = properties;
        } else {
            this.properties = oldPlayer.Properties;
        }
        
    }
    public Player SetProp<T>(Prop<T> property, T value) {
        return new Player(this, newPorperties : properties.SetProp(property, value));
    }
    public T? GetProp<T>(Prop<T> property) {
        return properties.GetProp(property);
    }
    public Player Continue() {
        if (status == Status.IsGoingUp) {
            return new Player(this, newPos : pos.Move(0, -1));
        } else if (status == Status.IsGoingRight) {
            return new Player(this, newPos : pos.Move(1, 0));
        } else if (status == Status.IsGoingDown) {
            return new Player(this, newPos : pos.Move(0, 1));
        } else if (status == Status.IsGoingLeft) {
            return new Player(this, newPos : pos.Move(-1, 0));
        } else {
            return this;
        }
    }
}
