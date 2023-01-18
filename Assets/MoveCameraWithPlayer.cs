using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraWithPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform _player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(_player.position.x, _player.position.y, _player.position.z);
    }
}
