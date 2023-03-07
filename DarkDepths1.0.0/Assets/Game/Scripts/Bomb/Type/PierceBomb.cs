using UnityEngine;

public class PierceBomb : Bomb
{
    private void Update()
    {
        if (bombFuseTime > 0) bombFuseTime -= Time.deltaTime;
        else BlowBomb(fireLength);
    }

    protected override void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }

        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, staticLayerMask))
        {
            return;
        }

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, playableLayerMask))
        {
            if (bricksController != null)
            {
                bricksController.DestroyBrick(position);
                bricksController.SpawnAnimationDestroy(position);
            }
        }

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, direction, length - 1);
    }
}
