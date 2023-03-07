using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerAnimator : CharacterAnimator
{
    PlayerController playerController;

    protected override void Awake()
    {
        base.Awake();

        playerController = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        animator.SetFloat(CharacterAnimationKeys.Horizontal, playerController.PlInput.GetMovementInput().x);
        animator.SetFloat(CharacterAnimationKeys.Vertical, playerController.PlInput.GetMovementInput().y);

        FlipSprite(playerController.PlInput.GetMovementInput().x);
    }
}
