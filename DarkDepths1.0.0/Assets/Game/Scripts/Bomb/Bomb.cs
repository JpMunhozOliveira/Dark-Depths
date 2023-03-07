using UnityEngine;
public static class BombKeys
{
    public const string Bricks = "Bricks";
}

public class Bomb : MonoBehaviour, IDamageable
{
    [Header("Explosion")]
    [SerializeField] 
    protected Explosion explosionPrefab;
    [SerializeField] 
    protected LayerMask staticLayerMask;
    [SerializeField] 
    protected LayerMask playableLayerMask;
    [SerializeField] 
    protected float explosionDuration = 0.8f;
    [SerializeField] 
    protected float bombFuseTime = 3f;
    [Header("Audio")]
    [SerializeField]
    private AudioClip audio;

    [Header("Controller")]
    [SerializeField]
    protected TileBricksController bricksController;
    private PlayerController controller;
    protected int fireLength;
    private CircleCollider2D circleCollider2D;

    public PlayerController Controller { get => controller; set => controller = value; }

    private void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();

        if (TileBricksController.Instance)
        {
            bricksController = TileBricksController.Instance;
        }
        fireLength = Controller.FireLength;
    }

    protected void BlowBomb(int fireLenght)
    {
        circleCollider2D.enabled = false;
        var position = gameObject.transform.position;

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, Vector2.up, fireLenght);
        Explode(position, Vector2.down, fireLenght);
        Explode(position, Vector2.left, fireLenght);
        Explode(position, Vector2.right, fireLenght);

        AudioController.Instance.PlaySound(audio);

        Controller.ListBomb.Remove(gameObject);
        Destroy(gameObject);
    }

    protected virtual void Explode(Vector2 position, Vector2 direction, int length)
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
            return;
        }

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, direction, length - 1);
    }

    public virtual void TakeDamage()
    {
        BlowBomb(Controller.FireLength);
    }
}
