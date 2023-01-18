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
    private List<GameObject> _newOrder;
    private int _scenariCount;
    [SerializeField]
    private Transform _player;
    private GameObject _currentScenari;
    private EnemyWaveControler _eWC;
    // Start is called before the first frame update
    void Awake()
    {
        _startGame = GameManager.Instance;
        _startGame.OnStartGame += CreateEscenaris;
        _scenariCount = 0;
        _newOrder = new List<GameObject>();
        _eWC = gameObject.GetComponent<EnemyWaveControler>();
    }

    void CreateEscenaris()
    {
        Debug.Log("CreatedScenary");
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
        switch (door)
        {
            case 1:
                _scenariCount-=1;
                Debug.Log(_scenariCount);
                _currentScenari = _newOrder[_scenariCount];
              _currentScenari.GetComponent<LoadScenari>().ScenariAlredyLoaded();
                break;
            case 2:
                var previosposition = _newOrder[_scenariCount].transform;
                _scenariCount += 1;
                Debug.Log(_scenariCount);
                var newPosition = new Vector3(previosposition.position.x + 100,previosposition.position.y);
            _currentScenari = Instantiate(_newOrder[_scenariCount], newPosition, Quaternion.identity);
                break;
            case 0:
                _scenariCount = 0;
                Debug.Log(_scenariCount);
                _currentScenari = Instantiate(_newOrder[0],new Vector3(0,0),Quaternion.identity);
                break;
        }
        _currentScenari.GetComponent<LoadScenari>().Id = _scenariCount;
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
