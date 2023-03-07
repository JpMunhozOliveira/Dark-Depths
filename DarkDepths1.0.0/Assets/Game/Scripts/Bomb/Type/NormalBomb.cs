using UnityEngine;

public class NormalBomb : Bomb
{
    private void Update()
    {
        if (bombFuseTime > 0) bombFuseTime -= Time.deltaTime;
        else BlowBomb(fireLength);
    }
}
