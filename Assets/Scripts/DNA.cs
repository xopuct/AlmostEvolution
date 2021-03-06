﻿

using System.Collections.Generic;
using UnityEngine;


public class DNA : MonoBehaviour
{
    public Vector2i[] sensors;


    public Vector2i sensor
    {
        get
        {
            var index = (int)(rot / 45f);
            return sensors[index];
        }

    }

    public GameObject Obstacle;
    public Vector3 ObstaclePos;
    [Space()]
    public int[] genome;
    public int energy;

    [Space()]
    public float red;
    public float green;
    public float blue;


    public SpriteRenderer Color;
    public int controller;

    public float rot;

    public bool Dead = false;

    [HideInInspector]
    public bool Inited = false;

    public Sprite AliveSprite;
    public Sprite DeadSprite;
    public Color DeadColor;

    public Vector2i Pos
    {
        get { return Field.Instance.GetPosition(gameObject); }
        set { Field.Instance.SetPosition(gameObject, value); }
    }

    public void ChangeColor(float deltaR, float deltaG, float deltaB)
    {
        Profiler.BeginSample("Change color");
        red = Mathf.Clamp01(red + deltaR);
        green = Mathf.Clamp01(green + deltaG);
        blue = Mathf.Clamp01(blue + deltaB);
        UpdateColor();
        Profiler.EndSample();
    }

    public void UpdateColor()
    {
        if (!Dead)
            Color.color = new Color(red, green, blue);
    }

    public void Die()
    {
        Color.sprite = DeadSprite;
        Color.color = DeadColor;
        Dead = true;
    }

}
