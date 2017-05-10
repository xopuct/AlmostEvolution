using Entitas;


public class LogicSystems : Feature
{

    public LogicSystems(Contexts contexts)
    {
        Add(new EnergySystem(contexts));
        Add(new DivideSystem(contexts));
        Add(new LookSystem(contexts));
        Add(new TurnSystem(contexts));
        Add(new EatSystem(contexts));
        Add(new MoveSystem(contexts));
        Add(new SynthSystem(contexts));
        Add(new DeadSystem(contexts));
        Add(new UpdateControllerSystem(contexts));
    }
}
