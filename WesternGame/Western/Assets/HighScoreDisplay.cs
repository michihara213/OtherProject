using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
   int preHighScore = -1;  // 前のハイスコアを記録しておく変数

    void Start()
    {
        // 起動時に保存済みハイスコアを読み込む
        ScoreManager.instance.highScore = PlayerPrefs.GetInt("HighScore", 0);

        // 初期表示
        GetComponent<TextMeshProUGUI>().text = ScoreManager.instance.highScore.ToString("d6");
        preHighScore = ScoreManager.instance.highScore;
    }

    void Update()
    {
        // ハイスコアが更新されたときだけ表示を変える
        if (ScoreManager.instance.highScore != preHighScore)
        {
            preHighScore = ScoreManager.instance.highScore;
            GetComponent<TextMeshProUGUI>().text = preHighScore.ToString("d6");
        }
    }
}
