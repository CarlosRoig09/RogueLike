using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPuntuation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TMPro.TMP_Text>().text = "Score: " + GameManager.Instance.Puntuation.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
