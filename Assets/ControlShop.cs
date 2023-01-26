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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
