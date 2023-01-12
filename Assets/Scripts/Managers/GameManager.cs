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
    private bool _calledStartGame;
    public GameFinish GameFinish
    {
        get => _gameFinish;
    }
    [SerializeField]
    private PlayerData _playerData;
    public float numberOfBulletsWeapon1;
    public float numberOfBulletsWeapon2;
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
            if (!_calledStartGame) {
                OnStartGame();
                _calledStartGame = true;
            }
            if (_playerData.State == Life.Death)
            {
                _gameFinish = GameFinish.Lose;
                SceneManager.LoadScene("GameOver");
            }
            if (GameObject.Find("Spawner").GetComponent<EnemyWaveControler>().WaveState == WaveState.WaveEnded)
            {
                _gameFinish = GameFinish.Win;
                SceneManager.LoadScene("GameOver");
            }
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
