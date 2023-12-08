using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Timer : MonoBehaviour
{
    private int sec = 30;
    private int min = 1;
    private Coroutine timer;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button skipButton;

    private void Update()
    {
        if (sec == 0 && min == 0)
        {
            StopCoroutine(timer);
            timerText.DOFade(0, 0.7f);
            skipButton.image.DOFade(1f, 1);
            skipButton.interactable = true;
            sec = 30;
            min = 1;
        }
    }
    public void StartTimer()
    {
        skipButton.image.DOFade(0.2f, 1);
        skipButton.interactable = false;
        timerText.DOFade(1, 0.7f);
        timer = StartCoroutine(ITimer());
    }
    IEnumerator ITimer()
    {
        while (true)
        {
            if (sec == 0)
            {
                min--;
                sec = 60;
            }
            sec--;
            timerText.text = min.ToString("D2") + ":" + sec.ToString("D2");
            yield return new WaitForSeconds(1);
        }
    }
}