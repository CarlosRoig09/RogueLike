using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Audio Manager is NULL");
            }
            return _instance;
        }
    }
    [SerializeField] private AudioClip _menu,_gamebattle,_game,_gameover,_shop;
    private AudioSource _aS;
    private bool _alreadyCallBatlleMusic;
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
        _alreadyCallBatlleMusic = false;
        _aS= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void ChangeOST(string scene)
    {
        switch (scene)
        {
            case "GameScene":
                _aS.clip = _game;
                break;
            case "GameOver":
                _aS.clip = _gameover;
                break;
            case "GameStart":
                _aS.clip = _menu;
                break;
            case "Combat":
                _aS.clip = _gamebattle;
                break;
            case "Shop":
                _aS.clip = _shop;
                break;
            default:
                break;
        }
        _aS.Play();
    }
}
