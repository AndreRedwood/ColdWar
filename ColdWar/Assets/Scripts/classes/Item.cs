using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
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
	private Sprite sprite;
	public Sprite Sprite
	{
		get { return sprite; }
	}
}
