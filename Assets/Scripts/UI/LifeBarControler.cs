using UnityEngine;
using UnityEngine.UI;

public class LifeBarControler : MonoBehaviour
{
    private Slider _lifeBar;
    private PlayerData _playerData;
    // Start is called before the first frame update


    void Start()
    {
        _playerData = GameObject.Find("Player").GetComponent<PlayerController>().PlayerDataSO;
        _lifeBar = gameObject.GetComponent<Slider>();
        _lifeBar.maxValue = _playerData.maxlife;
        _lifeBar.value = _lifeBar.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        _lifeBar.value = _playerData.life;
    }
}
