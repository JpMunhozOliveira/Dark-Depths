using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemActivator : MonoBehaviour
{

    [SerializeField]
    private int distanceToActivate = 10;

    private List<BehaviorExecutor> activatorItems;

    private List<BehaviorExecutor> addList;

    public List<BehaviorExecutor> AddList { get => addList; set => addList = value; }

    void Start()
    {
        activatorItems = new List<BehaviorExecutor>();
        addList = new List<BehaviorExecutor>();

        AddToList();
    }

    void AddToList()
    {
        if (addList.Count > 0)
        {
            foreach (BehaviorExecutor item in addList)
            {
                if (item != null)
                {
                    activatorItems.Add(item);
                }
            }

            addList.Clear();
        }

        StartCoroutine("CheckActivation");
    }

    IEnumerator CheckActivation()
    {
        List<BehaviorExecutor> removeList = new List<BehaviorExecutor>();

        if (activatorItems.Count > 0)
        {
            foreach (BehaviorExecutor item in activatorItems)
            {
                if (item != null)
                {
                    if (Vector2.Distance(transform.position, item.transform.position) > distanceToActivate)
                    {
                        item.enabled = false;
                    }
                    else
                    {
                        item.enabled = true;
                    }
                }else{
                    removeList.Add(item);
                }

                yield return new WaitForSeconds(0.01f);
            }
        }

        yield return new WaitForSeconds(0.01f);

        if (removeList.Count > 0)
        {
            foreach (BehaviorExecutor item in removeList)
            {
                activatorItems.Remove(item);
            }
        }

        yield return new WaitForSeconds(0.01f);

        AddToList();
    }
}
