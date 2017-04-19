
using NUnit.Framework;
using Entitas;

[TestFixture]
public class MoveTests : TestBase
{

    GameEntity CreateCell(int basicGenomeValue)
    {
        var obj = context.CreateEntity();
        obj.AddPosition(2, 2);
        int[] genome = new int[64];
        for (int i = 0; i < 64; i++)
            genome[i] = basicGenomeValue;

        obj.AddCell(genome, 1000, 0);
        obj.AddSensor(SensorHelper.GetSensorValue(), 359);

        return obj;
    }

    [Test]
    public void SimpleMoveTest()
    {
        var obj = CreateCell(9);
        var dir = obj.sensor.sensor;
        var initialPosition = obj.position;
        var system = new MoveSystem(contexts);
        system.Execute();
        Assert.AreEqual(initialPosition + dir, obj.position.ToVector2i());
        Assert.AreEqual(obj.cell.controller + 1, obj.changeController.newControllerValue);
    }
}
