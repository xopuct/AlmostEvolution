using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
public class Bot : MonoBehaviour
{

    int botLayer;
    int wallLayer;

    void Start()
    {
        botLayer = LayerMask.NameToLayer("bot");
        wallLayer = LayerMask.NameToLayer("wall");
    }

    // Use this for initialization
    void InitCell(DNA cell)
    {
        //genome = new int[64];
        cell.controller = 0;
        if (LevelManager.Instance.StartEnergy > 0) cell.energy = LevelManager.Instance.StartEnergy;
        if (Random.Range(0, 3) == 1)
        {
            LevelManager.Instance.mutations++;
            cell.genome[Random.Range(0, 63)] = Random.Range(0, 63);
        }
        cell.UpdateColor();

        //for (int i = 0; i < cell.genome.Length; i++)
        //{
        //genome[i] = Random.Range(0,63);
        //genome[i] = 10;
        //}

        cell.Inited = true;
    }


    public void Step(DNA cell)
    {
        UnityEngine.Profiling.Profiler.BeginSample("Get obstacles");
        var obstacle = Field.Instance.GetObjectInPos(cell.Pos + cell.sensor);
        if (obstacle)
            cell.Obstacle = obstacle.gameObject;
        else
            cell.Obstacle = null;
        UnityEngine.Profiling.Profiler.EndSample();
        //Profiler.BeginSample("Get neighbor");
        //colliders = Registry.Instance.GetInRadius(sensor.position, 0.1f);
        //Profiler.EndSample();
        if (cell.controller > 63) cell.controller -= 64;

        cell.energy--;

        if (cell.energy > LevelManager.Instance.EnergyToDivide)
        {
            TryToDivide(cell);
        }
        if (cell.energy <= 0)
        {
            Registry.Instance.Kill(cell.gameObject);
            return;
            //Debug.Log("starved");
        }
        // смотрит
        if (cell.genome[cell.controller] == 0)
        {
            UnityEngine.Profiling.Profiler.BeginSample("Look");
            Look(cell);
            UnityEngine.Profiling.Profiler.EndSample();
            return;
        }
        // поворачивается
        else if (cell.genome[cell.controller] > 0 && cell.genome[cell.controller] < 8)
        {
            UnityEngine.Profiling.Profiler.BeginSample("Turn");
            Turn(cell);
            UnityEngine.Profiling.Profiler.EndSample();
            return;
        }
        // жрёт
        else if (cell.genome[cell.controller] == 8)
        {
            UnityEngine.Profiling.Profiler.BeginSample("Look");
            Eat(cell);
            UnityEngine.Profiling.Profiler.EndSample();
            return;
        }
        // жрёт
        else if (cell.genome[cell.controller] == 9)
        {
            UnityEngine.Profiling.Profiler.BeginSample("Move");
            Move(cell);
            UnityEngine.Profiling.Profiler.EndSample();

            return;
        }
        // фотосинтез
        else if (cell.genome[cell.controller] == 10)
        {
            UnityEngine.Profiling.Profiler.BeginSample("Synth");
            Synth(cell);
            UnityEngine.Profiling.Profiler.EndSample();
            return;
        }
        // проверяет здоровье
        else if (cell.genome[cell.controller] == 11)
        {
            UnityEngine.Profiling.Profiler.BeginSample("Check Energy");
            CheckEnergy(cell);
            UnityEngine.Profiling.Profiler.EndSample();
            return;
        }
        // рожает
        else if (cell.genome[cell.controller] == 12)
        {
            if (cell.energy > LevelManager.Instance.EnergyToDivide)
            {
                TryToDivide(cell);
                return;
            }
        }
        // переход
        else if (cell.genome[cell.controller] > 12)
        {
            cell.controller += cell.genome[cell.controller];
            return;
        }

    }

