using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TerrainFeature
{
	[SerializeField]
	private string name;
	public string Name
	{
		get { return name; }
	}
	[SerializeField]
	private Sprite featureSprite;
	public Sprite FeatureSprite
	{
		get { return featureSprite; }
	}
	[SerializeField]
	private int cover;
	public int Cover
	{
		get { return cover; }
	}
}
