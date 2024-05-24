using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTimeEventHandler : MonoBehaviour
{
    [SerializeField] private FadeScript fadeScriptMenuVideoCanvas;
    [SerializeField] private FadeScript fadeScriptMenuCanvas;

    private void Start()
    {
        //prepare canvas to be hidden
        fadeScriptMenuVideoCanvas.HideInstantUI();
        fadeScriptMenuCanvas.HideInstantUI();

        FunctionTimer.CreateTimer(() => fadeScriptMenuVideoCanvas.ShowUI(), 0.5f, "ShowMenu");
        FunctionTimer.CreateTimer(() => fadeScriptMenuCanvas.ShowUI(), 2f, "ShowMenu");

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
