using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitType
{
	[SerializeField]
	private string name;
	public string Name
	{
		get { return name; }
	}
	[SerializeField]
	private string description;
	public string Description
	{
		get { return description; }
	}
	[SerializeField]
	private WeaponType[] permittedWeaponTypes;
	public WeaponType[] PermittedWeaponTypes
	{
		get { return permittedWeaponTypes; }
	}
	[SerializeField]
	private string startingWeaponName;
	public string StartingWeaponName
	{
		get { return startingWeaponName; }
	}
	[SerializeField]
	private Sprite baseSprite;

	[SerializeField]
	private UnitStats baseStats;
	public UnitStats BaseStats
	{
		get { return baseStats; }
	}
}
