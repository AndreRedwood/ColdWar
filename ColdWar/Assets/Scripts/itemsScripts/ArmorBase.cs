using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "Objects/NewArmor", order = 3)]
public class ArmorBase : Item
{
	[Header("ArmorData")]
	public int dogdeBonus;
	public int armorBonus;
}

[System.Serializable]
public class Armor
{
	public ArmorBase baseArmor;
}
