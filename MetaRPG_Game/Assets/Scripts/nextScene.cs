using UnityEngine;
using UnityEngine.SceneManagement;

public class nextScene : MonoBehaviour
{
    public void progress()
    {
        SceneManager.LoadScene(1);
    }
}
