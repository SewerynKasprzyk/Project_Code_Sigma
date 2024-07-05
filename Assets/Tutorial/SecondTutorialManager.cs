using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondTutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex = 0;
    public GameObject enemy;
    public GameObject enemy2;
    private bool isTransitioning = false;
    public float waitTime = 2.0f;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        // Dezaktywuj wszystkie popupy na pocz¹tku
        foreach (var popup in popUps)
        {
            if (popup != null)
            {
                popup.SetActive(false);
            }
        }

        // zainicjowaæ pierwszy popup, jeœli jest potrzebny od razu po uruchomieniu.
        if (popUps.Length > 0)
        {
            popUps[0].SetActive(true);
        }

    }


    private void Update()
    {
        if (isTransitioning)
        {
            return;
        }

        //For every kill of enemy increment int counter, when counter is 5 the next popup will be displayed
        if (popUpIndex == 0 && enemy == null)
        {
            /*int counter = 0;
            counter++;
            if (counter == 5)
            {*/
            StartCoroutine(DisplayPopupForTwoSec(popUpIndex + 1, 0.5f));
            //}
        }
        else if (popUpIndex == 1 && enemy2 == null)
        {
            StartCoroutine(DisplayPopupForTwoSec(popUpIndex + 1, 0.5f));
        } 
    }

    IEnumerator DisplayPopupForTwoSec(int newIndex, float delay)
    {
        isTransitioning = true;

        // Dezaktywuj bie¿¹cy popup
        if (popUpIndex < popUps.Length && popUps[popUpIndex].activeSelf)
        {
            yield return StartCoroutine(FadeOut(popUps[popUpIndex], 0.5f));
        }

        yield return new WaitForSeconds(delay);

        popUpIndex = newIndex;

        // Aktywuj nowy popup
        if (popUpIndex < popUps.Length)
        {
            popUps[popUpIndex].SetActive(true);
            StartCoroutine(FadeIn(popUps[popUpIndex], 0.5f));
            yield return new WaitForSeconds(2);
            StartCoroutine(FadeOut(popUps[popUpIndex], 0.5f));

        }

        isTransitioning = false;
    }

    IEnumerator ChangePopUpIndexAfterDelay(int newIndex, float delay)
    {
        isTransitioning = true;

        // Dezaktywuj bie¿¹cy popup
        if (popUpIndex < popUps.Length && popUps[popUpIndex].activeSelf)
        {
            yield return StartCoroutine(FadeOut(popUps[popUpIndex], 0.5f));
        }

        yield return new WaitForSeconds(delay);

        popUpIndex = newIndex;

        // Aktywuj nowy popup
        if (popUpIndex < popUps.Length)
        {
            popUps[popUpIndex].SetActive(true);
            StartCoroutine(FadeIn(popUps[popUpIndex], 0.5f));
        }

        isTransitioning = false;
    }


    IEnumerator FadeIn(GameObject popup, float duration)
    {
        CanvasGroup canvasGroup = popup.GetComponent<CanvasGroup>();
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, (Time.time - startTime) / duration);
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    IEnumerator FadeOut(GameObject popup, float duration)
    {
        CanvasGroup canvasGroup = popup.GetComponent<CanvasGroup>();
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, (Time.time - startTime) / duration);
            yield return null;
        }
        canvasGroup.alpha = 0;
        popup.SetActive(false);
    }
}

