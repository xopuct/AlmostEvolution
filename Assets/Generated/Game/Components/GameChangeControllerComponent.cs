//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ChangeControllerComponent changeController { get { return (ChangeControllerComponent)GetComponent(GameComponentsLookup.ChangeController); } }
    public bool hasChangeController { get { return HasComponent(GameComponentsLookup.ChangeController); } }

    public void AddChangeController(int newNewControllerValue) {
        var index = GameComponentsLookup.ChangeController;
        var component = CreateComponent<ChangeControllerComponent>(index);
        component.newControllerValue = newNewControllerValue;
        AddComponent(index, component);
    }

    public void ReplaceChangeController(int newNewControllerValue) {
        var index = GameComponentsLookup.ChangeController;
        var component = CreateComponent<ChangeControllerComponent>(index);
        component.newControllerValue = newNewControllerValue;
        ReplaceComponent(index, component);
    }

    public void RemoveChangeController() {
        RemoveComponent(GameComponentsLookup.ChangeController);
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

    static Entitas.IMatcher<GameEntity> _matcherChangeController;

    public static Entitas.IMatcher<GameEntity> ChangeController {
        get {
            if(_matcherChangeController == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ChangeController);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherChangeController = matcher;
            }

            return _matcherChangeController;
        }
    }
}
