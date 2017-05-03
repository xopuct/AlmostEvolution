using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Entitas;

[TestFixture]
public class TestBase
{
    protected Contexts contexts;
    protected GameContext context;

    [SetUp]
    public void CreateContext()
    {
        contexts = new Contexts();
        Contexts.sharedInstance = contexts;
        //contexts.game.SetField(100, 60);
        context = contexts.game;
    }
}