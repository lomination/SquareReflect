using System.Collections.Immutable;

public class PropMap {
    private readonly ImmutableDictionary<string, object?> properties;
    public ImmutableDictionary<string, object?> Properties {get => new Dictionary<string, object?>(properties).ToImmutableDictionary();}
    public PropMap() {
        properties = ImmutableDictionary.Create<string, object?>();
    }
    public PropMap(ImmutableDictionary<string, object?> properties) {
        this.properties = properties;
    }
    public PropMap SetProp<T>(Prop<T> property, T? value) {
        if (properties.ContainsKey(property.Name)) {
            Dictionary<string, object?> newPropertyMapProperties = new(properties) {
                [property.Name] = value
            };
            return new PropMap(newPropertyMapProperties.ToImmutableDictionary());
        } else {
            return new PropMap(properties.Add(property.Name, value));
        }
    }
    public T? GetProp<T>(Prop<T> property) {
        if (properties.ContainsKey(property.Name)) {
            return (T?)properties[property.Name];
        } else {
            return default;
        }
    }
}
