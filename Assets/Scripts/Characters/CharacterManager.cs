using PlayerInputs;
using UnityEngine;

namespace Characters
{
    public class CharacterManager : MonoBehaviour
    {
        private InputHandler _inputHandler;
        private CharacterAnimation _characterAnimation;
        private CharacterMovement _characterMovement;
        
        public bool isInteracting;
        public bool canDoCombo;
        
        private static readonly int IsInteracting = Animator.StringToHash("IsInteracting");
        private static readonly int CanDoCombo = Animator.StringToHash("CanDoCombo");

        private void Awake()
        {
            _inputHandler = GetComponent<InputHandler>();
            _characterAnimation = GetComponentInChildren<CharacterAnimation>();
            _characterMovement = GetComponent<CharacterMovement>();
        }

        private void Update()
        {
            isInteracting = _characterAnimation.Anim.GetBool(IsInteracting);
            canDoCombo = _characterAnimation.Anim.GetBool(CanDoCombo);
            
            _inputHandler.TickInput(Time.deltaTime);

            _characterMovement.Move(Time.deltaTime);

            _characterMovement.Roll(Time.deltaTime);
        }

        private void LateUpdate()
        {
            _inputHandler.rollFlag = false;
            _inputHandler.lightAttackInput = false;
            _inputHandler.heavyAttackInput = false;
        }
    }
}
