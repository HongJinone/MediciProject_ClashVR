using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus2HP_C : MonoBehaviour {
    [Header ("Health")]
    public float startHealth;
    public float currentHealth;
    //public UnityEngine.UI.Image healthBar;
    public bool nexusIsDestroyed;
    public bool enemyNexusIsDestroyed;
    public UnityEngine.UI.Image healthBar;


//    public Renderer modelRenderer;
    float m_Timer;
    public Nexus1HP_C enemyNexus;
    public GameObject endButtonUI;
    public GameObject winUI;
    public GameObject loseUI;
    public GameObject myNexusHP;
    // public float fadeDuration = 1f;

    // public CanvasGroup gameLoseCanvasGroup;
    // public CanvasGroup gameWinCanvasGroup;

    // Start is called before the first frame update
    void Start () {
        currentHealth = startHealth;
        //sr = GetComponent<SpriteRenderer> ();
    }

    private void OnTriggerEnter (Collider other) {
            //1. 오직 해당 영역에 충돌한 주체가 적 유닛일 때만 엔딩 진행
        if (other.gameObject.layer == 8) {            
            GetNexusDamage();
            Destroy(other.gameObject);
        } 
    }

    // Update is called once per frame
    void Update () {
        //만약 체력이 0 이하이면 게임오버 씬을 부른다.
        if (currentHealth <= 0) {
            nexusIsDestroyed=true;
            EndLevel();
        } else if(enemyNexusIsDestroyed)
        {
            EndLevel();
        }
    }


    public void GetNexusDamage () {
        MyDebug.Log ("넥서스 2가 데미지를 입었다");
        currentHealth --;
        //화면 이미지를 material을 빨간색으로 변경
//        modelRenderer.material.SetColor("_Color", Color.red);
        MyDebug.Log("Nexus: RedFlash");
        healthBar.fillAmount = currentHealth/startHealth;

        //Invoke("ResetMaterial", 0.1f);
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
        m_Timer += Time.deltaTime;
        Debug.Log("Game Over");
        if(this.nexusIsDestroyed)
        {
            Debug.Log("플레이어가 패배했습니다");
            //gameLoseCanvasGroup.alpha = m_Timer / fadeDuration;
            loseUI.SetActive(true);    
        } else if(enemyNexus.nexusIsDestroyed)
        {
            Debug.Log("플레이어가 승리했습니다");
            //gameWinCanvasGroup.alpha = m_Timer / fadeDuration;
            winUI.SetActive(true);    
        }
        //항복 애니메이션
        //플레이어가 패배했다는 메세지, 화면
        //다른 플레이어가 승리했다는 메세지, 화면
        Invoke("EndButtonInvoker", 2f);
    }
    void EndButtonInvoker()
    {
        endButtonUI.SetActive(true);
    }
    public void GetEnemyCheck(bool t)
    {
        enemyNexusIsDestroyed = t;
    }
}