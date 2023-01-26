using UnityEngine;
using System.Collections.Generic;

public class SetItemsShop : MonoBehaviour
{
    [SerializeField]
    private ItemData[] _itemDatas;
    [SerializeField]
    private GameObject[] _itemHolder;
    private GameObject[] itemsSpawned;
    // Start is called before the first frame update
    void Start()
    {
        itemsSpawned = new GameObject[_itemHolder.Length];
        bool isRepitive;
        int count = 0;
        while (count < _itemHolder.Length)
        {
            do
            {
                isRepitive = false;
                itemsSpawned[count] = _itemDatas[RandomMethods.ReturnARandomObject(_itemDatas, 0, _itemDatas.Length, 0)].prefab;
             if(count > 0)
                 for (int i = count - 1; i >=0 &&!isRepitive; i--)
                 {
                     if (itemsSpawned[count] == itemsSpawned[i])
                         isRepitive = true;
             }
            } while (isRepitive);
            ParentAndChildrenMethods.ParentAChildren(_itemHolder[count],Instantiate(itemsSpawned[count]));
            _itemHolder[count].transform.GetChild(0).transform.localPosition = new Vector3(0,0);
            count++;
        }
    }
}
