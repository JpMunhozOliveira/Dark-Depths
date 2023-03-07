using UnityEngine;

public class MineBomb : Bomb
{
    public bool activeMine;

    private void Update()
    {
        if (activeMine)
        {
            BlowBomb(fireLength);
        }
    }
}
