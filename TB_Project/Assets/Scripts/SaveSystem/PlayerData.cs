using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float movespeed;

    public PlayerData(float movementSpeed) 
    {
        movespeed = movementSpeed;
    }
}
