using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour
{
    [SerializeField]
    private Probability_PowerUps[] powerList;

    private int maxRange = 0;

    private void Start()
    {
        for (int j = 0; j <= powerList.Length - 1; j++)
            maxRange += powerList[j].probability;
    }

    public void SpawnPowerUp(Vector3 position)
    {

        int i = Random.Range(0, maxRange);

        for (int k = 0; k <= powerList.Length - 1; k++)
        {
            if (i <= powerList[k].probability)
            {
                Instantiate(powerList[k]._gameObject, position, transform.rotation);
                break;
            }else{
                i -= powerList[k].probability;
            }
        }
    }
}

[System.Serializable]
class Probability_PowerUps
{
    public GameObject _gameObject;
    public int probability;
}