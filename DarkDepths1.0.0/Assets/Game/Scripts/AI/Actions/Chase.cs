using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("Game/Chase")]
public class Chase : BasePrimitiveAction
{
    [InParam("AIPathFinder")]
    private AIPathFinder pathFinder;
    [InParam("EnemyController")]
    private EnemyController controller;

    public override void OnStart()
    {
        base.OnStart();
        //ReCalculatePath();
    }

    public override TaskStatus OnUpdate()
    {
        if (controller.CurrentPath.Count < 1)
        {
            //ReCalculatePath();
        }
        return TaskStatus.RUNNING;
    }

    /*public void ReCalculatePath()
    {
        controller.CurrentColor = Color.red;

        List<Vector2> freeNodes = pathFinder.GetFreeNodesRadius();
        var r = Random.Range(0, freeNodes.Count);

        if (Vector2.Distance(controller.gameObject.transform.position, controller.Target.transform.position) < 0.5f)
        {
            controller.CurrentPath = pathFinder.GetPath(freeNodes[r]);
        }
        else
        {
            controller.CurrentPath = pathFinder.GetPath(controller.Target.transform.position);

            if(controller.CurrentPath.Count == 0)
            {
                controller.CurrentPath = pathFinder.GetPath(freeNodes[r]);
            }
        }
    }*/
}
