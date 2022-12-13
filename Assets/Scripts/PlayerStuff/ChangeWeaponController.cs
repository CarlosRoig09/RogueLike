using UnityEngine;

public class ChangeWeaponController : MonoBehaviour
{
    private ShootControler _shootControler;
    private void Awake()
    {
        _shootControler = gameObject.GetComponent<ShootControler>();
    }
    public void SetNewWeaponData(WeaponData weaponData)
    {
        _shootControler.NewWeapon(weaponData.shootData);
    }
}
