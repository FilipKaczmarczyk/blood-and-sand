using System;

namespace Characters
{
    public class PlayerHealthSystem : HealthSystem
    {
        private CharacterAnimation _characterAnimation;

        private void Awake()
        {
            _characterAnimation = GetComponentInChildren<CharacterAnimation>();
        }
        
        public override void TakeDamage(int damage)
        {
            currentHealth -= damage;
            
            if (currentHealth > 0)
            {
                _characterAnimation.PlayTargetAnimation("Hurt", true);
            }
            else
            {
                currentHealth = 0;
                _characterAnimation.PlayTargetAnimation("Death", true);
            }

            OnHealthChanged();
        }
    }
}
