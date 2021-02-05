using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Objects/NewWeapon", order = 1)]
public class WeaponBase : Item
{
	[Header("WeaponData")]
	public weaponType weaponType;
	public int damage;
	public int shoots;
	public int neededSkill;
	public int weaponAccuracyBonus;
	public ammoType ammo;
	public magazineType magazine;
	public Sprite magazineSprite;
}

[System.Serializable]
public class Weapon
{
	public int weaponAmmo;
	public WeaponBase baseWeapon;
}