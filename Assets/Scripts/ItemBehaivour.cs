using System.Collections;
using UnityEngine;

public abstract class ItemBehaivour : MonoBehaviour, IGivePuntuation
{
    public ItemData itemData;
    public abstract void GiveToPlayer(GameObject player);
    public abstract void DestroyItem();
    public abstract IEnumerator TimeTillItemDesapeare(float time);

    public void GivePuntuation(float Puntuation)
    {
        GameManager.Instance.Puntuation += Puntuation;
    }
}
