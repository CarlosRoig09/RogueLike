using System.Collections.Generic;
using UnityEngine;
public class GestionInventory : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;
    public Inventory Inventory { get { return inventory; }
        set { inventory = value; }
    }
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
      //  AddWeapon(firstWeapon);
      //  SetWeaponValues(inventory.Weapons[0]);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool AddWeapon(WeaponData weaponData)
    {
        var cloneWeapon = Instantiate(weaponData);
        var meleeClone = Instantiate(weaponData.meleeData);
        var shootClone = Instantiate(weaponData.shootData);
        cloneWeapon.meleeData = meleeClone;
        cloneWeapon.shootData = shootClone;
        if (inventory.LimitWeapons > inventory.Weapons.Count)
        {
            if (!IsInTheList(weaponData))
            {
                inventory.Weapons.Add(cloneWeapon);
                if (inventory.Weapons.Count == 1)
                {
                    SetWeaponValues(inventory.Weapons[_counter]);
                }
                return true;
            } 
        }
        else
        {
            Debug.Log("No more space");
           // DetachTheCurrentWeapon(cloneWeapon);
            return true;
        }
        return false;
    }

    public void DetachTheCurrentWeapon(WeaponData newWeapon)
    {
            //Instantiate(inventory.Weapons[_counter].prefab,new Vector3(transform.position.x+2,transform.position.y+2),Quaternion.identity);
            inventory.Weapons[_counter] = newWeapon;
            SetWeaponValues(inventory.Weapons[_counter]);
    }
    public void ChangeWeapon()
    {
        if (inventory.Weapons.Count > 0)
        {
            if (inventory.Weapons[_counter].WA == WeaponState.Normal)
            {
                _counter += 1;
                if (inventory.Weapons.Count <= _counter)
                    _counter = 0;
                SetWeaponValues(inventory.Weapons[_counter]);
            }
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

    public void AddCoins(float value)
    {
        inventory.Coins += value;
    }

    public void SetWeaponValues(WeaponData newweapon)
    {
       _weaponController.SetNewWeaponData(newweapon);
    }
}
