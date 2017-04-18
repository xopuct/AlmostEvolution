//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly CorpseComponent corpseComponent = new CorpseComponent();

    public bool isCorpse {
        get { return HasComponent(GameComponentsLookup.Corpse); }
        set {
            if(value != isCorpse) {
                if(value) {
                    AddComponent(GameComponentsLookup.Corpse, corpseComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.Corpse);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherCorpse;

    public static Entitas.IMatcher<GameEntity> Corpse {
        get {
            if(_matcherCorpse == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Corpse);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCorpse = matcher;
            }

            return _matcherCorpse;
        }
    }
}