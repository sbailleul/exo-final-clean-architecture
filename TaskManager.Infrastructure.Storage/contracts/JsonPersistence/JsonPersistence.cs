using System.Text.Json;
using TaskManager.Infrastructure.Storage.contracts.Persistence;

namespace TaskManager.Infrastructure.Storage.contracts.JsonPersistence;

public class JsonPersistence<T> : IPersistence<T> where T : IEntity
{
    private string _path;

    public JsonPersistence(string path)
    {
        if (!File.Exists(path))
        {
            initializePersistence(path);
        }

        _path = path;
    }

    private void initializePersistence(String path)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        using (StreamWriter sw = new StreamWriter(File.Open(path, FileMode.Append))) sw.Write("[]");
    }

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