using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeItemController : ItemBehaivour
{
    public LifeItemSO lifeItemSO;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(TimeTillItemDesapeare(lifeItemSO.CountDown));
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
        player.GetComponent<Player>().SumLife(lifeItemSO.VariableLife);
        GivePuntuation(lifeItemSO.Puntuation);
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
