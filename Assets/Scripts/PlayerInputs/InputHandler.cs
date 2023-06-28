using System;
using Characters;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace PlayerInputs
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool rollInput;
        public bool lightAttackInput;
        public bool heavyAttackInput;

        public bool comboFlag;
        public bool rollFlag;
        
        private PlayerControls _inputActions;
        private CharacterAttack _characterAttack;
        private PlayerInventory _playerInventory;
        private CharacterManager _characterManager;

        private Vector2 _movementInput;
        private Vector2 _cameraInput;

        private void Awake()
        {
            _characterAttack = GetComponent<CharacterAttack>();
            _playerInventory = GetComponent<PlayerInventory>();
            _characterManager = GetComponent<CharacterManager>();
        }

        private void OnEnable()
        {
            if (_inputActions == null)
            {
                _inputActions = new PlayerControls();
                _inputActions.PlayerMovement.Movement.performed += inputActions => _movementInput = inputActions.ReadValue<Vector2>();
                _inputActions.PlayerMovement.Camera.performed += i => _cameraInput = i.ReadValue<Vector2>();
            }
            
            _inputActions.Enable();
        }
        
        private void OnDisable()
        {
            _inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
            RollInput(delta);
            AttackInput(delta);
        }

        private void MoveInput(float delta)
        {
            horizontal = _movementInput.x;
            vertical = _movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = _cameraInput.x;
            mouseY = _cameraInput.y;
        }

        private void RollInput(float delta)
        {
            rollInput = _inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Performed;

            if (rollInput)
            {
                rollFlag = true;
            }
        }

        private void AttackInput(float delta)
        {
            _inputActions.PlayerActions.LightAttack.performed += i => lightAttackInput = true;
            _inputActions.PlayerActions.HeavyAttack.performed += i => heavyAttackInput = true;

            if (lightAttackInput)
            {
                if (_characterManager.canDoCombo)
                {
                    comboFlag = true;
                    _characterAttack.HandleCombo(_playerInventory.rightWeapon);
                    comboFlag = false;
                }
                else
                {
                    if(_characterManager.isInteracting)
                        return;
                    
                    if(_characterManager.canDoCombo)
                        return;
                    
                    _characterAttack.LightAttack(_playerInventory.rightWeapon);
                }
            }

            if (heavyAttackInput)
            {
                _characterAttack.HeavyAttack(_playerInventory.rightWeapon);
            }
        }
    }
}
