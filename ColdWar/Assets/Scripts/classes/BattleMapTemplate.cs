using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleMapTemplate
{
	[SerializeField]
	private string backgroundImageName;
	public string BackgroundImageName
	{
		get { return backgroundImageName; }
	}
	[SerializeField]
	private BattleMapTemplateFeature[] terrainFeatures;
	public BattleMapTemplateFeature[] TerrainFeatures
	{
		get { return terrainFeatures; }
	}

	[SerializeField]
	private int[] row1Features;
	public int[] Row1Features
	{
		get { return row1Features; }
	}
	[SerializeField]
	private int[] row2Features;
	public int[] Row2Features
	{
		get { return row2Features; }
	}
	[SerializeField]
	private int[] row3Features;
	public int[] Row3Features
	{
		get { return row3Features; }
	}
	[SerializeField]
	private int[] row4Features;
	public int[] Row4Features
	{
		get { return row4Features; }
	}
}

[System.Serializable]
public class BattleMapTemplateFeature
{
	[SerializeField]
	private string terrainName;
	public string TerrainName
	{
		get { return terrainName; }
	}
	[SerializeField]
	private int limitInRow;
	public int LimitInRow
	{
		get { return limitInRow; }
	}
	[SerializeField]
	private int limitOnBattlefield;
	public int LimitOnBattlefield
	{
		get { return limitOnBattlefield; }
	}
}