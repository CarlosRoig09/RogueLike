using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItemController : ItemBehaivour
{
    public WeaponData weaponData;
    // Start is called before the first frame update
    void Start()
    {
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
            DestroyItem();
        }
    }

    public override void GiveToPlayer(GameObject player)
    {
      player.GetComponent<GestionInventory>().AddWeapon(weaponData);
    }

    public override void DestroyItem()
    {
        Destroy(gameObject);
    }

    public override IEnumerator TimeTillItemDesapeare(float time)
    {
        yield return new WaitForSeconds(time);
        DestroyItem();
    }
}
