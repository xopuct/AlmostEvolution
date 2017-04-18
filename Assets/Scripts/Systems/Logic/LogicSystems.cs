using Entitas;


public class LogicSystems : Feature
{

    public LogicSystems(Contexts context)
    {
        Add(new EnergySystem(context));
        Add(new DivideSystem(context));
        Add(new EatSystem(context));
        Add(new FieldSystem(context));
        Add(new LookSystem(context));
        Add(new MoveSystem(context));
        Add(new SynthSystem(context));
        Add(new TurnSystem(context));
        Add(new DeadSystem(context));
    }
}
