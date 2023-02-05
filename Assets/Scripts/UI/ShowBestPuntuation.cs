using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBestPuntuation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TMPro.TMP_Text>().text = JsonFitxerMethods.ReturnBestPuntuation("Ranking.json").ToString();   
    }
}
