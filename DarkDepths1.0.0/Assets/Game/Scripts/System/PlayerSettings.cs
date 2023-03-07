using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSettings
{
    public int Health;
    public float Speed;
    public int TypeBomb;
    public int BombAllowed;
    public int FireLenght;

    public PlayerSettings(int health, float speed, int typeBomb, int bombAllowed, int fireLenght)
    {
        Health = health;
        Speed = speed;
        TypeBomb = typeBomb;
        BombAllowed = bombAllowed;
        FireLenght = fireLenght;
    }
}
    