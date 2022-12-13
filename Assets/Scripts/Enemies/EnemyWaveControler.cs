using System.Collections.Generic;
using UnityEngine;
public enum WaveState
{
    CallFirstWave,
    FirstWave,
    CallSecondWave,
    SecondWave, 
    CallThirtWave,
    ThirthWave,
    WaveEnded
}
public class EnemyWaveControler : MonoBehaviour
{
    //Haré probablemente para variar un ScriptableObject de cada zona y sus Wave en un futuro.
    [SerializeField]
    private float _firstWaveTurret;
    [SerializeField]
    private float _firstWaveKamikazee;
    [SerializeField]
    private float _secondWaveTurret;
    [SerializeField]
    private float _secondWaveKamikazee;
    [SerializeField]
    private float _thirtWaveTurret;
    [SerializeField]
    private float _thirtWaveKamikazee;
    [SerializeField]
    private GameObject _turret;
    [SerializeField]
    private GameObject _kamikazee;
    private List<EnemyWave> _enemyWaves;
    private float _currentEnemy;
    private WaveState _waveState;
    private List<GameObject> _enemySpawned;
    public WaveState WaveState
    {
        get => _waveState;
    }
    // Start is called before the first frame update
    void Start()
    {
        _enemyWaves = new List<EnemyWave> { new EnemyWave(1, _firstWaveKamikazee, _firstWaveTurret), new EnemyWave(2, _secondWaveKamikazee, _secondWaveTurret), new EnemyWave(3, _thirtWaveKamikazee, _thirtWaveTurret) };
        _waveState = WaveState.CallFirstWave;
        _enemySpawned = new List<GameObject>();
    }

    void Update()
    {
        switch (_waveState)
        {
            case WaveState.CallFirstWave:
                Debug.Log("Wave1");
                CallWave(WaveState.FirstWave,0);
                break;
            case WaveState.FirstWave:
                ControlIfWaveIsFinished(WaveState.CallSecondWave);
                break;
            case WaveState.CallSecondWave:
                Debug.Log("Wave2");
               CallWave(WaveState.SecondWave,1);
                break;
            case WaveState.SecondWave:
                ControlIfWaveIsFinished(WaveState.CallThirtWave);
                break;
            case WaveState.CallThirtWave:
                Debug.Log("Wave3");
               CallWave(WaveState.ThirthWave,2);
                break;
            case WaveState.ThirthWave:
                ControlIfWaveIsFinished(WaveState.WaveEnded);
                break;
        }
    }

    private void CallWave(WaveState newState, int waveNumber)
    {
        //No hay muchas más variantes, más allá de sumar o restar enemigos. 
        float extraKamikazee = 0;
        float extraTurret = 0;
        if (Random.Range(1,100)<=20)
        {
            extraKamikazee = Random.Range(1, 2);
            extraTurret = Random.Range(1, 2);
        }
        EnemySpawn(_kamikazee, _enemyWaves[waveNumber].KamikazeeNumber + extraKamikazee);
        EnemySpawn(_turret, _enemyWaves[waveNumber].TurretNumber + extraTurret);
        _currentEnemy = _enemyWaves[waveNumber].KamikazeeNumber + _enemyWaves[waveNumber].TurretNumber + extraKamikazee + extraTurret;
        _waveState = newState;
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
        var hitColliders = Physics.OverlapSphere(position, 2);
        if (hitColliders.Length > 0)
            return true;
        return false;
    }
}
