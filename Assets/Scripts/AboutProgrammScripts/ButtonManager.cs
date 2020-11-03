using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void OnClickInfoButton()
    {
        Application.OpenURL("https://vk.com/maktrahhher");
    }

    public void OnClickBackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
