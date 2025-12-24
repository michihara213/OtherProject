using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    int preScorePoint = 0;  // 前のスコアを取得する変数

    void Start()
    {
        ScoreManager.instance.scorePoint = 0;    // スコアをリセットさせたいので0を代入
    }

    void Update()
    {
        if (ScoreManager.instance.scorePoint - preScorePoint > 0)    // スコアが変動した場合
        {
            preScorePoint = ScoreManager.instance.scorePoint;    // スコア数を代入する
            GetComponent<TextMeshProUGUI>().text = ScoreManager.instance.scorePoint.ToString("d6");  // スコア数を表示させる処理
        }
    }
}
