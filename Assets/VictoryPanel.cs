using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class VictoryPanel : MonoBehaviour
{
    public List<Button> buttons = new List<Button>();
    public AudioClip hoverSound;
    private AudioSource audioSource;
    private int selectedIndex = -1;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        buttons[0].onClick.AddListener(PlayAgain);
        buttons[1].onClick.AddListener(MainMenu);
        buttons[2].onClick.AddListener(QuitGame);
    }

    void Update()
    {
        CheckMouseHover();
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
                    PlayHoverSound();
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

    void PlayHoverSound()
    {
        if (hoverSound && audioSource)
            audioSource.PlayOneShot(hoverSound);
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
