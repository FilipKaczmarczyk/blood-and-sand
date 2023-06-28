using Characters;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [SerializeField] private int damage = 25;
    private Collider _damageCollider;
    
    private void Awake()
    {
        _damageCollider = GetComponent<Collider>();
        _damageCollider.gameObject.SetActive(true);
        _damageCollider.isTrigger = true;
        _damageCollider.enabled = false;
    }

    public void EnableDamageCollider()
    {
        _damageCollider.enabled = true;
    }

    public void DisableDamageCollider()
    {
        _damageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag($"Hittable"))
        {
            var characterStats = other.GetComponent<HealthSystem>();

            if (characterStats != null)
            {
                characterStats.TakeDamage(damage);
            }
        }
    }
}
