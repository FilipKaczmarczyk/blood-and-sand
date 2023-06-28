using Characters;
using UnityEngine;

public class TestDamage : MonoBehaviour
{
    [SerializeField] private int testDamage;

    private void OnTriggerEnter(Collider other)
    {
        var characterStats = other.GetComponent<HealthSystem>();

        if (characterStats != null)
        {
            characterStats.TakeDamage(testDamage);
        }
    }
}
