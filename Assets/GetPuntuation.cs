using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPuntuation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PuntuationChanged();
    }

   public void PuntuationChanged()
    {
        gameObject.GetComponent<TMPro.TMP_Text>().text = GameManager.Instance.Puntuation.ToString();
    }
}
