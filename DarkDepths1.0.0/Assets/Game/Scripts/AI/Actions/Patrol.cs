using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("Game/Patrol")]
public class Patrol : BasePrimitiveAction
{
    [InParam("AIPathFinder")]
    private AIPathFinder pathFinder;
    [InParam("EnemyController")]
    private EnemyController controller;

    public override void OnStart()
    {
        base.OnStart();
        ReCalculatePath();
    }

    public override TaskStatus OnUpdate()
    {
        if (controller.CurrentPath.Count < 1)
        {
            ReCalculatePath();
        }
        return TaskStatus.RUNNING;
    }

    public void ReCalculatePath()
    {
        controller.CurrentColor = Color.green;

        pathFinder.GetFreeNodesRadius();

        if(pathFinder.FreeNodes.Count > 0)
        {
            var r = Random.Range(0, pathFinder.FreeNodes.Count - 1);
            controller.CurrentPath = pathFinder.GetPath(pathFinder.FreeNodes[r]);
        }
    }
}
