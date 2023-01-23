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
    private Transform _player;
    [SerializeField]
    private GameObject _spawner;
    public delegate void OpenDoor();
    public event OpenDoor OnOpenDoor;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _spawner = GameObject.Find("Spawner");
        ScenariLoaded(_id);
    }
  private void ScenariLoaded(int id)
   {
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _player.transform.position = new Vector3(_door1.transform.position.x+0.2f,_door1.transform.position.y);
        _spawner.GetComponent<EnemyWaveControler>().CallWave(id,transform.position);
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
            default:
                break;
        }
    }

    public void CallOpenDoor()
    {
        _spawner.GetComponent<ControlScenari>().newScene = false;
        OnOpenDoor();
    }
}
