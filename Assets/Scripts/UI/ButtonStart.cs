using UnityEngine.SceneManagement;
using UnityEngine;
public class ButtonStart : MonoBehaviour
{
    [SerializeField]
    private string _scene;
    public void TaskOnClick()
    {
        SceneManager.LoadScene(_scene, LoadSceneMode.Single);
    }
   

}
