﻿using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
public class Bot : MonoBehaviour
{
    public LayerMask side;
    public LayerMask wall;
    public LayerMask food;
    public LayerMask bot;


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
        Profiler.BeginSample("Get obstacles");
        cell.Obstacle = Field.Instance.GetObjectInPos(cell.Pos + cell.sensor);
        Profiler.EndSample();
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
            Profiler.BeginSample("Look");
            Look(cell);
            Profiler.EndSample();
            return;
        }
        // поворачивается
        else if (cell.genome[cell.controller] > 0 && cell.genome[cell.controller] < 8)
        {
            Turn(cell);
            return;
        }
        // жрёт
        else if (cell.genome[cell.controller] == 8)
        {
            Profiler.BeginSample("Look");
            Eat(cell);
            Profiler.EndSample();
            return;
        }
        // жрёт
        else if (cell.genome[cell.controller] == 9)
        {
            Profiler.BeginSample("Move");
            Move(cell);
            Profiler.EndSample();

            return;
        }
        // фотосинтез
        else if (cell.genome[cell.controller] == 10)
        {
            Synth(cell);
            return;
        }
        // проверяет здоровье
        else if (cell.genome[cell.controller] == 11)
        {
            CheckEnergy(cell);
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
        for (int i = cell.sensors.Length - 1; i >= 0; i--)
        {
            if (Field.Instance.IsFree(cell.Pos + cell.sensors[i]))
            {
                Divide(cell, cell.Pos + cell.sensors[i]);
                return;
            }
        }
        Die(cell);
    }

    void Divide(DNA cell, Vector2i dividePoint)
    {
        Registry.Instance.Add(dividePoint, cell);
        cell.energy /= 2;
    }

    void Look(DNA cell)
    {
        if (!cell.Obstacle)                                                  // Пусто
        {
            cell.controller++;
            return;
        }

        if (cell.Obstacle.layer == LayerMask.NameToLayer("wall"))    // стена
        {
            cell.controller += 2;
            return;
        }
        else if (cell.Obstacle.layer == LayerMask.NameToLayer("bot"))     // бот
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

        if (cell.Obstacle.layer == LayerMask.NameToLayer("bot"))     // бот
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
        if (!cell.Obstacle && Field.Instance.IsFree(cell.Pos + cell.sensor))                                                  // Пусто
        {
            cell.Pos = cell.Pos + cell.sensor;
            cell.controller++;
            return;
        }
        if (!cell.Obstacle)
        {
            cell.controller += 2;
            return;
        }
        else if (cell.Obstacle.gameObject.layer == LayerMask.NameToLayer("side"))         // Край
        {
            var newx = cell.Pos.x + cell.sensor.x;
            if (newx < 0) newx = 99;
            if (newx > 99) newx = 0;
            cell.Pos = new Vector2i(newx, cell.sensor.y);
            cell.controller++;
            return;
        }
        else if (cell.Obstacle.gameObject.layer == LayerMask.NameToLayer("bot"))     // бот
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
            cell.Pos = cell.Pos + cell.sensor;
            cell.ChangeColor(1, -1, -1);
            return;
        }

    }

    void Synth(DNA cell)
    {
        cell.energy += (int)Mathf.Round(cell.transform.position.y * LevelManager.Instance.SynthMultipler);
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
        int dismatches = 0;

        for (int i = 0; i < cell.genome.Length; i++)
        {
            if (dismatches < 2)
            {
                if (cell.genome[i] != Registry.Instance.Get(cell.Obstacle).genome[i]) dismatches++;
            }
            else return false;
        }
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
