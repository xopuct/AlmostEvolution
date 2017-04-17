using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

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


    protected void Start()
    {
        Application.runInBackground = true;

        //if (Configuration)
        //    Configuration.Place();
        //else
        //    Debug.LogError("Please setup cell configuration");

        var contexts = Contexts.sharedInstance;
        //contexts.SetAllContexts();

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
        //cellsCountText.text = Registry.Instance.GetCellsCount().ToString();
        //corpseCountText.text = Registry.Instance.GetCorpsesCount().ToString();
    }
}
