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
    private int _scenariCountX;
    private int _scenariCountY;
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
                _newOrder.Add(_escenari[Random.Range(0, _escenari.Count)]);
            /*   for (int y = 0;y < _newOrder; y++)
       {
         for (int x = 0; x < length; x++)
         {

         }
     }*/
            count++;
        }
        _eWC.CreateWaves();
        LoadScenari(0);
    }

   public void LoadScenari(float door)
    {
        Transform previosposition = null;
        string instantDoor = null;
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
                previosposition = _newOrder[_scenariCountX].transform;
                break;
        }
        if (_scenariCountX >= 0 || _scenariCountX < _newOrder.Count)
        {
            Debug.Log(_scenariCountX);
            if (!SearchIfASceneariIsLoaded(GameObject.FindGameObjectsWithTag("scenari"), _scenariCountX))
            {
                if (door != 0)
                {
                    addDistance = 100;
                    previosposition = ReturnALoadedScenari(GameObject.FindGameObjectsWithTag("scenari"), _scenariCountX).transform;
                }
                newScene = true;
                var newPosition = new Vector3(previosposition.position.x + addDistance, previosposition.position.y);
                _currentScenari = Instantiate(_newOrder[_scenariCountX], newPosition, Quaternion.identity);
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
            else if (_scenariCountX >= _newOrder.Count)
                _scenariCountX = _newOrder.Count - 1;
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
