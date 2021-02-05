using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum unitStatus {PlayerUnit, AIUnit, Dead };
[System.Serializable]
public class Unit
{
	[Header("BasicInfo")]
	public string name;
	public Sprite portraiat;
	public int squadID;

	[Header("UnitData")]
	public unitStatus status;
	public Class unitClass;
	public int initiative;
	public int maxHP;
	public int evasion;
	public int accuracy;

	[Header("EquipmentData")]
	public Armor armor;
	public Weapon mainWeapon;
	public Weapon secondaryWeapon;
	public Magazine[] magazines;

	[Header("SkillsData")]
	public Skill[] skills;

	[Header("BattleData")]
    public Vector2Int position;
    public int actionsLeft;
	public int HP;
	public int actualInitiative;
	public bool moveDone;

	[Header("GraphicData")]
	public Sprite unitSprite;
}
