using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : ItemBehaivour
{
    [SerializeField]
    private CoinItemSO _cISO;
    public override void DestroyItem()
    {
        Destroy(gameObject);
    }

    public override void GiveToPlayer(GameObject player)
    {
        player.GetComponent<GestionInventory>().AddCoins(_cISO.value);
        DestroyItem();
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(TimeTillItemDesapeare(_cISO.CountDown));
    }
}
