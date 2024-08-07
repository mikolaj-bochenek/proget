namespace Proget.Messaging;

public interface ISerializer
{
    string Serialize<T>(T value);
    string Serialize(object value);
    T? Deserialize<T>(string value);
    object? Deserialize(string value, Type type);
    object? Deserialize(string value);
}
