using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public new string name;
    public Sprite portraiat;

    public bool isAIControlled;
    public bool isDead;

    public Vector2Int position;

    public int actionsLeft;

    public int initiative;
    public int baseIntiative;

    public int HP;
    public int maxHP;

    public int evasion;
    public int accuracy;

    public Item armor;
    public Item mainWeapon;
    public int mainWeaponAmmo;
    public int mainWeaponMagazines;
    public Item secondaryWeapon;
    public int secondayWeaponAmmo;

    public Skill[] skills;
}
