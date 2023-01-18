using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScenari : MonoBehaviour
{
    private int _id;
    public int Id
    {
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
        _player.transform.position = new Vector3(_door1.transform.position.x+0.2f,_door1.transform.position.y);
        _spawner.GetComponent<EnemyWaveControler>().CallWave(id,transform.position);
   }

 public void ScenariAlredyLoaded()
    {
        _player.transform.position = new Vector3(_door2.transform.position.x + 0.2f, _door1.transform.position.y);
    }

    public void CallOpenDoor()
    {
        OnOpenDoor();
    }
}
