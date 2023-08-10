public class Prop<T> {
    private readonly string name;
    public string Name {get => this.name;}
    public Prop(string name) {
        this.name = name;
    }
}
