using UnityEngine;
using UnityEngine.SceneManagement;

public class ManiJol : MonoBehaviour
{
    public void PokaUshel()
    {
        Application.Quit();
    }

    public void VeseluhaAllOn()
    {
        SceneManager.LoadScene(1);
    }

    public void Backham()
    {
        SceneManager.LoadScene(0);
    }
}
