using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlShop : MonoBehaviour
{
    private GameObject _item;
    // Start is called before the first frame update
    void Start()
    {
        _item = transform.GetChild(0).gameObject;
        _item.GetComponent<ItemBehaivour>().enabled= false;
        foreach (var item in _item.GetComponents<Collider2D>())
        {
            item.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.CompareTag("Player"))
        {
            float price;
           if(collision.gameObject.GetComponent<GestionInventory>().Inventory.Coins>= ( price =_item.GetComponent<ItemBehaivour>().itemData.Price))
            {
                collision.gameObject.GetComponent<GestionInventory>().Inventory.Coins -= price;
                foreach (var item in _item.GetComponents<Collider2D>())
                {
                    item.enabled = true;
                }
               _item.GetComponent<ItemBehaivour>().enabled = true;
                GetComponent<Collider2D>().enabled = false;
            }

        }
    }
}
