using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum classType {tank, damageDealear, support };
[CreateAssetMenu(fileName = "Class", menuName = "Classes/NewClass", order = 1)]
public class Class : ScriptableObject
{
	[Header("BaseInfo")]
	public new string name;
	public classType type;
	public string description;

	[Header("BaseStats")]
	public int baseHP;
	public int baseInitiative;
	public int baseEvasion;
	public int baseAccuracy;

	[Header("WeaponProficiency")]
	public weaponType[] maintWeapon;
	public weaponType[] secondaryWeapon;

	[Header("Skills")]
	public Skill[] skills;
}
