using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Image goalImage;
    public Text goalText;
    public Sprite[] goalSprite;
    public CanvasGroup gameOverUI;
    public CanvasGroup winUI;

    private AudioManager audioManager;
    private bool endGame = false;

    void Start()
    {
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();

        NewGoal();
    }

    void Update()
    {
        if (!endGame)
        {
            goalText.text = DataManager.goalCount + " / " + DataManager.goal;

            if (DataManager.goalCount == DataManager.goal)
                NewGoal();
        }
    }

    public void NewGoal()
    {
        if (DataManager.level == 3)
        {
            endGame = true;
            Win();
        } else
        {
            int index = Random.Range(0, 3);

            DataManager.goal += 3;
            DataManager.goalCount = 0;
            DataManager.goalEnemyId = index + 1;
            DataManager.goalEnemy = index;
            goalImage.sprite = goalSprite[index];

            DataManager.level++;

            for (int i = 0; i < DataManager.unlockedPowers.Length; i++)
            {
                DataManager.unlockedPowers[i] = i + 1 <= DataManager.level;
            }
        }
    }

    public void GameOver()
    {
        gameOverUI.gameObject.SetActive(true);
        StartCoroutine(FadeCanvas(gameOverUI, gameOverUI.alpha, 1));
        audioManager.PlayEndGame();
    }

    public void Win()
    {
        winUI.gameObject.SetActive(true);
        StartCoroutine(FadeCanvas(winUI, winUI.alpha, 1));
        audioManager.PlayEndGame();
    }

    public IEnumerator FadeCanvas(CanvasGroup canvas, float start, float end, float lerpTime = 0.5f)
    {
        float startLerp = Time.time;
        float timePassed = Time.time - startLerp;
        float percentage = timePassed / lerpTime;

        while (true)
        {
            timePassed = Time.time - startLerp;
            percentage = timePassed / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentage);

            canvas.alpha = currentValue;

            if (percentage >= 1) break;

            yield return new WaitForEndOfFrame();
        }
    }
}
