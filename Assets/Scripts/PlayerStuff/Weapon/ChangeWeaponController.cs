using UnityEngine;

public class ChangeWeaponController : MonoBehaviour
{

    public void SetNewWeaponData(WeaponData weaponData)
    {
        GameObject newWeapon = Instantiate(weaponData.prefab);
        Collider2D[] colliders = newWeapon.GetComponents<PolygonCollider2D>();
        if (colliders.Length > 1)
            Destroy(colliders[1]);
        if (transform.childCount > 0)
        {
            GameObject actualWeapon = transform.GetChild(0).gameObject;
            newWeapon.transform.SetPositionAndRotation(actualWeapon.transform.position, actualWeapon.transform.rotation);
            Destroy(actualWeapon);
        }
        ParentAndChildrenMethods.ParentAChildren(gameObject, newWeapon);
        Destroy(newWeapon.GetComponent<WeaponItemController>());
        IWeaponControler weaponComponent = newWeapon.GetComponent<IWeaponControler>();
        if (weaponComponent is MonoBehaviour mono)
            mono.enabled = true;
       weaponComponent.WeaponSO = weaponData;
        transform.parent.gameObject.GetComponentInParent<PlayerController>().Weapon = weaponComponent;
    }
  
}
