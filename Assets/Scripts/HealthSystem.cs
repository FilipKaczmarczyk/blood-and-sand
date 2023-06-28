using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnHealthChange;
    
    [SerializeField] private int maxHealth = 100;
    [SerializeField] protected int currentHealth  = 100;
    
    private void Start()
    {
        SetHealthFromMaxLevel();
    }

    private void SetHealthFromMaxLevel()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;

        OnHealthChanged();
    }

    protected void OnHealthChanged()
    {
        OnHealthChange?.Invoke(this, EventArgs.Empty);
    }

    public float GetHealthNormalized()
    {
        return (float)currentHealth / maxHealth;
    }
}
