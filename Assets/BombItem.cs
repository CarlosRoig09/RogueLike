using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : ItemBehaivour
{
    public override void DestroyItem()
    {
        Destroy(gameObject);
    }

    public override void GiveToPlayer(GameObject player)
    {
        GivePuntuation(itemData.Puntuation);
    }

    public override IEnumerator TimeTillItemDesapeare(float time)
    {
        yield return new WaitForSeconds(time);
        DestroyItem();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GiveToPlayer(collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(TimeTillItemDesapeare(itemData.CountDown));
    }
}
