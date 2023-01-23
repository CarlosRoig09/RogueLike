using System.Collections.Generic;
using UnityEngine;
public class GestionInventory : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;
    private ChangeWeaponController _weaponController;
    private int _counter;
    [SerializeField]
    private WeaponData firstWeapon;
    // Start is called before the first frame update
    void Awake()
    {
        inventory.Weapons = new List<WeaponData>();
        _counter = 0;
        _weaponController = GameObject.Find("Weapon").GetComponent<ChangeWeaponController>();
        AddWeapon(firstWeapon);
        SetWeaponValues(inventory.Weapons[0]);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddWeapon(WeaponData weaponData)
    {
        if (inventory.LimitWeapons >= inventory.Weapons.Count)
        {
            if (!IsInTheList(weaponData))
            {
                inventory.Weapons.Add(Instantiate(weaponData));
            }
            else Debug.Log("Weapon Repeated");
        }
        else Debug.Log("No more space");
    }

    public void DetachTheCurrentWeapon()
    {
        if(inventory.Weapons.Count>1)
        {
            Instantiate(inventory.Weapons[_counter]);
            inventory.Weapons.RemoveAt(_counter);
            SetWeaponValues(inventory.Weapons[_counter]);
        }
    }
    public void ChangeWeapon()
    {
        if (inventory.Weapons[_counter].WA==WeaponState.Normal) {
            _counter += 1;
            if (inventory.Weapons.Count <= _counter)
                _counter = 0;
            if (inventory.Weapons.Count > 1)
                SetWeaponValues(inventory.Weapons[_counter]);
            }
    }
    private bool IsInTheList(WeaponData weaponData)
    {
        foreach (var weapon in inventory.Weapons)
            if (weapon.Name == weaponData.Name)
            {
              /*  weapon.shootData.TotalBullets += weaponData.shootData.TotalBullets;
                weapon.shootData.currentBullets = weapon.shootData.TotalBullets;*/
                return true;
            }
        return false;
    }


    public void SetWeaponValues(WeaponData newweapon)
    {
       _weaponController.SetNewWeaponData(newweapon);
    }
}
