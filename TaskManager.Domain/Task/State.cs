namespace TaskManager.Domain.Task;




public record State(string Value)
{
    public static State Todo() => new State("Todo");
    public static State Pending() => new State("Pending");
    public static State Progress() => new State("Progress");
    public static State Done() => new State("Done");
    public static State Cancelled() => new State("Cancelled");
    public static State Closed() => new State("Closed");
}