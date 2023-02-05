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
    private Vector3[] _allPosiblePositions;
    public Vector3[] AllPositions { get { return _allPosiblePositions;} }
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
    public void CallWave(Vector3 scenariPosition,Vector3 scenariInitialPos, Vector3 scenariFinalPos)
    {
        AudioManager.Instance.ChangeOST("Combat");
        transform.position = scenariPosition;
        _currentEnemy = 0;
       var waves = _waves[0].CallWave();
        foreach (int kamiKazeeNumber in waves.KamikazeeNumber)
        {
            EnemySpawn(_kamikazee, kamiKazeeNumber,scenariPosition,scenariInitialPos,scenariFinalPos);
            _currentEnemy += kamiKazeeNumber;
        }
        foreach (int turretNumber in waves.TurretNumber)
        {
            EnemySpawn(_turret, turretNumber,scenariPosition,scenariInitialPos,scenariFinalPos);
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
                AudioManager.Instance.ChangeOST("GameScene");
                return true;
            }
        }
        currentEnemy = _currentEnemy;
        totalEnemy = _enemySpawned.Count;
        return false;
    }

    public void EnemySpawn(GameObject enemy, int number, Vector3 scenariPosition, Vector3 scenariInitialPos, Vector3 scenariFinalPos)
    {
        for (int i = 0; i < number; i++)
            _enemySpawned.Add(ControlInstancePosition(enemy,scenariPosition,scenariInitialPos,scenariFinalPos));
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

    private GameObject ControlInstancePosition(GameObject enemy, Vector3 scenariPosition, Vector3 scenariInitialPos, Vector3 scenariFinalPos)
    {
       var AllPositions = AllPosiblePositions(new Vector3(scenariInitialPos.x+scenariPosition.x,scenariInitialPos.y+scenariPosition.y),new Vector3(scenariFinalPos.x+scenariPosition.x,scenariFinalPos.y+scenariPosition.y));
        // do
        //{
        for (int i = 0; i < AllPositions.Length;)
        {
            int y;
            int x;
            if ((x = Random.Range(0, AllPositions.Length)) != (y = Random.Range(0, AllPositions.Length)))
            {
                i++;
                (AllPositions[x], AllPositions[y]) = (AllPositions[y], AllPositions[x]);
            }
        }
        for (int i = 0; i < AllPositions.Length; i++)
        {
            if (GameObjectInThatPosition(AllPositions[i]))
                return Instantiate(enemy, AllPositions[i], Quaternion.identity);
        }
        return Instantiate(enemy, transform.position, Quaternion.identity);
    }

    private bool GameObjectInThatPosition(Vector3 position)
    {
        if ((_player.transform.position-position).magnitude>3&&!Physics2D.Raycast(transform.position,position,(position-transform.position).magnitude))
            return true;
            return false;
    }

    private Vector3[] AllPosiblePositions(Vector3 firstPosition, Vector3 lastPosition)
    {
        var differentToWall = false;
        _allPosiblePositions = new Vector3[0];
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
                System.Array.Resize(ref _allPosiblePositions, _allPosiblePositions.Length + 1);
                _allPosiblePositions[^1] = actualPosition;
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
