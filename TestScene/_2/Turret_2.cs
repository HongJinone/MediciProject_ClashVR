using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_2 : MonoBehaviour {
    
    [Header("Health")]
    public float startHealth = 100;
    public float currentHealth;
    public UnityEngine.UI.Image healthBar;

    //터닝은 일단 스킵:https://youtu.be/QKhn2kl9_8I 18:09~
    void Start () {
        currentHealth = startHealth;
    }

    public void TakeDamage (float amount) {             //데미지를 입는다
        currentHealth -= amount;           
        healthBar.fillAmount = currentHealth / startHealth;    //헬스바 이미지를 깎는다
        if(healthBar.fillAmount<=0)                     //헬스바 fill값이 0보다 작으면 이 GO를 파괴.
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision other) {
        // 나랑 부딪힌 상대방이 총알이라면
        if(other.gameObject.tag=="Arrow"){
            Debug.Log("터렛이 맞았다");
        // TakeDamage 함수를 호출하고싶다.
            TakeDamage(20f);
            // 총알은 없애고싶다.
            Destroy(other.gameObject);
        } 
       
    }
}