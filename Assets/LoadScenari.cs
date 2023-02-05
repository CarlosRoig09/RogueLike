using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScenari : MonoBehaviour
{
    private int _id;
    public int Id
    {
        get { return _id; }
        set { _id = value;}
    }
    [SerializeField]
    private GameObject _door1;
    [SerializeField]
    private GameObject _door2;
    [SerializeField]
    private GameObject _door3;
    [SerializeField]
    private GameObject _door4;
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private GameObject _spawner;
    public delegate void OpenDoor(int id);
    public event OpenDoor OnOpenDoor;
    [SerializeField]
    private Vector3 ScenariStartPos;
    [SerializeField]
    private Vector3 ScenariEndPos;
    private string[] _noShowDoors;
    public string[] NoShowDoors
    {
        get { return _noShowDoors; }
        set { _noShowDoors = value;}
    }

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _spawner = GameObject.Find("Spawner");
        ScenariLoaded(_id);
        foreach (string door in _noShowDoors)
        {
            NoInstantDoor(door);
        }
    }

    private void NoInstantDoor(string door)
    {
        switch (door)
        {
            case "Door1":
                Destroy(_door1);
                break;
            case "Door2":
                Destroy(_door2);
                break;
            case "Door3":
                Destroy(_door3);
                break;
            case "Door4":
                Destroy(_door4);
                break;
        }
    }
  private void ScenariLoaded(int id)
   {
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _player.transform.position = new Vector3(_door1.transform.position.x+0.2f,_door1.transform.position.y);
        if (transform.GetChild(0).CompareTag("combat"))
            _spawner.GetComponent<EnemyWaveControler>().CallWave(transform.position, ScenariStartPos,ScenariEndPos);
        else
            CallOpenDoor();
   }

 public void ScenariAlredyLoaded(string door)
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
         switch (door)
        {
            case "Door1":
                _player.transform.position = new Vector3(_door1.transform.position.x + 5f, _door2.transform.position.y);
                break;
            case "Door2":
                _player.transform.position = new Vector3(_door2.transform.position.x -5f, _door2.transform.position.y);
                break;
            case "Door3":
                _player.transform.position = new Vector3(_door3.transform.position.x + 5f, _door3.transform.position.y);
                break;
            case "Door4":
                _player.transform.position = new Vector3(_door4.transform.position.x + 5f, _door4.transform.position.y);
                break;
            default:
                break;
        }
    }

    public void CallOpenDoor()
    {
        _spawner.GetComponent<ControlScenari>().newScene = false;
        OnOpenDoor(_id);
    }
}
