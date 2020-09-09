using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ammoType {_9x19_Parabellum, _7q62x54_R};
public enum itemType {armor, mainWeapon};
public enum weaponType {SMG, RF};
[CreateAssetMenu(fileName = "Item", menuName = "Objects/NewItem", order = 1)]
public class Item : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite sprite;

    public itemType itemType;

    public int dogdeBonus;
    public int armorBonus;

    [Space(20,order = 0)]
    [Header("WeaponTypeItem", order = 1)]
    public weaponType weaponType;
    public int damage;
    public int shoots;
    public int ammoCapacity;
    public int neededSkill;
    public int weaponAccuracyBonus;
    public ammoType ammo;
    public magazineType magazine;
    public Sprite magazineSprite;
}
