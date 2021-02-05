using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum skillType {attack, attackRow, selfBuff};
[System.Serializable]
[CreateAssetMenu(fileName = "Skill", menuName = "Skills/NewSkill", order = 1)]
public class Skill : ScriptableObject
{
    public string skillName;
    public Sprite icon;
    public skillType type;

	public bool unlocked;

    public Vector2Int[] targetsCells;

    [Space(20,order =0)]
    [Header("AttackTypeSkill",order =1)]
    public float damageMultiplayer;
    public int attackCount;
    public float accuracyMultiplayer;

    [Space(20, order = 0)]
    [Header("BuffTypeSkill", order = 1)]
    public string buffedStat;
    public float buffMultiplayer;
    public int buffLasting;

    public int actionCost;
}
