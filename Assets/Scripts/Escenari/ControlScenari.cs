using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScenari : MonoBehaviour
{
    private GameManager _startGame;
    [SerializeField]
    private List<GameObject> _escenari;

    private List<GameObject> _newOrder;
    public List<GameObject> Escenaris
    {
        get => _newOrder;
    }
    private int _scenariCount;
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
        _scenariCount = 0;
        _newOrder = new List<GameObject>();
        _eWC = gameObject.GetComponent<EnemyWaveControler>();
    }

    void CreateEscenaris()
    {
        bool isRepitive;
        int count = 0;
        while(count < _escenari.Count)
        {
            isRepitive = false;
            /*do
            {*/
                _newOrder.Add(_escenari[Random.Range(0, _escenari.Count)]);
               /* if(count > 0)
                    for (int i = count - 1; i < _escenari.Count&&!isRepitive; i--)
                    {
                       // if (_newOrder[count] == _newOrder[i])
                           // isRepitive = true;*/
                   // }
           // } while (isRepitive);
            count++;
        }
        _eWC.CreateWaves();
        LoadScenari(0);
    }

   public void LoadScenari(float door)
    {
        Transform previosposition = null;
        float addDistance = 0;
        string instantDoor = null;
       switch (door)
        {
            case 1:
                _scenariCount-=1;
                instantDoor = "Door2";
                break;
            case 2:
                 previosposition = ReturnALoadedScenari(GameObject.FindGameObjectsWithTag("scenari"),_scenariCount).transform;
                _scenariCount += 1;
                addDistance = 100;
                instantDoor = "Door1";
                break;
            case 0:
                _scenariCount = 0;
                addDistance = 0;
                previosposition = _newOrder[_scenariCount].transform;
                break;
        }
        if (_scenariCount >= 0 || _scenariCount < _newOrder.Count)
        {
            Debug.Log(_scenariCount);
            if (!SearchIfASceneariIsLoaded(GameObject.FindGameObjectsWithTag("scenari"), _scenariCount))
            {
                newScene = true;
                var newPosition = new Vector3(previosposition.position.x + addDistance, previosposition.position.y);
                _currentScenari = Instantiate(_newOrder[_scenariCount], newPosition, Quaternion.identity);
            }
            else
            {
                _currentScenari = ReturnALoadedScenari(GameObject.FindGameObjectsWithTag("scenari"), _scenariCount);
                _currentScenari.GetComponent<LoadScenari>().ScenariAlredyLoaded(instantDoor);
            }
            _currentScenari.GetComponent<LoadScenari>().Id = _scenariCount;
        }
        else
        {
            if (_scenariCount < 0)
                _scenariCount = 0;
            else if (_scenariCount >= _newOrder.Count)
                _scenariCount = _newOrder.Count - 1;
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
