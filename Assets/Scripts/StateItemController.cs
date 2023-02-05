using System.Collections;
using UnityEngine;

public class StateItemController : ItemBehaivour
{
    [SerializeField]
    private StatItemSO _sUICSO;
    public override void DestroyItem()
    {
        Destroy(gameObject);
    }

    public override void GiveToPlayer(GameObject player)
    {
        if (_sUICSO.temporal)
            player.GetComponent<ControlStats>().ModificadorDeStat(_sUICSO.Stat,_sUICSO.modUp,_sUICSO.timeDuration);
        else
            player.GetComponent<ControlStats>().ModificadorDeStat(_sUICSO.Stat, _sUICSO.modUp);
        GivePuntuation(_sUICSO.Puntuation);
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
            DestroyItem();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(TimeTillItemDesapeare(_sUICSO.CountDown));
    }
}
