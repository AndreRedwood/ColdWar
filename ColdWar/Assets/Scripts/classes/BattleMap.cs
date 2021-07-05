using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleMap
{
	[SerializeField]
	private Sprite background;
	[SerializeField]
	private BattleTile[,] tilesLeft = new BattleTile[4,3];
	public BattleTile[,] TilesLeft
	{
		get { return tilesLeft; }
	}
	private BattleTile[,] tilesRight = new BattleTile[4, 3];
	public BattleTile[,] TilesRight
	{
		get { return tilesRight; }
	}

	public BattleMap(ResourceManager resourceManager, BattleMapTemplate template)
	{
		int[] currentRowApperance = new int[template.TerrainFeatures.Length];
		int[] allRowsApperance = new int[template.TerrainFeatures.Length];
		background = resourceManager.findBattleBackgroundSpriteByName(template.BackgroundImageName);
		for (int t = 0; t < 3; t++)
		{
			//Debug.Log("今");
			int roll = Random.Range(0, 100);
			int rollHeplerInt = 0;
			for (int i = 0; i < template.TerrainFeatures.Length; i++)
			{
				if(roll < template.Row1Features[i] + rollHeplerInt)
				{
					//Debug.Log("今");
					if (currentRowApperance[i] < template.TerrainFeatures[i].LimitInRow && allRowsApperance[i] < template.TerrainFeatures[i].LimitOnBattlefield)
					{
						//Debug.Log("今"+i);
						tilesLeft[0, t].Feature = resourceManager.findTerrainFeatureByName(template.TerrainFeatures[i].TerrainName);
						currentRowApperance[i]++;
						allRowsApperance[i]++;
					}
					break;
				}
				else
				{
					rollHeplerInt += template.Row1Features[i];
				}
			}
		}
	}
}
