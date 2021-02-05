using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Magazine", menuName = "Objects/NewMagazine", order = 2)]
public class MagazineBase : Item
{
    [Header("MagazineData")]
	public ammoType ammo;
	public magazineType magazine;
	public int ammoCapacity;
}

[System.Serializable]
public class Magazine
{
	public MagazineBase baseMagazine;
}