using PlayerInputs;
using UnityEngine;

namespace Characters
{
    public class CharacterMovement : MonoBehaviour
    {
        public Rigidbody RigidBody { get; private set; }

        private CharacterManager _characterManager;
        private InputHandler _inputHandler;
        private CharacterAnimation _characterAnimation;
        
        [SerializeField] private Vector3 _moveDirection;

        [Header("Stats")] 
        [SerializeField] 
        private float movementSpeed = 5;

        [SerializeField] private float rotationSpeed = 10;

        private void Awake()
        {
            _characterManager = GetComponent<CharacterManager>();
            RigidBody = GetComponent<Rigidbody>();
            _inputHandler = GetComponent<InputHandler>();
            _characterAnimation = GetComponentInChildren<CharacterAnimation>();
        }

        private void Start()
        {
            _characterAnimation.Init();
        }
        
        #region Movement

        private Vector3 normalVector;
        private Vector3 targetPosition;

        public void Move(float delta)
        {
            if(_characterManager.isInteracting)
                return;
            
            _moveDirection = new Vector3(_inputHandler.horizontal, 0.0f, _inputHandler.vertical);
            _moveDirection.Normalize();

            _moveDirection *= movementSpeed;

            RigidBody.MovePosition(RigidBody.position + _moveDirection * delta);

            _characterAnimation.UpdateAnimatorValues(_inputHandler.moveAmount, 0);

            if (_characterAnimation.CanRotate)
            {
                Rotate(Time.deltaTime);
            }
        }

        private void Rotate(float delta)
        {
            var targetDir = new Vector3(_inputHandler.horizontal, 0.0f, _inputHandler.vertical);
            targetDir.Normalize();

            targetDir *= rotationSpeed;
          

            if (targetDir == Vector3.zero)
            {
                targetDir = transform.forward;
            }

            var rs = rotationSpeed;

            var tr = Quaternion.LookRotation(targetDir);
            var targetRotation = Quaternion.Slerp(transform.rotation, tr, rs * delta);

            transform.rotation = targetRotation;
        }

        public void Roll(float delta)
        {
            if (_characterAnimation.IsInteracting())
                return;

            if (_inputHandler.rollFlag)
            {
                _moveDirection = new Vector3(_inputHandler.horizontal, 0.0f, _inputHandler.vertical);
                _moveDirection.Normalize();

                if(_inputHandler.moveAmount > 0)
                {
                    _characterAnimation.PlayTargetAnimation("Roll", true);
                    _moveDirection.y = 0.0f;
                    var rollRotation = Quaternion.LookRotation(_moveDirection);
                    transform.rotation = rollRotation;
                }
                else
                {
                    _characterAnimation.PlayTargetAnimation("StepBack", true);
                }
            }
        }

        #endregion
    }
}
