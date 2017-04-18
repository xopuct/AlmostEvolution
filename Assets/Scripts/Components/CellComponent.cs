
using Entitas;
using UnityEngine;


public class CellComponent : IComponent
{
    public int[] genome;
    public int energy;

    public int controller;

    public int CurrentGene { get { return genome[controller % genome.Length]; } }

}
