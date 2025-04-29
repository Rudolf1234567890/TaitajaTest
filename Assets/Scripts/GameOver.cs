using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
   
    public void RestartGame()
    {
        
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
    }
    
    public void MainMenu()
    {
        
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }       
}
