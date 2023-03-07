using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyAnimator : CharacterAnimator
{
    EnemyController mMovement;
    
    protected override void Awake()
    {
        base.Awake();

        mMovement = GetComponent<EnemyController>(); 
    }

    void Update()
    {
        animator.SetFloat(CharacterAnimationKeys.Horizontal, mMovement.MoveDirection.x);
        animator.SetFloat(CharacterAnimationKeys.Vertical, mMovement.MoveDirection.y);

        FlipSprite(mMovement.MoveDirection.x);
    }

    private void DeadEvent()
    {
        Destroy(gameObject);
    }
}
