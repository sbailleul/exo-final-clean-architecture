using TaskManager.Domain.Pipeline;
using TaskManager.Domain.Time;

namespace TaskMananger.Infrastructure.Logging;

public class Logger : IMiddleware<string[], int>
{
    private readonly string _dstFileName;
    private readonly ITimeGenerator _timeGenerator;

    public Logger(string dstFileName, ITimeGenerator timeGenerator)
    {
        _dstFileName = dstFileName;
        _timeGenerator = timeGenerator;
    }

    public int Next(string[] req, Func<string[], int> func)
    {
        using var textWriter = File.AppendText(_dstFileName);
        try
        {
            var res = func.Invoke(req);
            textWriter.WriteLine($"[ok+][${_timeGenerator.Now()}]{string.Join(' ', req)}");
            return res;
        }
        catch (Exception e)
        {
            textWriter.WriteLine($"[err][${_timeGenerator.Now()}]{string.Join(' ', req)} : {e.Message}");
            throw;
        }
    }
}