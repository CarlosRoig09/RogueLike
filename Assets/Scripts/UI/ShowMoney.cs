using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMoney : MonoBehaviour
{
    [SerializeField]
    private Inventory _inventory;

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TMPro.TMP_Text>().text = _inventory.Coins.ToString();
    }
}
