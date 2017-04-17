using Entitas;


public class ViewSystems : Feature
{

    public ViewSystems(Contexts context)
    {
        Add(new AddViewSystem(context));
        Add(new RemoveViewSystem(context));
        Add(new RenderColorSystem(context));
        Add(new RenderPositionSystem(context));
    }
}
