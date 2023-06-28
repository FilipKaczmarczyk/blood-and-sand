using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/New Weapon")]
    public class Weapon : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("One Handed Attack Animations")]
        public List<string> ohLightAttacks;
        public string ohHeavyAttack;
    }
}
