using System.Collections;
using UnityEngine;

public class DisableIfFarAway : MonoBehaviour
{
    [SerializeField]
    private ItemActivator activationScript;

    void Start()
    {
        activationScript = GameObject.FindWithTag("Player").GetComponent<ItemActivator>();

        StartCoroutine("AddToList");
    }

    IEnumerator AddToList()
    {
        yield return new WaitForSeconds(0.1f);

        BehaviorExecutor behavior = GetComponent<BehaviorExecutor>();

        activationScript.AddList.Add(behavior);
    }
}
