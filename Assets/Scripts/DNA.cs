

using UnityEngine;


public class DNA : MonoBehaviour
{
    public Transform[] sensors;
    public Transform sensor;

    public Collider2D Obstacle;
    public Vector3 ObstaclePos;
    [Space()]
    public int[] genome;
    public int energy;

    [Space()]
    public float red;
    public float green;
    public float blue;


    public SpriteRenderer Color;
    public Collider2D[] colliders;
    public int controller;

    public float rot;

    public bool Dead = false;

    [HideInInspector]
    public bool Inited = false;

    public Sprite AliveSprite;
    public Sprite DeadSprite;
    public Color DeadColor;
    public void ChangeColor(float deltaR, float deltaG, float deltaB)
    {
        red = Mathf.Clamp01(red + deltaR);
        green = Mathf.Clamp01(green + deltaG);
        blue = Mathf.Clamp01(blue + deltaB);
        UpdateColor();
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
