using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScenari : MonoBehaviour
{
    [SerializeField]
    private GameObject _door1;
    [SerializeField]
    private GameObject _dorr2;
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private GameObject _spawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ScenariLoaded()
    {
        _player.transform.position = _door1.transform.position;
        _spawner.GetComponent<EnemyWaveControler>().CallWave();
    }
}
