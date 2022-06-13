namespace TaskManager.Domain.Task;

public record CompleteParentId(string Value)
{
    public int GetFirstId()
    {
        return Value.Split(":").Select(int.Parse).ToList()[0];
    }

    public CompleteParentId GetChildrenIds()
    {
        var multipleIds = Value.Split(":").Select(int.Parse).ToList();
        multipleIds.RemoveAt(0);
        return new CompleteParentId(string.Join(':', multipleIds));
    }

    public bool IsEmpty() => Value.Length == 0;
};