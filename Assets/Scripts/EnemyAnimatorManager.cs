using UnityEngine;

public class EnemyAnimatorManager : AnimatorManager
{
    private EnemyLocomotionManager _enemyLocomotionManager;

    public override void Awake()
    {
        base.Awake();
        
        _enemyLocomotionManager = GetComponentInParent<EnemyLocomotionManager>();
    }

    private void OnAnimatorMove()
    {
        _enemyLocomotionManager.RigidBody.drag = 0;
        var deltaPosition = Anim.deltaPosition;
        deltaPosition.y = 0;
        var velocity = deltaPosition / Time.deltaTime;
        _enemyLocomotionManager.RigidBody.velocity = velocity;
    }
}
