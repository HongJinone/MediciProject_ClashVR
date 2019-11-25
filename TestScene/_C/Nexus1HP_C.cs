using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus1HP_C : MonoBehaviour {
    [Header ("Health")]
    public float startHealth;
    public float currentHealth;
    //public UnityEngine.UI.Image healthBar;
    public bool nexusIsDestroyed;
//    public Renderer modelRenderer;
    public Nexus2HP_C enemyNexus;
    // public CanvasGroup gameLoseCanvasGroup;
    // public CanvasGroup gameWinCanvasGroup;
    public float fadeDuration = 1f;
    // public GameObject endButtonUI;
    float m_Timer;
    public UnityEngine.UI.Image healthBar;

    // Start is called before the first frame update
    void Start () {
        currentHealth = startHealth;
        //sr = GetComponent<SpriteRenderer> ();
    }

    // Update is called once per frame
    void Update () {
        if (currentHealth <= 0) {
        nexusIsDestroyed=true;
        EndLevel();
        // } else if(enemyNexus.nexusIsDestroyed)
        // {
        //     EndLevel();
        }
    }

    public void GetNexusDamage () {
        MyDebug.Log ("넥서스 1가 데미지를 입었다");
        currentHealth --;
        //화면 이미지를 material을 빨간색으로 변경
//        modelRenderer.material.SetColor("_Color", Color.red);
        MyDebug.Log("Nexus: RedFlash");
//        Invoke("ResetMaterial", 0.1f);
        healthBar.fillAmount = currentHealth/startHealth;
    }
    // void Die () {
    //     isDead = true; //유닛, 터렛 쪽에서 이 값을 보고 움직임을 정지할 것임.
    //     //소멸한다. 애니메이션.
    //     Destroy (myParent);
    // }
    // void ResetMaterial()
    // {
    //     //모델의 material을 원래 색으로 변경
    //     modelRenderer.material.SetColor("_Color", Color.white);
    // }
    void EndLevel()
    {
        enemyNexus.GetEnemyCheck(true);
        // //Debug.Log("Game Over");
        // m_Timer += Time.deltaTime;
        // if(this.nexusIsDestroyed)
        // {
        //     Debug.Log("플레이어가 패배했습니다");
        //     gameLoseCanvasGroup.alpha = m_Timer / fadeDuration;
        // } else if(enemyNexus.nexusIsDestroyed)
        // {
        //     Debug.Log("플레이어가 승리했습니다");
        //     gameWinCanvasGroup.alpha = m_Timer / fadeDuration;
        // }
        // endButtonUI.SetActive(true);
        // //항복 애니메이션
        // //플레이어가 패배했다는 메세지, 화면
        // //다른 플레이어가 승리했다는 메세지, 화면
        // //나가기 버튼
    }
    void RetryLevel()
    {
        Debug.Log("해당 레벨을 재도전합니다.");
        //1. 게임에서 졌을 경우
        //2. 게임을 다시 시작하고 싶은 경우(메뉴 호출)
    }
    void ChangeLevel()
    {
        Debug.Log("다음 레벨로 이동합니다.");
        //1. 게임에서 이기거나 졌을 경우
        //2. 게임을 다시 시작하고 싶은 경우
    }
    void QuitLevel()
    {
        Debug.Log("메인 메뉴로 이동합니다.");
        //1. 게임에서 졌을 경우
        //2. 게임 도중 나가고 싶을 경우
    }
}