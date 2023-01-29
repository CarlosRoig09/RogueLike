using UnityEngine;
using UnityEngine.SceneManagement;
public enum Escenas
{
    StartScreen,
    GameScreen,
    GameOverScreen
}
public enum GameFinish
{
    Win,
    Lose
}
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Game Manager is NULL");
            }
            return _instance;
        }
    }
    public delegate void StartGame();
    public event StartGame OnStartGame;
    private Escenas _scene;
    private GameFinish _gameFinish;
    private ControlScenari _cS;
    private bool _calledStartGame;
    public GameFinish GameFinish
    {
        get => _gameFinish;
    }
    [SerializeField]
    private PlayerData _playerData;
    private float _puntuation;
    private GetPuntuation _controlPuntuation;
    public float Puntuation
    {
        get => _puntuation;
        set { _puntuation = value;
            _controlPuntuation.PuntuationChanged();
            }
    }
    private float _numberOfEnemyKilled;
    public float NumberOfEnemyKilled
    {
        get => _numberOfEnemyKilled;
    }
    private float _enemyKilledInCurrentRoom;
    private float _currentEnemy;
    private float _totalEnemy;
    private float _rooms;
    public float Rooms
    { 
        get => _rooms;
    }
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _calledStartGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeScene();
        if (_scene == Escenas.GameScreen)
        {
            if (!_calledStartGame)
            {
                _controlPuntuation = GameObject.Find("Puntuation").GetComponent<GetPuntuation>();
                _cS = GameObject.Find("Spawner").GetComponent<ControlScenari>();
                _puntuation = 0;
                _rooms = 0;
                _numberOfEnemyKilled = 0;
                _calledStartGame = true;
                OnStartGame();
            }
            if (_playerData.State == Life.Death)
            {
                _numberOfEnemyKilled += _enemyKilledInCurrentRoom;
                _gameFinish = GameFinish.Lose;
                SceneManager.LoadScene("GameOver");
            }
            if (GameObject.Find("Spawner").GetComponent<EnemyWaveControler>().ControlIfWaveIsFinished(out _currentEnemy, out _totalEnemy) && _cS.newScene)
            {
                GameObject.Find("Spawner").GetComponent<ControlScenari>().DoorOpens();
                _numberOfEnemyKilled += _enemyKilledInCurrentRoom;
                _rooms += 1;
            }
            else
                if (_numberOfEnemyKilled < _totalEnemy - _currentEnemy)
            _enemyKilledInCurrentRoom = _totalEnemy - _currentEnemy;
        }
        void ChangeScene()
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "GameScene":
                    _scene = Escenas.GameScreen;
                    break;
                case "GameOver":
                    _scene = Escenas.GameOverScreen;
                    _calledStartGame = false;
                    break;
                case "GameStart":
                    _scene = Escenas.StartScreen;
                    break;
                default:
                    break;
            }
        }
    }
}
