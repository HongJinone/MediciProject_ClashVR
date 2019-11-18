using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager_A : MonoBehaviour
{
    //public GameObject parent;
    public int fp;
    public Text countFPText;
    private void Start() {
        fp=500;
    }
    
    //업데이트 말고 값이 들어오면 출력하게 할 수 있나? 왜 Start로는 되지 않았지?
    private void Update() {
        SetCountText();
    }

    void SetCountText()
    {
        countFPText.text=fp.ToString();
    }
}
