using System.Collections.Immutable;

public class PropMap {
    private readonly ImmutableDictionary<string, object?> properties;
    public ImmutableDictionary<string, object?> Properties { get => properties; }
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
    public override bool Equals(object? obj) {
        if (obj is null || GetType() != obj.GetType()) {
            return false;
        } else {
            PropMap other = (PropMap)obj;
            if (properties.Keys.Count() == other.properties.Keys.Count()) {
                foreach (string key in properties.Keys) {
                    if (!other.properties.ContainsKey(key) || other.properties[key] != properties[key]) {
                        return false;
                    }
                }
                return true;
            } else {
                return false;
            }
        }
    }
    public override int GetHashCode() {
        int hash = 17;
        foreach (KeyValuePair<string, object?> pair in properties) {
            hash = hash * 23 + pair.Key.GetHashCode();
            if (pair.Value is not null) {
                hash = hash * 23 + pair.Value.GetHashCode();
            }
        }
        return hash;
    }
}
