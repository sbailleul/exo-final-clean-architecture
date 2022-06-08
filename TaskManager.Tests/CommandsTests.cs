using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using TaskManger.Presentation.CLI;

namespace TaskManager.Tests;

public class CommandsTests
{
    [Test]
    public void BullShitTest()
    {
        var parser = new CommandParser();
        parser.Run(new string[]{"add", "-p", "123:45", "-d","2022-04-01"});
    }
}