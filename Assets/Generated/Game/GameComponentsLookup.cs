//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentsLookupGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public static class GameComponentsLookup {

    public const int Cell = 0;
    public const int Color = 1;
    public const int Corpse = 2;
    public const int Destroyed = 3;
    public const int Dividing = 4;
    public const int Killer = 5;
    public const int NewCell = 6;
    public const int Position = 7;
    public const int Sensor = 8;
    public const int View = 9;

    public const int TotalComponents = 10;

    public static readonly string[] componentNames = {
        "Cell",
        "Color",
        "Corpse",
        "Destroyed",
        "Dividing",
        "Killer",
        "NewCell",
        "Position",
        "Sensor",
        "View"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(CellComponent),
        typeof(ColorComponent),
        typeof(CorpseComponent),
        typeof(DestroyedComponent),
        typeof(DividingComponent),
        typeof(KillerComponent),
        typeof(NewCellComponent),
        typeof(PositionComponent),
        typeof(SensorComponent),
        typeof(ViewComponent)
    };
}
