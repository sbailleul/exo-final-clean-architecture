using System.Text.Json;
using TaskManager.Infrastructure.Storage.contracts.Persistence;

namespace TaskManager.Infrastructure.Storage.contracts.JsonPersistence;

public class JsonPersistence<T> : IPersistence<T> where T : IEntity
{
    private string _path = "/Users/simonhalimi/Library/Application Support/JetBrains/Rider2021.3/scratches/test.json";

    public IEnumerable<T> FindAll()
    {
        string text = File.ReadAllText(_path);
        return JsonSerializer.Deserialize<IEnumerable<T>>(text) ?? Array.Empty<T>();
    }

    public T? Find(int id)
    {
        string text = File.ReadAllText(_path);
        return (JsonSerializer.Deserialize<IEnumerable<T>>(text) ?? Array.Empty<T>())
            .FirstOrDefault(entity => entity.GetId() == id);
    }

    public void Set(T toWrite)
    {
        string rawJson = File.ReadAllText(_path);
        var entities = JsonSerializer.Deserialize<IEnumerable<T>>(rawJson) ?? Array.Empty<T>();

        if (entities.Any(entity => entity.GetId() == toWrite.GetId()))
        {
            entities = entities.Select(entity => entity.GetId() == toWrite.GetId() ? toWrite : entity);
        }
        else
        {
            entities = entities.Append(toWrite);
        }

        File.WriteAllText(_path, JsonSerializer.Serialize(entities));
    }

    public void Delete(int id)
    {
        string rawJson = File.ReadAllText(_path);
        var entities = JsonSerializer.Deserialize<IEnumerable<T>>(rawJson) ?? Array.Empty<T>();

        if (entities.Any(entity => entity.GetId() == id))
        {
            entities = entities.Where(entities => entities.GetId() != id);
        }

        File.WriteAllText(_path, JsonSerializer.Serialize(entities));
    }
}