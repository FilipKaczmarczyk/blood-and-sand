using Items;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    public Transform parentOverride;
    public bool isRightHandSlot;
    public bool isLeftHandSlot;

    public GameObject currentWeaponModel;

    public void UnloadWeaponAndDestroy()
    {
        if (currentWeaponModel != null)
        {
            Destroy(currentWeaponModel);
        }
    }
    
    public void UnloadWeapon()
    {
        if (currentWeaponModel != null)
        {
            currentWeaponModel.SetActive(false);
        }
    }
    
    public void LoadWeaponModel(Weapon weapon)
    {
        UnloadWeaponAndDestroy();

        if (weapon == null)
        {
            UnloadWeapon();
            return;
        }

        var model = Instantiate(weapon.modelPrefab) as GameObject;

        if (model != null)
        {
            if (parentOverride != null)
            {
                model.transform.parent = parentOverride;
            }
            else
            {
                model.transform.parent = transform;
            }
            
            model.transform.localPosition = Vector3.zero;
            model.transform.localRotation = Quaternion.identity;
            model.transform.localScale = Vector3.one;
        }

        currentWeaponModel = model;
    }
}
