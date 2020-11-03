using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class ButtonManagement : MonoBehaviour
{
    public GameObject exitPanel;
    public Ease moveEase;

    private float moveDuration = 1f;

    private Button newGame;
    private Button leaderBord;
    private Button aboutProgramm;
    private Button exitGame;
    private Button stayButton;
    private Button exitButton;

    public void Awake()
    {
        newGame = GameObject.Find("NewGameButton").GetComponent<Button>();
        leaderBord = GameObject.Find("LeaderbordButton").GetComponent<Button>();
        aboutProgramm = GameObject.Find("AboutProgrammButton").GetComponent<Button>();
        exitGame = GameObject.Find("ExitButton").GetComponent<Button>();
    }

    public void OnClickNewGameButton () //Метод, открывающий сцену с геймплеем
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickLeaderbordButton() //Метод, открывающий сцену с таблицей лидеров
    {
        SceneManager.LoadScene("Leaderbord");
    }

    public void OnClickAboutProgrammButton() //Метод, открывающий сцену с описанием программы
    {
        SceneManager.LoadScene("AboutProgramm");
    }

    public void OnClickExitGameButton() //Метод, открывающий панель с кнопками "Остаться" и "Выйти"
    {
        OpenExitPanel(exitPanel.GetComponent<RectTransform>(), exitPanel);
        newGame.interactable = false;
        leaderBord.interactable = false;
        aboutProgramm.interactable = false;
        exitGame.interactable = false;
    }

    public void OnClickStayButton()
    {
        StartCoroutine(CloseExitPanel(exitPanel.GetComponent<RectTransform>(), exitPanel));
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }

    private void OpenExitPanel(RectTransform rect, GameObject go) //Метод с анимацией открывания панели выхода
    {
        go.SetActive(true);
        rect.DOAnchorPosX( 0f, moveDuration).SetEase(moveEase);
    }
    
    IEnumerator CloseExitPanel(RectTransform rect, GameObject go) //Метод с анимацией закрытия панели выхода
    {
        rect.DOAnchorPosX( -2000f, moveDuration).SetEase(moveEase);
        yield return new WaitForSeconds(1f);
        go.SetActive(false);
        newGame.interactable = true;
        leaderBord.interactable = true;
        aboutProgramm.interactable = true;
        exitGame.interactable = true;
    }
}
