using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemClass
{
    public enum WeaponType
    {
        offensive,
        defensive
    };

    public string item;
    public int ID;
    public string name;
    public int HP;
    public WeaponType type;
    public int ATK;
}
