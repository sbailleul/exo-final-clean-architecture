using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using TaskManger.Presentation.CLI;

namespace TaskManager.Tests;

public class CommandsTests
{
    [Test]
    public void JustTest()
    {
        var parser = new CommandParser();
        parser.Run(new string[]{"add"});
    }
}