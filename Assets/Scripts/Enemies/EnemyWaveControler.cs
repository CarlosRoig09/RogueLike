using System.Collections.Generic;
using UnityEngine;
public enum WaveState
{
    CallWave,
    WaveEnded
}
public class EnemyWaveControler : MonoBehaviour, IGivePuntuation
{
    //Haré probablemente para variar un ScriptableObject de cada zona y sus Wave en un futuro.
    private List<EnemyWave> _waves;
   // [SerializeField]

    [SerializeField]
    private GameObject _turret;
    [SerializeField]
    private GameObject _kamikazee;
    private int _currentEnemy;
    private WaveState _waveState;
    private List<GameObject> _enemySpawned;
    [SerializeField]
    private Transform _player;
    private bool _activeWave;
    [SerializeField]
    private LayerMask _wall;
    private List<Vector3> _allPosiblePositions;
    public List<Vector3> AllPositions { get { return _allPosiblePositions;} }
    public WaveState WaveState
    {
        get => _waveState;
    }
    public List<EnemyWave> Waves
    {
        get { return _waves; }
        set { _waves = value; }
    }
    private static EnemyWaveControler _instance;

    public static EnemyWaveControler Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Spawner is NULL");
            }
            return _instance;
        }
    }
    void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
        {
            _instance = this;
        }
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
    public void CallWave(Vector3 scenariPosition,Vector3 scenariInitialPos, Vector3 scenariFinalPos,GameObject spawnDoor)
    {
        transform.position = scenariPosition;
        _currentEnemy = 0;
       var waves = _waves[0].CallWave();
        AllPosiblePositions(new Vector3(scenariInitialPos.x + scenariPosition.x, scenariInitialPos.y + scenariPosition.y), new Vector3(scenariFinalPos.x + scenariPosition.x, scenariFinalPos.y + scenariPosition.y));
        foreach (int kamiKazeeNumber in waves.KamikazeeNumber)
        {
            EnemySpawn(_kamikazee, kamiKazeeNumber, spawnDoor);
            _currentEnemy += kamiKazeeNumber;
        }
        foreach (int turretNumber in waves.TurretNumber)
        {
            EnemySpawn(_turret, turretNumber,spawnDoor);
            _currentEnemy += turretNumber;
        }
        _activeWave = true;
    }

    public bool ControlIfWaveIsFinished(out int currentEnemy, out int totalEnemy)
    {
        if (_activeWave)
        {
            CurrentEnemy(ref _currentEnemy);
            currentEnemy = _currentEnemy;
            totalEnemy = _enemySpawned.Count;
            if (_currentEnemy <= 0)
            {
                GivePuntuation(_enemySpawned.Count*10);
                _activeWave = false;
                //AudioManager.Instance.ChangeOST("GameScene");
                return true;
            }
        }
        currentEnemy = _currentEnemy;
        totalEnemy = _enemySpawned.Count;
        return false;
    }

    public void EnemySpawn(GameObject enemy, int number,GameObject spawnDoor)
    {
        for (int i = 0; i < number; i++)
            _enemySpawned.Add(ControlInstancePosition(enemy,spawnDoor));
    }
    public void CurrentEnemy(ref int currentEnemies)
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

    private GameObject ControlInstancePosition(GameObject enemy,GameObject spawnDoor)
    {
        for (int i = 0; i < _allPosiblePositions.Count;)
        {
           int y;
            int x;
            if ( (x=i) != (y = Random.Range(0, _allPosiblePositions.Count)))
            {
                i++;
                (_allPosiblePositions[x], _allPosiblePositions[y]) = (_allPosiblePositions[y], _allPosiblePositions[x]);
            }
       }
        for (int i = 0; i < _allPosiblePositions.Count; i++)
        {
                Vector3 enemyPosition = _allPosiblePositions[i];
                _allPosiblePositions.RemoveAt(i);
                var enemyInstance = Instantiate(enemy, enemyPosition, Quaternion.identity);
            if (GameObjectInThatPosition(enemyInstance,spawnDoor))
            return enemyInstance;
            else
                Destroy(enemyInstance);
        }
        return Instantiate(enemy, transform.position, Quaternion.identity);
    }

    private bool GameObjectInThatPosition(GameObject enemy,GameObject spwanDoor)
    {
        if (!Physics2D.Raycast(enemy.transform.position,spwanDoor.transform.position,2f,11))
            return true;
            return false;
    }

    private List<Vector3> AllPosiblePositions(Vector3 firstPosition, Vector3 lastPosition)
    {
        var differentToWall = false;
        _allPosiblePositions = new List<Vector3>();
        Vector3 actualPosition = firstPosition;
        do
        {
            if (Physics2D.Raycast(transform.position, actualPosition, (actualPosition-transform.position).magnitude, _wall))
            {
                if (differentToWall || actualPosition.x >= lastPosition.x)
                {
                    actualPosition = new Vector3(firstPosition.x, actualPosition.y - 1f);
                    differentToWall = false;
                }
            }
            else
            {
                _allPosiblePositions.Add(actualPosition);
                differentToWall = true;
            }
            actualPosition = new Vector3(actualPosition.x + 1f, actualPosition.y);
        } while (actualPosition.y>=lastPosition.y);
        return _allPosiblePositions;
    }

    public void GivePuntuation(int Puntuation)
    {
        GameManager.Instance.Puntuation += Puntuation;
    }
}
