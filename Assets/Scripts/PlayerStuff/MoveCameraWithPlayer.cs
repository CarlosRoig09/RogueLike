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
    private void LateUpdate()
    {
        transform.position = new Vector2(_player.position.x + 0.5f, _player.position.y + 0.5f);

    }
}
