using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ammoType {_9x19_Parabellum, _7q62x54_R, _7q62x25_Tokarev};
public enum magazineType { _3x4_Stick, _3_Clip, _4x4_Drum };
public enum weaponType {SMG, AR, RF, HG, SG, MG, Grenade, Blade};
public enum itemType {Weapon, Armor, Magazine };
[System.Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "Objects/NewItem", order = 4)]
public class Item : ScriptableObject
{
	[Header("ItemData")]
    public new string name;
    public string description;
    public Sprite sprite;
	public itemType type;
}
