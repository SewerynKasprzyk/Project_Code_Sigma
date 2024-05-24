using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTest : MonoBehaviour
{

    private void Start()
    {
        FunctionTimer.CreateTimer(TestingAction, 3f, "Timer");
        FunctionTimer.CreateTimer(TestingAction_2, 4f, "Timer_2");

        FunctionTimer.StopTimer("Timer");
    }

    private void TestingAction()
    {
        Debug.Log("Timer");
    }

    private void TestingAction_2()
    {
        Debug.Log("Timer_2");
    }

}
