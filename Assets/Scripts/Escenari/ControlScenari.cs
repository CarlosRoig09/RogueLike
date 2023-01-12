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
    // Start is called before the first frame update
    void Awake()
    {
        _startGame = GameManager.Instance;
        _startGame.OnStartGame += CreateEscenaris;
        _scenariCount = 0;
    }

    void CreateEscenaris()
    {
        bool isRepitive;
        int count = 0;
        while(count < _escenari.Count)
        {
            isRepitive = false;
            do
            {
                _newOrder[count] = _escenari[Random.Range(0, _escenari.Count)];
                if(count > 0)
                    for (int i = count - 1; i < _escenari.Count&&!isRepitive; i--)
                    {
                        if (_newOrder[count] == _newOrder[i])
                            isRepitive = true;
                    }
            } while (isRepitive);
            count++;
        }
        LoadScenari(0);
    }

    void LoadScenari(float door)
    {
        switch (door)
        {
            case 1:
                _scenariCount-=1;
                break;
            case 2:
                var previosposition = _newOrder[_scenariCount].transform;
                _scenariCount += 1;
                var newPosition = previosposition.position + previosposition.localScale;
                Instantiate(_newOrder[_scenariCount], newPosition, Quaternion.identity);
                break;
            case 0:
                _scenariCount = 0;
                Instantiate(_newOrder[0],new Vector3(0,0),Quaternion.identity);
                break;
        }
    }

    private void OnDisable()
    {
        _startGame.OnStartGame -= CreateEscenaris;
    }
}
