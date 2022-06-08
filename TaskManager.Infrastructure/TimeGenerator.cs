using TaskManager.Domain.Time;

namespace TaskManager.Infrastructure;

public class TimeGenerator: ITimeGenerator
{
    public DateTime Now() => DateTime.Now;
}