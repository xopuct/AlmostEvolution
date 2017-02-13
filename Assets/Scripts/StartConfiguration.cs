using UnityEngine;
using System;
using System.Collections.Generic;

public class StartConfiguration : MonoBehaviour
{
    public int StartEnergy = 50;

    public float SynthMultipler = 1;

    public int EnergyToDivide = 1000;

    public float CorpseEnergyReduction = 0.99f;

    public int LowLevelCorpseEnergy = 200;

    public int MinCorpseEnergy = 10;

    [Serializable]
    public struct Slot
    {
        public Vector2 Position;
        public DNA Cell;
    }

    public List<Slot> Slots;
    public string ProductedCellName = "Cell";


    public void Place()
    {
        foreach (var slot in Slots)
        {
            Registry.Instance.Add(new Vector2i(Mathf.RoundToInt(slot.Position.x), Mathf.RoundToInt(slot.Position.y)), slot.Cell).name = ProductedCellName;
            //var go = Instantiate(slot.Cell, slot.Position, Quaternion.identity);
        }
    }
}
