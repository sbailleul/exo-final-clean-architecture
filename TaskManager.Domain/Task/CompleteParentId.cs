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

    public int GetLast()
    {
        var multipleIds = Value.Split(":").Select(int.Parse).ToList();
        return multipleIds.Last();
    }

    public bool Is(int comparedInt)
    {
        return comparedInt.ToString() == Value;
    }

    public bool IsEmpty() => Value.Length == 0;

    public CompleteParentId GetLastFullParentId()
    {
        var multipleIds = Value.Split(":").Select(int.Parse).ToList();

        if (multipleIds.Count() <= 1) return new CompleteParentId(string.Join(':', multipleIds));

        multipleIds.RemoveAt(multipleIds.Count - 1);
        return new CompleteParentId(string.Join(':', multipleIds));
    }

    public bool IsSoloId()
    {
        return GetFirstId().ToString().Equals(this.Value);
    }
};