using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        StartCoroutine(DelayedPlay());
    }

    public void QuitGame()
    {
        StartCoroutine(DelayedQuit());
    }

    private IEnumerator DelayedPlay()
    {
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("Main");
    }

    private IEnumerator DelayedQuit()
    {
        yield return new WaitForSeconds(0.4f);
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
