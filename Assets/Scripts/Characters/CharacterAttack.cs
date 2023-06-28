using System;
using Items;
using PlayerInputs;
using UnityEngine;

namespace Characters
{
    public class CharacterAttack : MonoBehaviour
    {
        private CharacterAnimation _characterAnimation;
        private InputHandler _inputHandler;
        public string lastAttack;
        
        private static readonly int CanDoCombo = Animator.StringToHash("CanDoCombo");

        private void Awake()
        {
            _characterAnimation = GetComponentInChildren<CharacterAnimation>();
            _inputHandler = GetComponent<InputHandler>();
        }

        public void HandleCombo(Weapon weapon)
        {
            if (_inputHandler.comboFlag)
            {
                _characterAnimation.Anim.SetBool(CanDoCombo, false);
            
                if (weapon.ohLightAttacks.Contains(lastAttack))
                {
                    var nextComboIndex = weapon.ohLightAttacks.IndexOf(lastAttack) + 1;

                    Debug.Log(nextComboIndex);
                    
                    if (weapon.ohLightAttacks.Count > nextComboIndex)
                    {
                        LightAttack(weapon, nextComboIndex);
                    }
                }
            }
        }
        
        public void LightAttack(Weapon weapon, int comboIndex = 0)
        {
            _characterAnimation.PlayTargetAnimation(weapon.ohLightAttacks[comboIndex], true);
            lastAttack = weapon.ohLightAttacks[comboIndex];
        }

        public void HeavyAttack(Weapon weapon)
        {
            _characterAnimation.PlayTargetAnimation(weapon.ohHeavyAttack, true);
            lastAttack = weapon.ohHeavyAttack;
        }
    }
}
