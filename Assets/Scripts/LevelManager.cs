using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;
using Entitas.VisualDebugging.Unity;

public class LevelManager : Singleton<LevelManager>
{

    public int StartEnergy { get { return Configuration.StartEnergy; } }

    public float SynthMultipler { get { return Configuration.SynthMultipler; } }

    public int EnergyToDivide { get { return Configuration.EnergyToDivide; } }

    public float CorpseEnergyReduction { get { return Configuration.CorpseEnergyReduction; } }
    public int LowLevelCorpseEnergy { get { return Configuration.LowLevelCorpseEnergy; } }
    public int MinCorpseEnergy { get { return Configuration.MinCorpseEnergy; } }

    public int mutations;
    public TextMesh mutationsText;
    public TextMesh fpsText;
    public TextMesh cellsCountText;
    public TextMesh corpseCountText;

    public StartConfiguration Configuration;

    Systems _systems;
    private Contexts contexts;

    protected void Start()
    {
        Application.runInBackground = true;

        //if (Configuration)
        //    Configuration.Place();
        //else
        //    Debug.LogError("Please setup cell configuration");

        contexts = Contexts.sharedInstance;
        //#if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
        //        var contextObserver = new ContextObserver(contexts.game);
        //        Object.DontDestroyOnLoad(contextObserver.gameObject);
        //#endif
        //contexts.SetAllContexts();
        contexts.game.SetField(100, 60);
        _systems = createSystems(contexts);
        _systems.Initialize();
    }

    Systems createSystems(Contexts contexts)
    {
        return new Feature("Systems")
            // Initialize
            .Add(new InitSystem(contexts, Configuration))

            //// Logic
            .Add(new LogicSystems(contexts))
            .Add(new CellInitSystem(contexts))
            //// Update
            //.Add(new GameSystems(contexts))

            //// Render
            .Add(new ViewSystems(contexts))

            //// Destroy
            .Add(new DestroySystem(contexts));
    }

    // Update is called once per frame
    void Update()
    {
        _systems.Execute();

        mutationsText.text = mutations.ToString();
        fpsText.text = (1 / Time.deltaTime).ToString();
        var dead = contexts.game.GetGroup(GameMatcher.Corpse).count;
        cellsCountText.text = (contexts.game.GetGroup(GameMatcher.Cell).count - dead).ToString();
        corpseCountText.text = dead.ToString();
    }
}
