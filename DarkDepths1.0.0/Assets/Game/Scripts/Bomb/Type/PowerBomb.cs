using UnityEngine;

public class PowerBomb : Bomb
{
    private int power = 15;

    private void Update()
    {
        if (bombFuseTime > 0) bombFuseTime -= Time.deltaTime;
        else BlowBomb(power);
    }

    public override void TakeDamage()
    {
        BlowBomb(power);
    }
}
