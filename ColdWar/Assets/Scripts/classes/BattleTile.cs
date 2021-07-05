using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleTile
{
	[SerializeField]
	private TerrainFeature feature = null;
	public TerrainFeature Feature
	{
		set { Feature = value; }
		get { return feature; }
	}
}
