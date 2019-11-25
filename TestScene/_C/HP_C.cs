using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_C : MonoBehaviour {
    [Header ("Health")]
    public float startHealth;
    public float currentHealth;
    public UnityEngine.UI.Image healthBar;
    public bool isDead;
    public GameObject myParent;

    public Renderer modelRenderer;

    // Start is called before the first frame update
    void Start () {
        currentHealth = startHealth;
        //sr = GetComponent<SpriteRenderer> ();
    }

    // Update is called once per frame
    void Update () {
        //만약 체력이 0 이하이면 죽는다.
        if (currentHealth <= 0) {
            Die ();
        }
    }

    public void GetDamage (float v) {
        MyDebug.Log ("데미지를 입었다");
        //모델의 material을 빨간색으로 변경
        modelRenderer.material.SetColor("_Color", Color.red);
        MyDebug.Log("RedFlash");
        Invoke("ResetMaterial", 0.1f);
        currentHealth -= v;
        if(healthBar!=null)
            healthBar.fillAmount = currentHealth/startHealth;
    }
    void Die () {
        isDead = true; //유닛, 터렛 쪽에서 이 값을 보고 움직임을 정지할 것임.
        //소멸한다. 애니메이션.
        Destroy (myParent, .5f);
    }
    void ResetMaterial()
    {
        //모델의 material을 원래 색으로 변경
        modelRenderer.material.SetColor("_Color", Color.white);
    }
    //유닛 체력 버프
    public void SetHealth(float _value)
    {
        currentHealth += _value;
    }
}