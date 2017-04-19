//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public NewCellComponent newCell { get { return (NewCellComponent)GetComponent(GameComponentsLookup.NewCell); } }
    public bool hasNewCell { get { return HasComponent(GameComponentsLookup.NewCell); } }

    public void AddNewCell(int[] newGenome, int newController, int newEnergy, UnityEngine.Color newColor, int newRot) {
        var index = GameComponentsLookup.NewCell;
        var component = CreateComponent<NewCellComponent>(index);
        component.genome = newGenome;
        component.controller = newController;
        component.energy = newEnergy;
        component.color = newColor;
        component.rot = newRot;
        AddComponent(index, component);
    }

    public void ReplaceNewCell(int[] newGenome, int newController, int newEnergy, UnityEngine.Color newColor, int newRot) {
        var index = GameComponentsLookup.NewCell;
        var component = CreateComponent<NewCellComponent>(index);
        component.genome = newGenome;
        component.controller = newController;
        component.energy = newEnergy;
        component.color = newColor;
        component.rot = newRot;
        ReplaceComponent(index, component);
    }

    public void RemoveNewCell() {
        RemoveComponent(GameComponentsLookup.NewCell);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherNewCell;

    public static Entitas.IMatcher<GameEntity> NewCell {
        get {
            if(_matcherNewCell == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.NewCell);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherNewCell = matcher;
            }

            return _matcherNewCell;
        }
    }
}
