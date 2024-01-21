using UnityEngine;
using UnityEngine.UI;

public class LifeBarControler : MonoBehaviour
{
    private Image _lifeBar;
    private PlayerData _playerData;
    // Start is called before the first frame update


    void Start()
    {
        _playerData = GameObject.Find("Player").GetComponent<PlayerController>().PlayerDataSO;
        _lifeBar = gameObject.transform.GetChild(2).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        _lifeBar.fillAmount = _playerData.life/_playerData.maxlife;
    }
}
