using Items;
using UnityEngine;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public Weapon rightWeapon;
        public Weapon leftWeapon;
    
        private WeaponSlotManager _weaponSlotManager;

        private void Awake()
        {
            _weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        }

        private void Start()
        {
            _weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
            _weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
        }
    }
}