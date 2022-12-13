using UnityEngine;
using UnityEngine.UI;
public class FinishGameText : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager.GameFinish == GameFinish.Win)
            gameObject.GetComponent<TMPro.TMP_Text>().text = "Congratulations, you win.";
        else
            gameObject.GetComponent<TMPro.TMP_Text>().text = "You lose.";
    }
}
