using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ammoType
{
	_pistolAmmo,
	_rifleAmmo
}

[System.Serializable]
public class Weapon : Item
{
	[SerializeField]
	private WeaponType weaponType;
	public WeaponType WeaponType
	{
		get { return weaponType; }
	}
	[SerializeField]
	private ammoType ammoType;
	public ammoType AmmoType
	{
		get { return ammoType; }
	}
	[SerializeField]
	private int magazineCapacity;
	public int MagazineCapacity
	{
		get { return magazineAmmo; }
	}
	[SerializeField]
	private int magazineAmmo;
	public int MagazineAmmo
	{
		get { return magazineAmmo; }
		set { magazineAmmo = value; }
	}
	[SerializeField]
	private int burstCount;
	public int BursCount
	{
		get { return burstCount; }
	}
	[SerializeField]
	private int baseDamage;
	public int BaseDamage
	{
		get { return baseDamage; }
	}
	[SerializeField]
	private int accuracyModifier;
	public int AccuracyModifier
	{
		get { return accuracyModifier; }
	}
}
