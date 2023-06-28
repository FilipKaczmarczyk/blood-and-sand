using Items;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    private WeaponSlot _rightHandSlot;
    private WeaponSlot _leftHandSlot;

    private DamageCollider _rightHandDamageCollider;
    private DamageCollider _leftHandDamageCollider;

    private void Awake()
    {
        LoadSlots();
    }

    private void LoadSlots()
    {
        var weaponSlots = GetComponentsInChildren<WeaponSlot>();
        foreach (var weaponSlot in weaponSlots)
        {
            if (weaponSlot.isLeftHandSlot)
            {
                _leftHandSlot = weaponSlot;
            }
            else if (weaponSlot.isRightHandSlot)
            {
                _rightHandSlot = weaponSlot;
            }
        }
    }

    public void LoadWeaponOnSlot(Weapon weapon, bool isLeft)
    {
        if (isLeft)
        {
            _leftHandSlot.LoadWeaponModel(weapon);
            LoadLeftWeaponDamageCollider();
        }
        else
        {
            _rightHandSlot.LoadWeaponModel(weapon);
            LoadRightWeaponDamageCollider();
        }
    }

    #region WeaponDamageColliders

    private void LoadLeftWeaponDamageCollider()
    {
        _leftHandDamageCollider = _leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }
    
    private void LoadRightWeaponDamageCollider()
    {
        _rightHandDamageCollider = _rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }
    
    public void EnableLeftWeaponCollider()
    {
        _leftHandDamageCollider.EnableDamageCollider();
    }
    
    public void DisableLeftWeaponCollider()
    {
        _leftHandDamageCollider.DisableDamageCollider();
    }
    
    public void EnableRightWeaponCollider()
    {
        _rightHandDamageCollider.EnableDamageCollider();
    }
    
    public void DisableRightWeaponCollider()
    {
        _rightHandDamageCollider.DisableDamageCollider();
    }

    #endregion
}
