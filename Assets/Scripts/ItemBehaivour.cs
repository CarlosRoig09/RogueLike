using System.Collections;
using UnityEngine;

public abstract class ItemBehaivour : MonoBehaviour
{
    public abstract void GiveToPlayer(GameObject player);
    public abstract void DestroyItem();
    public abstract IEnumerator TimeTillItemDesapeare(float time);
}
