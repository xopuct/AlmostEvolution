using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Entitas;
using UnityEngine;

[TestFixture]
public class FieldTests : TestBase
{

    [Test]
    public void CreateEntityTest()
    {
        var obj = context.CreateEntity();
        obj.AddPosition(2, 2);
        context.field.Move(obj, new Vector2i(0, 0));
        Assert.AreEqual(context.field.GetObjectInPos(new Vector2i(0, 0)), obj);
    }

    [Test]
    public void MoveTest()
    {
        var obj = context.CreateEntity();
        obj.AddPosition(2, 2);
        context.field.Move(obj, new Vector2i(0, 0));
        Assert.AreEqual(new Vector2i(0, 0), new Vector2i(obj.position.X, obj.position.Y));
    }


    [Test]
    public void Move2Test()
    {
        var obj = context.CreateEntity();
        obj.AddPosition(2, 2);
        context.field.Move(obj, new Vector2i(0, 0));
        context.field.Move(obj, new Vector2i(0, 1));

        Assert.AreEqual(context.field.GetObjectInPos(new Vector2i(0, 0)), null);
    }



    [Test]
    public void MoveToLockedPointTest()
    {
        var obj1 = context.CreateEntity();
        obj1.AddPosition(0, 0);
        var obj2 = context.CreateEntity();
        obj2.AddPosition(1, 0);

        context.field.Move(obj1, new Vector2i(0, 0));
        context.field.Move(obj2, new Vector2i(0, 0));

        Assert.AreEqual(context.field.GetObjectInPos(new Vector2i(0, 0)), obj1);
    }

    [Test]
    public void ClearTest()
    {
        var obj1 = context.CreateEntity();
        obj1.AddPosition(0, 0);

        context.field.Move(obj1, new Vector2i(0, 0));
        context.field.Clear(new Vector2i(0, 0));
        Assert.AreEqual(context.field.GetObjectInPos(new Vector2i(0, 0)), null);
    }

    [Test]
    public void MoveToOut()
    {
        Assert.AreEqual(false, context.field.IsFree(new Vector2i(-1, 0)));
        Assert.AreEqual(false, context.field.IsFree(new Vector2i(0, -1)));
        Assert.AreEqual(false, context.field.IsFree(new Vector2i(-1, -1)));

        var width = context.field.Width;
        var height = context.field.Height;

        Assert.AreEqual(false, context.field.IsFree(new Vector2i(width, 5)));
        Assert.AreEqual(false, context.field.IsFree(new Vector2i(5, height)));
        Assert.AreEqual(false, context.field.IsFree(new Vector2i(width, height)));
    }

    //[Test]
    //public void ProjectileCollision()
    //{
    //    var system = new CollisionDamageSystem(contexts);

    //    var enemy = context.CreateEntity();
    //    enemy.AddEnemy(new EnemyType() { Type = FormationEnemyType.Common });
    //    enemy.AddHp(10, 10);

    //    var proj = context.CreateEntity();
    //    proj.isProjectile = true;
    //    proj.AddDamage(10);
    //    proj.AddCollision(enemy, Vector3.zero);

    //    system.Execute();

    //    Assert.AreEqual(new BigNum(10), enemy.receivedDamage.value);
    //}
}
