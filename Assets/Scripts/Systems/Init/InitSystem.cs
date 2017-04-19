﻿using Entitas;
using UnityEngine;

public sealed class InitSystem : IInitializeSystem
{
    readonly GameContext _context;
    StartConfiguration _config;

    public InitSystem(Contexts contexts, StartConfiguration config)
    {
        _context = contexts.game;
        _config = config;
    }

    public void Initialize()
    {
        foreach (var slot in _config.Slots)
        {
            var e = _context.CreateEntity();
            e.AddNewCell(slot.Cell.genome, 0, _config.StartEnergy, new Color(slot.Cell.red, slot.Cell.green, slot.Cell.blue), (int)slot.Cell.rot);
            e.AddPosition((int)slot.Position.x, (int)slot.Position.y);
        }
    }
}