    void TryToDivide(DNA cell)
    {
        UnityEngine.Profiling.Profiler.BeginSample("Try to Divide");
        for (int i = cell.sensors.Length - 1; i >= 0; i--)
        {
            if (Field.Instance.IsFree(cell.Pos + cell.sensors[i]))
            {
                Divide(cell, cell.Pos + cell.sensors[i]);
                UnityEngine.Profiling.Profiler.EndSample();
                return;
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();
    }

    void Divide(DNA cell, Vector2i dividePoint)
    {
        cell.energy /= 2;
        Registry.Instance.Add(dividePoint, cell);
    }

    void Look(DNA cell)
    {
        if (!cell.Obstacle)                                                  // Пусто
        {
            cell.controller++;
            return;
        }

        if (cell.Obstacle.layer == wallLayer)    // стена
        {
            cell.controller += 2;
            return;
        }
        else if (cell.Obstacle.layer == botLayer)     // бот
        {
            if (Registry.Instance.Get(cell.Obstacle.gameObject).Dead)
            {
                cell.controller += 3;
            }
            else
            {
                if (CheckRelations(cell)) cell.controller += 4;
                else cell.controller += 5;
            }
            return;
        }
    }

    void Turn(DNA cell)
    {
        cell.rot += 45 * cell.genome[cell.controller];
        if (cell.rot >= 360) cell.rot -= 360;
        // cell.transform.rotation = Quaternion.Euler(0, 0, cell.rot);
        cell.controller++;
    }

    void Eat(DNA cell)
    {
        if (!cell.Obstacle)                                                  // Пусто
        {
            cell.controller++;
            return;
        }

        if (cell.Obstacle.layer == botLayer)     // бот
        {
            var targetCalories = Registry.Instance.Get(cell.Obstacle).energy;

            if (Registry.Instance.Get(cell.Obstacle).Dead)
            {
                cell.energy += targetCalories;
                cell.controller += 3;
            }
            else
            {
                if (CheckRelations(cell))
                {
                    cell.controller += 4;
                    cell.energy += targetCalories / 2;
                }
                else
                {
                    cell.controller += 5;
                    cell.energy += targetCalories;
                }
            }

            Registry.Instance.Remove(cell.Obstacle.gameObject);
            cell.ChangeColor(1, -1, -1);

            return;
        }
    }

    void Move(DNA cell)
    {
        var targetPos = cell.Pos + cell.sensor;
        targetPos.x = (int)Mathf.Repeat(targetPos.x, Field.Instance.Width);
        if (!cell.Obstacle && Field.Instance.IsFree(targetPos))                                                  // Пусто
        {
            cell.Pos = targetPos;
            cell.controller++;
            return;
        }
        if (!cell.Obstacle)
        {
            cell.controller += 2;
            return;
        }
        else if (cell.Obstacle.gameObject.layer == botLayer)     // бот
        {
            var targetCalories = Registry.Instance.Get(cell.Obstacle).energy;
            if (Registry.Instance.Get(cell.Obstacle.gameObject).Dead)
            {
                cell.energy += targetCalories;
                cell.controller += 3;
            }
            else
            {
                if (CheckRelations(cell))
                {
                    cell.controller += 4;
                    cell.energy += targetCalories / 2;
                }
                else
                {
                    cell.controller += 5;
                    cell.energy += targetCalories;
                }
            }

            Registry.Instance.Remove(cell.Obstacle.gameObject);
            cell.Pos = targetPos;
            cell.ChangeColor(1, -1, -1);
            return;
        }

    }

    void Synth(DNA cell)
    {
        cell.energy += (int)(cell.Pos.y * LevelManager.Instance.SynthMultipler);
        cell.controller++;

        cell.ChangeColor(-0.1f, 0.1f, -0.1f);
    }

    void CheckEnergy(DNA cell)
    {
        int cnt = cell.controller + 1;
        if (cnt > 63) cnt -= 64;
        if (cell.energy < cell.genome[cnt] * 15)
        {
            cell.controller += 2;
        }
        if (cell.energy >= cell.genome[cnt] * 15)
        {
            cell.controller += 3;
        }
    }

    public void Die(DNA cell)
    {
        Registry.Instance.Kill(cell.gameObject);
    }

    bool CheckRelations(DNA cell)
    {
        UnityEngine.Profiling.Profiler.BeginSample("Check Relations");
        int dismatches = 0;

        for (int i = 0; i < cell.genome.Length; i++)
        {
            if (dismatches < 2)
            {
                if (cell.genome[i] != Registry.Instance.Get(cell.Obstacle).genome[i]) dismatches++;
            }
            else
            {
                UnityEngine.Profiling.Profiler.EndSample();
                return false;
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();
        return true;
    }

    void Update()
    {
        foreach (var cell in Registry.Instance.GetAllCellsDNA())
        {
            if (cell && !cell.Dead)
            {
                if (!cell.Inited)
                    InitCell(cell);
                else
                    Step(cell);
            }
        }
    }
}
