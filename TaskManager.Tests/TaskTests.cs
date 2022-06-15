using System;
using System.Collections.Generic;
using NUnit.Framework;
using TaskManager.Domain.Task;
using TaskManager.Domain.Task.dtos;

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
        t1 = Task.CreateNew(new TaskId(1), null, "desc", null, DateTime.Now, null, null);
        t11 = Task.CreateNew(new TaskId(11), null, "desc", null, DateTime.Now, null, null);
        t12 = Task.CreateNew(new TaskId(12), null, "desc", null, DateTime.Now, null, null);
        t121 = Task.CreateNew(new TaskId(121), null, "desc", null, DateTime.Now, null, null);
        t1.AddChild(t11, new CompleteParentId("1"));
        t1.AddChild(t12, new CompleteParentId("1"));
        t1.AddChild(t121, new CompleteParentId("1:12"));

    }

    [Test]
    public void DefaultTaskShouldBeTodo()
    {

        Task sut = Task.CreateNew(new TaskId(1), null, "desc", null, DateTime.Now, null, null);

        Assert.AreEqual("Todo", sut.State.Value);
    }

    [Test]
    public void TaskShouldBeAbleToFindChildrenRecursively()
    {
        var sut1 = t1.GetChild(new CompleteParentId("12:121"));
        var sut2 = t1.GetChild(new CompleteParentId("11"));
        var sut3 = t1.GetChild(new CompleteParentId("12:222:333"));
        
        Assert.NotNull(sut1);
        Assert.AreEqual(121, sut1!.Id.Value);
        
        
        Assert.NotNull(sut2);
        Assert.AreEqual(11, sut2!.Id.Value);
        
        Assert.IsNull(sut3);
        
    }

    [Test]
    public void TaskChangedToClosedShouldHaveClosedDate()
    {
        Task sut = Task.CreateNew(new TaskId(1), null, "desc", null, DateTime.Now, null, null);
        var updateDto = new UpdateTaskCommandDto("1", null, null, null, "Closed");
        var currentDate = DateTime.Now;
        sut.Update(updateDto, currentDate);
        Assert.NotNull(sut.CloseDate);
        Assert.AreEqual(currentDate, sut.CloseDate);
        Assert.AreEqual("Closed", sut.State.Value);
    }
}