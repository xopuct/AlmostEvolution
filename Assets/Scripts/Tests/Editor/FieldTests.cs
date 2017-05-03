using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Entitas;
using UnityEngine;

[TestFixture]
public class FieldTests : TestBase
{

    //[Test]
    //public void CreateEntityTest()
    //{
    //    var obj = context.CreateEntity();
    //    obj.AddPosition(2, 2);
    //    context.field.Move(obj, new Vector2i(0, 0));
    //    Assert.AreEqual(context.field.GetObjectInPos(new Vector2i(0, 0)), obj);
    //}

   

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
