using UnityEngine;
using UnityEngine.UI;

public class LifeBarControler : MonoBehaviour
{
    private Slider _lifeBar;
    [SerializeField]
    private PlayerData playerData;
    // Start is called before the first frame update


    void Start()
    {
        _lifeBar = gameObject.GetComponent<Slider>();
        _lifeBar.maxValue = playerData.maxlife;
        _lifeBar.value = _lifeBar.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        _lifeBar.value = playerData.life;
    }
}
