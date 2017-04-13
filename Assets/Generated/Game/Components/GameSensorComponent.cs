//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public SensorComponent sensor { get { return (SensorComponent)GetComponent(GameComponentsLookup.Sensor); } }
    public bool hasSensor { get { return HasComponent(GameComponentsLookup.Sensor); } }

    public void AddSensor(Vector2i[] newSensors, float newRot) {
        var index = GameComponentsLookup.Sensor;
        var component = CreateComponent<SensorComponent>(index);
        component.sensors = newSensors;
        component.rot = newRot;
        AddComponent(index, component);
    }

    public void ReplaceSensor(Vector2i[] newSensors, float newRot) {
        var index = GameComponentsLookup.Sensor;
        var component = CreateComponent<SensorComponent>(index);
        component.sensors = newSensors;
        component.rot = newRot;
        ReplaceComponent(index, component);
    }

    public void RemoveSensor() {
        RemoveComponent(GameComponentsLookup.Sensor);
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

    static Entitas.IMatcher<GameEntity> _matcherSensor;

    public static Entitas.IMatcher<GameEntity> Sensor {
        get {
            if(_matcherSensor == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Sensor);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherSensor = matcher;
            }

            return _matcherSensor;
        }
    }
}
