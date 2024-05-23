using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeEventHandler : MonoBehaviour
{
    [SerializeField] private FadeScript fadeScriptVideoCanvas;
    [SerializeField] private FadeScript fadeScriptLogoCanvas;
    [SerializeField] private FadeScript fadeScriptGameNameCanvas;

    private void Start()
    {
        FunctionTimer.CreateTimer(() => fadeScriptVideoCanvas.ShowUI(), 0f, "ShowIntro");

        FunctionTimer.CreateTimer(() => fadeScriptLogoCanvas.ShowUI(), 3f, "ShowLogo");
        FunctionTimer.CreateTimer(() => fadeScriptLogoCanvas.HideUI(), 7f, "HideLogo");

        FunctionTimer.CreateTimer(() => fadeScriptGameNameCanvas.ShowUI(), 9f, "ShowGameName");
        FunctionTimer.CreateTimer(() => fadeScriptGameNameCanvas.HideUI(), 13f, "HideGameName");

        FunctionTimer.CreateTimer(() => fadeScriptVideoCanvas.HideUI(), 15f, "HideIntro");

        FunctionTimer.CreateTimer(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1), 16f, "MenuSceneChange");

        //FunctionTimer.StopTimer("Timer");
    }

    private void TestingAction()
    {
        //Debug.Log("Timer");
    }

    private void TestingAction_2()
    {
        //Debug.Log("Timer_2");
    }

}
