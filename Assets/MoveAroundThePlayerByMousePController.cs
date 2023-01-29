using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum HandState
{
    MoveByMouse,
    MoveByRotateAnim,
}
public class MoveAroundThePlayerByMousePController : MonoBehaviour
{
    [SerializeField]
    private float _nearThePlayer;
    private GameObject _player;
    private HandState _hS;
    private float _roationSpeed;
    public float RoationSpeed { set { _roationSpeed = value; } }
    public HandState HS
    {
        get { return _hS; }
        set { _hS = value; }
    }
    void Start()
    {
        _player =GameObject.Find("Player");
        _hS = HandState.MoveByMouse;
    }
    // Update is called once per frame
    void Update()
    {
        switch (_hS)
        {
            case HandState.MoveByMouse:
                RotationZ(_player.GetComponent<PlayerController>().VectorMousePlayerAngle());
                break;
            case HandState.MoveByRotateAnim:
                RotateAround(transform.parent.transform.position);
                break;
        }
    }

    void RotationZ(Vector3 vector)
    {
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y,angle);
        float xPosition = Mathf.Cos(Mathf.Deg2Rad * angle);
        float yPosition = Mathf.Sin(Mathf.Deg2Rad * angle);
        transform.localPosition = new Vector3(xPosition/_nearThePlayer, yPosition/_nearThePlayer, 0);
    }

    void RotateAround(Vector3 pointToRotate)
    {
        transform.RotateAround(pointToRotate, new Vector3(0, 0, 1), _roationSpeed * Time.fixedDeltaTime);
    }
}
