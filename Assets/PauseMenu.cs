using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public List<Button> buttons = new List<Button>();
    private bool isPaused = false;
    private int selectedIndex = -1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }

        if (isPaused)
        {
            CheckMouseHover();
        }
    }

    void CheckMouseHover()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            RectTransform rt = buttons[i].GetComponent<RectTransform>();

            if (RectTransformUtility.RectangleContainsScreenPoint(rt, Input.mousePosition))
            {
                if (selectedIndex != i)
                {
                    if (selectedIndex != -1)
                        ResetButton(selectedIndex);

                    selectedIndex = i;
                    EnlargeButton(i);
                }
            }
            else
            {
                if (selectedIndex == i)
                {
                    ResetButton(i);
                    selectedIndex = -1;
                }
            }
        }
    }

    void EnlargeButton(int index)
    {
        RectTransform rt = buttons[index].GetComponent<RectTransform>();
        rt.localScale = Vector3.one * 1.1f;
        rt.localPosition += new Vector3(10f, 0f, 0f);
    }

    void ResetButton(int index)
    {
        RectTransform rt = buttons[index].GetComponent<RectTransform>();
        rt.localScale = Vector3.one;
        rt.localPosition -= new Vector3(10f, 0f, 0f);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
