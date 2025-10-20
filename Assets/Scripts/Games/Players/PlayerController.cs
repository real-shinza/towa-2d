using UnityEngine;
using UnityEngine.InputSystem;

namespace Towa.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerInput playerInput;
        private InputAction moveAction, jumpAction, attackAction, strikeAction, guardAction, pauseAction;

        private void Awake()
        {
            SetupInputAction();
        }

        private void SetupInputAction()
        {
            moveAction = playerInput.currentActionMap["Move"];
            jumpAction = playerInput.currentActionMap["Jump"];
            attackAction = playerInput.currentActionMap["Attack"];
            strikeAction = playerInput.currentActionMap["Strike"];
            guardAction = playerInput.currentActionMap["Block"];
            pauseAction = playerInput.currentActionMap["Pause"];
        }

        public Vector2 GetInputMove() { return moveAction.ReadValue<Vector2>(); }
        public bool GetInputJump() { return jumpAction.triggered; }
        public bool GetInputAttack() { return attackAction.triggered; }
        public bool GetInputStrike() { return strikeAction.triggered; }
        public bool GetInputBlock() { return guardAction.triggered; }
        public bool GetInputPause() { return pauseAction.triggered; }
    }
}
