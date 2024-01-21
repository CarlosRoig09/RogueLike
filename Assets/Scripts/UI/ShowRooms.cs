using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRooms : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TMPro.TMP_Text>().text = "Rooms: " + GameManager.Instance.Rooms.ToString();
    }
}
