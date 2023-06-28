using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public bool IsPerformingAction { get; private set; }
    
    private EnemyLocomotionManager _enemyLocomotionManager;

    [Header("A.I. Settings")]
    public float detectionRadius;
    
    private void Awake()
    {
        _enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
    }

    private void FixedUpdate()
    {
        HandleCurrentAction();
    }

    private void HandleCurrentAction()
    {
        if (_enemyLocomotionManager.currentTarget == null)
        {
            _enemyLocomotionManager.HandleDetection();
        }
        else
        {
            _enemyLocomotionManager.HandleMoveToTarget();
        }
    }
}
