using System.Collections.Generic;
using UnityEngine;
public enum WaveState
{
    CallWave,
    WaveEnded
}
public class EnemyWaveControler : MonoBehaviour
{
    //Haré probablemente para variar un ScriptableObject de cada zona y sus Wave en un futuro.
    private GameManager _startGame;
    private List<EnemyWave> _waves;
   // [SerializeField]

    [SerializeField]
    private GameObject _turret;
    [SerializeField]
    private GameObject _kamikazee;
    private float _currentEnemy;
    private WaveState _waveState;
    private List<GameObject> _enemySpawned;
    private float _numberOfWaves;
    [SerializeField]
    private Transform _player;
    public WaveState WaveState
    {
        get => _waveState;
    }
    // Start is called before the first frame update
    void Start()
    {
        _startGame = GameManager.Instance;
        _startGame.OnStartGame += CreateWaves;
    }

    void Update()
    {
    }

    private void CreateWaves()
    {
        int numberOfWave = 0;
        foreach (var scenari in gameObject.GetComponent<ControlScenari>().Escenaris)
        {
            if (scenari.CompareTag("combat"))
                numberOfWave += 1;
        }
        _waves = new List<EnemyWave>(numberOfWave);
        foreach (var wave in _waves)
        {
           // wave = new EnemyWave(Random.Range(1,3),Random.Range(),);
        }
    }
    public void CallWave()
    {
    }

    private void ControlIfWaveIsFinished(WaveState nextWave)
    {
        CurrentEnemy(ref _currentEnemy);
        if (_currentEnemy <= 0)
            _waveState = nextWave;
    }

    public void EnemySpawn(GameObject enemy, float number)
    {
        for (int i = 0; i < number; i++)
            _enemySpawned.Add(ControlInstancePosition(enemy));
    }
    public void CurrentEnemy(ref float currentEnemies)
    {
        foreach (var enemy in _enemySpawned)
        {
                if(enemy!=null)
                if (enemy.GetComponent<Enemy>().State == Life.Death)
                {
                    currentEnemies -= 1;
                     enemy.GetComponent<Enemy>().Destroyed();
                }
        }
    }

    private GameObject ControlInstancePosition(GameObject enemy)
    {
        Vector3 position;
        do
        {
            position = new Vector3(Random.Range(-32.4f, 12.62f), Random.Range(-8.75f, 10.42f));
        } while (GameObjectInThatPosition(position));
        return Instantiate(enemy, position, Quaternion.identity);
    }

    private bool GameObjectInThatPosition(Vector3 position)
    {
        if (Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Camera.main.transform.position), position) || Physics2D.Raycast(position, _player.position, 100, 6))
            return true;
            return false;
    }
}
