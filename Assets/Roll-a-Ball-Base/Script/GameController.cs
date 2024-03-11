using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public class GameController : MonoBehaviour {
    [SerializeField] Canvas canvas;
    [SerializeField] Image scoreImage;
    [SerializeField] GameObject winnerLabelObject;
    [SerializeField] GameObject winnerLabel;
    [SerializeField] GameObject gameStartCanvas;
    [SerializeField] TextMeshProUGUI time;
    public CinemachineVirtualCamera virtualCamera;
    [System.NonSerialized] public PlayerController player;
    public bool isStarted = false;
    public static GameController gameController;
    public static int count;

    [Header("ほめ言葉")]
    public string[] winnerComment = { "★★★", "★★", "★" };
    public float goodTime = 5.0f;
    public float normalTime = 10.0f;

    private int initialCount;
    private int currentCount;

    private ScoreController scoreController;
    private bool isStart = false;
    private float pastTime = 0.0f;

    private void Awake()
    {
        gameController = this;
    }

    public void Start()
    {
        initialCount = GameObject.FindGameObjectsWithTag("Item").Length;
        currentCount = initialCount;
        scoreController = scoreImage.GetComponent<ScoreController>();
        canvas.gameObject.SetActive(true);
        scoreController.SetGageUnit(initialCount);
        scoreController.ChangeValue(0);
        count = GameObject.FindGameObjectsWithTag("Item").Length;
        pastTime = 0.0f;
    }

    public void Update()
    {
        if (!isStart) {
            if (isStarted) {
                isStart = true;
                gameStartCanvas.SetActive(false);
            }
        }
        else {
            pastTime += Time.deltaTime;
            count = GameObject.FindGameObjectsWithTag("Item").Length;
            if (count != currentCount) {
                scoreController.ChangeValue((initialCount - count) / (float)initialCount);
                currentCount = count;
                if (count == 0) {
                    // オブジェクトをアクティブにする
                    winnerLabelObject.SetActive(true);
                    pastTime *= 100;
                    pastTime = Mathf.Floor(pastTime);
                    pastTime /= 100;
                    time.text = pastTime.ToString("F2");
                    if (pastTime < goodTime) {
                        winnerLabel.GetComponent<Text>().text += "\n" + winnerComment[0];
                    }
                    else if (pastTime < normalTime) {
                        winnerLabel.GetComponent<Text>().text += "\n" + winnerComment[1];
                    }
                    else {
                        winnerLabel.GetComponent<Text>().text += "\n" + winnerComment[2];
                    }

                    player.SetClear();

                }

            }
        }

    }
    int sceneIndex;
    float waitTimeSave;
    public void ResetScene(float waitTime)
    {
        waitTimeSave = waitTime;
        Debug.Log("RESET");
        StartCoroutine(ResetSceneCoroutine());
    }
    private IEnumerator ResetSceneCoroutine()
    {
        yield return new WaitForSeconds(waitTimeSave);
        //現在のシーン番号を取得
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        //現在のシーンを再読込する
        SceneManager.LoadScene(sceneIndex);

    }

}
