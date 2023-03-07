using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTreasure : MonoBehaviour
{
    [SerializeField]
    Transform[] positions;

    [SerializeField]
    GameObject[] treasures;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        List<int> randomTreasures = GenerateNumbers(positions.Length);
    
        for(int i = 0; i < positions.Length; i++) { 
            Instantiate(treasures[randomTreasures[i]], positions[i]);
        }
            
    }

    private List<int> GenerateNumbers(int count)
    {
        List<int> numbers = new List<int>();
        int i = 0;

        do
        {
            int select = Random.Range(0, treasures.Length - 1);

            if (!numbers.Contains(select))
            {
                numbers.Add(select);
                i++;
            }
        } while (i < count);

        return numbers;
    }
}
