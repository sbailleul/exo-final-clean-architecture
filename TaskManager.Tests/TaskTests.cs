using System;
using System.Collections.Generic;
using NUnit.Framework;
using TaskManager.Domain.Task;

namespace TaskManager.Tests;

public class TaskTests
{
    private Task t121;
    private Task t12;
    private Task t11;
    private Task t1;
    
    [SetUp]
    public void Setup()
    {
        t121 = Task.CreateNew(new TaskId(121), null, "desc", null, DateTime.Now, null, null, null, null);
        t12 = Task.CreateNew(new TaskId(12), null, "desc", null, DateTime.Now, null, null, new List<Task> { t121 }, null);
        t11 = Task.CreateNew(new TaskId(11), null, "desc", null, DateTime.Now, null, null, null, null);
        t1 = Task.CreateNew(new TaskId(1), null, "desc", null, DateTime.Now, null, null,new List<Task>{t11, t12 }, null);

    }

    [Test]
    public void DefaultTaskShouldBeTodo()
    {

        Task sut = Task.CreateNew(new TaskId(1), null, "desc", null, DateTime.Now, null, null, null, null);

        Assert.AreEqual(TaskState.Todo, sut.State.Value);
    }

    [Test]
    public void TaskShouldBeAbleToFindChildrenRecursively()
    {
        var sut1 = t1.GetChildren("12:121");
        var sut2 = t1.GetChildren("11");
        var sut3 = t1.GetChildren("12:222:333");
        
        Assert.NotNull(sut1);
        Assert.AreEqual(121, sut1!.Id.Value);
        
        
        Assert.NotNull(sut2);
        Assert.AreEqual(11, sut2!.Id.Value);
        
        Assert.IsNull(sut3);
        
    }
}