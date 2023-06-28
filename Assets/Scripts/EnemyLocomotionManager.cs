using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotionManager : MonoBehaviour
{
    public Rigidbody RigidBody { get; private set; }
    
    public HealthSystem currentTarget;
    
    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float rotationSpeed;

    private NavMeshAgent _navMeshAgent;
    private EnemyManager _enemyManager;
    private EnemyAnimatorManager _enemyAnimatorManager;
    
    private float _distanceFormTarget;

    private static readonly int Vertical = Animator.StringToHash("Vertical");

    private void Awake()
    {
        _enemyManager = GetComponent<EnemyManager>();
        _enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        _navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        RigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _navMeshAgent.enabled = false;
        RigidBody.isKinematic = false;
    }

    public void HandleDetection()
    {
        var colliders = Physics.OverlapSphere(transform.position, _enemyManager.detectionRadius, detectionLayer);
        
        for(var i = 0; i < colliders.Length; i++)
        {
            var healthSystem = colliders[i].transform.GetComponent<HealthSystem>();

            if (healthSystem != null)
            {
                // CHECK FOR TEAM ID

                var targetDirection = healthSystem.transform.position - transform.position;

                currentTarget = healthSystem;
            }
        }
    }
    
    public void HandleMoveToTarget()
    {
        var targetDirection = currentTarget.transform.position - transform.position;
        _distanceFormTarget = Vector3.Distance(currentTarget.transform.position, transform.position);

        if (_enemyManager.IsPerformingAction)
        {
            _enemyAnimatorManager.Anim.SetFloat(Vertical, 0, 0.1f, Time.deltaTime);
            _navMeshAgent.enabled = false;
        }
        else
        {
            if (_distanceFormTarget > stoppingDistance)
            {
                _enemyAnimatorManager.Anim.SetFloat(Vertical, 1, 0.1f, Time.deltaTime);
            }
            else
            {
                _enemyAnimatorManager.Anim.SetFloat(Vertical, 0, 0.1f, Time.deltaTime);
            }
        }

        HandleRotateTowardsTarget();
        
        _navMeshAgent.transform.localPosition = Vector3.zero;
        _navMeshAgent.transform.localRotation = Quaternion.identity;
    }

    private void HandleRotateTowardsTarget()
    {
        if (_enemyManager.IsPerformingAction)
        {
            var direction = currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            var targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
        }
        else
        {
            var relativeDirection = transform.InverseTransformDirection(_navMeshAgent.desiredVelocity);
            var targetVelocity = RigidBody.velocity;

            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(currentTarget.transform.position);
            RigidBody.velocity = targetVelocity;
            transform.rotation = Quaternion.Slerp(transform.rotation, _navMeshAgent.transform.rotation, rotationSpeed / Time.deltaTime);
        }
    }
}
