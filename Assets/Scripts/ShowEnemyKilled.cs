using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEnemyKilled : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TMPro.TMP_Text>().text = "Enemies Killed: " + GameManager.Instance.NumberOfEnemyKilled.ToString();
    }
}
