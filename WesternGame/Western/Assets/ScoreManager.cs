using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // 静的なScoreManagerのインスタンスを作成
    public int scorePoint;  // スコアの数を計算する変数
    public int highScore;  //ハイスコアを計算する変数

    const string HighScoreKey = "HighScore";

    void Awake()
    {
        if (instance == null)   // インスタンスの中身が無い場合
        {
            instance = this;    // インスタンスの中身を入れる
            DontDestroyOnLoad(this.gameObject); // シーンが読み込まれても削除されないようにする

            highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        }
        else
        {
            Destroy(gameObject);    // ScoreManagerを削除する
        }
    }

    public void AddScore(int add)
    {
        scorePoint += add;
        UpdateHighScoreIfNeeded();
    }

    public void UpdateHighScoreIfNeeded()
    {
        if (scorePoint >= highScore)
        {
            highScore = scorePoint;
            PlayerPrefs.SetInt(HighScoreKey, highScore);
            PlayerPrefs.Save();
        }
    }

    public void ResetScore()
    {
        scorePoint = 0;
    }
}
