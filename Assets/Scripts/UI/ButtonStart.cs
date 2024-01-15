using UnityEngine.SceneManagement;
using UnityEngine;
public class ButtonStart : MonoBehaviour
{
    [SerializeField]
    private string _scene;
    public void TaskOnClick()
    {
        AudioManager.instance.StopAllThemes();
        AudioManager.instance.Play("MenuTheme");
        SceneManager.LoadScene(_scene, LoadSceneMode.Single);
    }
   

}
