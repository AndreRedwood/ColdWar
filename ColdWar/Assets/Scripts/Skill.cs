using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum skillType {attack, attackRow, selfBuff};
[System.Serializable]
public class Skill
{
    public string name;
    public Sprite icon;
    public skillType type;


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
