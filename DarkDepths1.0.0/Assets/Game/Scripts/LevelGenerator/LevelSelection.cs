using UnityEngine;
using Edgar.Unity;

[System.Serializable]
public class LevelSelection : MonoBehaviour
{
    [Header("Level Config")]
    private DungeonGeneratorGrid2D levelInfo;
    [SerializeField]
    private LevelGraph[] levelsGraph;
    [SerializeField]
    private LevelGraph[] finalLevels;

    private void Awake()
    {
        levelInfo = GetComponent<DungeonGeneratorGrid2D>();

        if(GameManager.Instance.World < 2)
        {
            var r = Random.Range(0, levelsGraph.Length);
            levelInfo.FixedLevelGraphConfig.LevelGraph = levelsGraph[r];
        }
        else
        {
            var r = Random.Range(0, finalLevels.Length);
            levelInfo.FixedLevelGraphConfig.LevelGraph = finalLevels[r];
        }
        
    }
}
