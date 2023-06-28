using PlayerInputs;
using UnityEngine;

namespace Characters
{
    public class CharacterAnimation : MonoBehaviour
    {
        [field:SerializeField] public bool CanRotate { get; private set; }
        public Animator Anim { get; private set; }

        private CharacterManager _characterManager;
        private InputHandler _inputHandler;
        private CharacterMovement _characterMovement;
        
        private int _vertical;
        private int _horizontal;
        private static readonly int CanDoCombo = Animator.StringToHash("CanDoCombo");

        public void Init()
        {
            Anim = GetComponent<Animator>();

            _characterManager = GetComponentInParent<CharacterManager>();
            _inputHandler = GetComponentInParent<InputHandler>();
            _characterMovement = GetComponentInParent<CharacterMovement>();

            _vertical = Animator.StringToHash("Vertical");
            _horizontal = Animator.StringToHash("Horizontal");
        }

        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement)
        {
            #region Vertical

            var v = 0.0f;

            if (verticalMovement > 0.0f && verticalMovement < 0.55f)
            {
                v = 0.5f;
            }
            else if(verticalMovement >= 0.5f)
            {
                v = 1.0f;
            }
            else if (verticalMovement < 0.0f && verticalMovement > -0.55f)
            {
                v = -0.5f;
            }
            else if (verticalMovement < -0.55f)
            {
                v = -1.0f;
            }
            else
            {
                v = 0.0f;
            }
            
            #endregion
            
            #region Horizontal
            
            var h = 0.0f;

            if (horizontalMovement > 0.0f && horizontalMovement < 0.55f)
            {
                h = 0.5f;
            }
            else if(horizontalMovement >= 0.5f)
            {
                h = 1.0f;
            }
            else if (horizontalMovement < 0.0f && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }
            else if (horizontalMovement < -0.55f)
            {
                h = -1.0f;
            }
            else
            {
                h = 0.0f;
            }

            #endregion
            
            Anim.SetFloat(_vertical, v, 0.1f, Time.deltaTime);
            Anim.SetFloat(_horizontal, h, 0.1f, Time.deltaTime);
        }

        public void PlayTargetAnimation(string targetAnim, bool isInteracting)
        {
            Anim.applyRootMotion = isInteracting;
            Anim.SetBool("IsInteracting", isInteracting);
            Anim.CrossFade(targetAnim, 0.2f);
        }

        public void StartRotate()
        {
            CanRotate = true;
        }

        public void StopRotation()
        {
            CanRotate = false;
        }

        public void EnableCombo()
        {
            Anim.SetBool(CanDoCombo, true);
        }

        public void DisableCombo()
        {
            Anim.SetBool(CanDoCombo, false);
        }

        private void OnAnimatorMove()
        {
            if (_characterManager.isInteracting == false)
                return;

            _characterMovement.RigidBody.drag = 0;
            var deltaPosition = Anim.deltaPosition;
            deltaPosition.y = 0;

            var velocity = deltaPosition / Time.deltaTime;
            _characterMovement.RigidBody.velocity = velocity;
        }

        public bool IsInteracting()
        {
            return Anim.GetBool("IsInteracting");
        }
    }
}
