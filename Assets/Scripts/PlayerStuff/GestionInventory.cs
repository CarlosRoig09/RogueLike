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
    void Start()
    {
        WeaponData copyOfFirstWeapon =Instantiate(firstWeapon);
        _counter = 0;
        _weaponController = GameObject.Find("Weapon").GetComponent<ChangeWeaponController>();
        inventory.Weapons = new List<WeaponData>
        {
            copyOfFirstWeapon
        };
        SetWeaponValues(inventory.Weapons[0]);
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void AddWeapon(WeaponData weaponData)
    {
        Debug.Log("AddWeapon Arrived");
        if (inventory.LimitWeapons >= inventory.Weapons.Count)
            if (!IsInTheList(weaponData))
            {
                float bullets = 0;
                switch (weaponData.Name)
                {
                    case "Pistol":
                        bullets = GameManager.Instance.numberOfBulletsWeapon1;
                        break;
                    case "Rifle":
                        bullets = GameManager.Instance.numberOfBulletsWeapon2;
                        break;
                }
                weaponData.shootData.currentBullets = bullets;
                weaponData.shootData.TotalBullets = bullets;
                Debug.Log("Weapon Add");
                inventory.Weapons.Add(weaponData);
            }
            else Debug.Log("Weapon Repeated");
    }
    public void ChangeWeapon()
    {
        _counter += 1;
        if (inventory.Weapons.Count <= _counter)
            _counter = 0;
        Debug.Log(_counter);
       SetWeaponValues(inventory.Weapons[_counter]);
    }
    private bool IsInTheList(WeaponData weaponData)
    {
        foreach (var weapon in inventory.Weapons)
            if (weapon.Name == weaponData.Name)
            {
                weapon.shootData.TotalBullets += weaponData.shootData.TotalBullets;
                weapon.shootData.currentBullets = weapon.shootData.TotalBullets;
                return true;
            }
        return false;
    }


    public void SetWeaponValues(WeaponData newweapon)
    {
        _weaponController.SetNewWeaponData(newweapon);
    }
}
