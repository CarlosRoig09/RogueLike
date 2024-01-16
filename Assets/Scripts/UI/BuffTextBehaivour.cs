using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTextBehaivour : MonoBehaviour
{
    public void OnEnable()
    {
        GetComponent<Animator>().SetBool("Start", true);
    }

    public void EndAnimation()
    {
        GetComponent<Animator>().SetBool("Start", false);
        gameObject.SetActive(false);
    }
}
