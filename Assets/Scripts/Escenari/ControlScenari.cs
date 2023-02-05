using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScenari : MonoBehaviour
{
    private GameManager _startGame;
    [SerializeField]
    private List<GameObject> _escenari;
    public List<GameObject> Escenaris
    {
        get => _escenari;
    }
    private int _scenariCountX;
    [SerializeField]
    private Transform _player;
    private GameObject _currentScenari;
    private EnemyWaveControler _eWC;
    public bool newScene;
    // Start is called before the first frame update
    void Start()
    {
        newScene = false;
        _startGame = GameManager.Instance;
        _startGame.OnStartGame += CreateEscenaris;
        _scenariCountX = 0;
        _eWC = gameObject.GetComponent<EnemyWaveControler>();
    }

    void CreateEscenaris()
    {
        for (int i = 0; i < Escenaris.Count;)
        {
            int y;
            int x;
            if ((x = Random.Range(0, _escenari.Count)) != (y = Random.Range(0, _escenari.Count)))
            {
                i++;
                (_escenari[x], _escenari[y]) = (_escenari[y], _escenari[x]);
            }
        }
        _eWC.CreateWaves();
        LoadScenari(0);
    }

   public void LoadScenari(float door)
    {
        Transform previosposition = null;
        string instantDoor = null;
        string[] noInstanceDoor = new string[1];
        float addDistance = 0;
        switch (door)
        {
            case 1:
                _scenariCountX -= 1;
                instantDoor = "Door2";
                break;
            case 2:
                _scenariCountX += 1;
                instantDoor = "Door1";
                break;
            case 0:
                _scenariCountX = 0;
                addDistance = 0;
                instantDoor = "Door1";
                previosposition = _escenari[_scenariCountX].transform;
                break;
        }
        if (_scenariCountX == 0)
            noInstanceDoor[0] = "Door1";
        if (_scenariCountX == _escenari.Count - 1)
            noInstanceDoor[0] = "Door2";
        if (_scenariCountX >= 0 || _scenariCountX < _escenari.Count)
        {
            Debug.Log(_scenariCountX);
            if (!SearchIfASceneariIsLoaded(GameObject.FindGameObjectsWithTag("scenari"), _scenariCountX))
            {
                if (door != 0)
                {
                    addDistance = 100;
                    previosposition = ReturnALoadedScenari(GameObject.FindGameObjectsWithTag("scenari"), _scenariCountX-1).transform;
                }
                newScene = true;
                var newPosition = new Vector3(previosposition.position.x + addDistance, previosposition.position.y);
                _currentScenari = Instantiate(_escenari[_scenariCountX], newPosition, Quaternion.identity);
                _currentScenari.GetComponent<LoadScenari>().NoShowDoors = noInstanceDoor;
            }
            else
            {
                _currentScenari = ReturnALoadedScenari(GameObject.FindGameObjectsWithTag("scenari"), _scenariCountX);
                _currentScenari.GetComponent<LoadScenari>().ScenariAlredyLoaded(instantDoor);
            }
            _currentScenari.GetComponent<LoadScenari>().Id = _scenariCountX;
        }
        else
        {
            if (_scenariCountX < 0)
                _scenariCountX = 0;
            else if (_scenariCountX >= _escenari.Count)
                _scenariCountX = _escenari.Count - 1;
        }
    }

    private bool SearchIfASceneariIsLoaded(GameObject[] Loaded, int WantToLoad)
    {
        foreach (var load in Loaded)
        {
            if (load.GetComponent<LoadScenari>().Id == WantToLoad)
            {
                return true;
            }
        }
        return false;
    }

    private GameObject ReturnALoadedScenari(GameObject[] Loaded, int WantToLoad)
    {
        foreach (var load in Loaded)
        {
            if (load.GetComponent<LoadScenari>().Id == WantToLoad)
            {
                return load;
            }
        }
        return null;
    }

    public void DoorOpens()
    {
        _currentScenari.GetComponent<LoadScenari>().CallOpenDoor();
    }

    private void OnDisable()
    {
        _startGame.OnStartGame -= CreateEscenaris;
    }
}
