using Pada1.BBCore;
using Pada1.BBCore.Framework;
using UnityEngine;

[Condition("Game/Perception/IsTargetVisible")]
public class IsTargetVisible : ConditionBase
{
    [InParam("EnemyController")]
    private EnemyController controller;

    [InParam("TargetMemoryDuration")]
    private float targetMemoryDuration;
    private float forgetTargetTime;

    public override bool Check()
    {
        if (controller.IsTargetVisible())
        {
            forgetTargetTime = Time.time + targetMemoryDuration;
            return true;
        }

        return Time.time < forgetTargetTime;
    }
}
