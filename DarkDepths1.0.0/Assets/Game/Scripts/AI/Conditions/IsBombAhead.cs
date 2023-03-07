using Pada1.BBCore;
using Pada1.BBCore.Framework;
using UnityEngine;

[Condition("Game/Perception/IsBombAhead")]
public class IsBombAhead : ConditionBase
{
    [InParam("EnemyController")]
    private EnemyController controller;

    public override bool Check()
    {
        return controller.IsBombAhead();
    }
}
