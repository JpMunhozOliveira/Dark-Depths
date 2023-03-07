using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInput : MonoBehaviour
{
    private struct PlayerInputConstants
    {
        public const string Horizontal = "Horizontal";
        public const string Vertical = "Vertical";
        public const string Attack = "Attack";
        public const string Action = "Action";
        public const string Interaction = "Interaction";
    }

    public Vector2 GetMovementInput()
    {

        float horizontalInput = Input.GetAxisRaw(PlayerInputConstants.Horizontal);
        float verticalInput = Input.GetAxisRaw(PlayerInputConstants.Vertical);

        if (Mathf.Approximately(horizontalInput, 0.0f))
        {
            horizontalInput = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Horizontal);
        }

        if (Mathf.Approximately(verticalInput, 0.0f))
        {
            verticalInput = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Vertical);
        }
        return new Vector2(horizontalInput, verticalInput);
    }

    public bool IsAttackButtonDown()
    {

        bool isKeyboardButtonDown = Input.GetKeyDown(KeyCode.J);
        bool isMobileButtonDown = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Attack);
        return isKeyboardButtonDown || isMobileButtonDown;
    }
    public bool IsAttackButtonUp()
    {

        bool isKeyboardButtonUp = Input.GetKey(KeyCode.J) == false;
        bool isMobileButtonDUp = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Attack);
        return isKeyboardButtonUp && isMobileButtonDUp;
    }

    public bool IsActionButtonDown()
    {

        bool isKeyboardButtonDown = Input.GetKeyDown(KeyCode.I);
        bool isMobileButtonDown = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Action);
        return isKeyboardButtonDown || isMobileButtonDown;
    }
    public bool IsActionButtonUp()
    {

        bool isKeyboardButtonUp = Input.GetKey(KeyCode.I) == false;
        bool isMobileButtonDUp = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Action);
        return isKeyboardButtonUp && isMobileButtonDUp;
    }

    public bool IsInteractionButtonDown()
    {

        bool isKeyboardButtonDown = Input.GetKeyDown(KeyCode.K);
        bool isMobileButtonDown = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Interaction);
        return isKeyboardButtonDown || isMobileButtonDown;
    }
    public bool IsInteractionButtonUp()
    {

        bool isKeyboardButtonUp = Input.GetKey(KeyCode.K) == false;
        bool isMobileButtonDUp = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Interaction);
        return isKeyboardButtonUp && isMobileButtonDUp;
    }
}
