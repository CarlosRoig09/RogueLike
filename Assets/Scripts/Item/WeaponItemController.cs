using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItemController : ItemBehaivour
{
    public WeaponData weaponData;
    // Start is called before the first frame update
    void Start()
    {
        weaponData.WA = WeaponState.Item;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(TimeTillItemDesapeare(weaponData.CountDown));
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GiveToPlayer(collision.gameObject);
        }
    }

    public override void GiveToPlayer(GameObject player)
    {
        if (player.GetComponent<GestionInventory>().AddWeapon(weaponData))
        {
            AudioManager.instance.Play("ObtainWeapon");
            GivePuntuation(weaponData.Puntuation);
            DestroyItem();
        }
    }

    public override void DestroyItem()
    {
        foreach (var collider in gameObject.GetComponents<PolygonCollider2D>())
            collider.enabled = false;
        Destroy(gameObject);
    }

    public override IEnumerator TimeTillItemDesapeare(float time)
    {
        yield return new WaitForSeconds(time);
        DestroyItem();
    }
}
