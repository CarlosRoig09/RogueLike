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
    private List<EnemyWave> _waves;
   // [SerializeField]

    [SerializeField]
    private GameObject _turret;
    [SerializeField]
    private GameObject _kamikazee;
    private float _currentEnemy;
    private WaveState _waveState;
    private List<GameObject> _enemySpawned;
    [SerializeField]
    private Transform _player;
    private bool _activeWave;
    public WaveState WaveState
    {
        get => _waveState;
    }
    // Start is called before the first frame update
    void Start()
    {
        _activeWave = false;
        _enemySpawned = new List<GameObject>();
    }
    public void CreateWaves()
    {
        Debug.Log("CreatedWaves");
        int numberOfWave = 0;
        _waves = new List<EnemyWave>();
        foreach (var scenari in gameObject.GetComponent<ControlScenari>().Escenaris)
        {
            if (scenari.transform.GetChild(0).CompareTag("combat"))
            {
                numberOfWave += 1;
                var turrets = new float[numberOfWave];
                var kamikazes = new float[numberOfWave];
                for (int i = 0; i < numberOfWave; i++)
                {
                    turrets[i] = Random.Range(numberOfWave, numberOfWave * 2) / numberOfWave;
                    kamikazes[i] = Random.Range(numberOfWave, numberOfWave * 2) / numberOfWave;
                }
                _waves.Add(new EnemyWave(numberOfWave,kamikazes,turrets));
            }
        }
    }
    public void CallWave(int currentSala, Vector3 scenariPosition)
    {
        _currentEnemy = 0;
       var waves = _waves[currentSala].CallWave();
        foreach (var kamiKazeeNumber in waves.KamikazeeNumber)
        {
            EnemySpawn(_kamikazee, kamiKazeeNumber,scenariPosition);
            _currentEnemy += kamiKazeeNumber;
        }
        foreach (var turretNumber in waves.TurretNumber)
        {
            EnemySpawn(_turret, turretNumber,scenariPosition);
            _currentEnemy += turretNumber;
        }
        _activeWave = true;
    }

    public bool ControlIfWaveIsFinished()
    {
        if (_activeWave)
        {
            CurrentEnemy(ref _currentEnemy);
            if (_currentEnemy <= 0)
            {
                _activeWave = false;
                return true;
            }
        }
        return false;
    }

    public void EnemySpawn(GameObject enemy, float number, Vector3 scenariPosition)
    {
        for (int i = 0; i < number; i++)
            _enemySpawned.Add(ControlInstancePosition(enemy,scenariPosition));
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

    private GameObject ControlInstancePosition(GameObject enemy, Vector3 scenariPosition)
    {
        Vector3 position;
        do
        {
            position = new Vector3(Random.Range(-32.4f, 12.62f) + scenariPosition.x, Random.Range(-8.75f, 10.42f)+scenariPosition.y);
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
