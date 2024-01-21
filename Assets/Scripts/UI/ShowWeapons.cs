using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWeapons : MonoBehaviour
{
    [SerializeField]
    private Inventory _inventory;

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TMPro.TMP_Text>().text = _inventory.Weapons.Count.ToString() + "/" + _inventory.LimitWeapons;
    }
}
