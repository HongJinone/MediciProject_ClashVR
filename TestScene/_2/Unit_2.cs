using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_2 : MonoBehaviour {
    [Header("Health")]
    public float startHealth = 100;
    public float currentHealth;
    public UnityEngine.UI.Image healthBar;
    
    [Header("AI Destination")]
    UnityEngine.AI.NavMeshAgent agent;
    public GameObject turret;       //AI의 이동목적지

    // Start is called before the first frame update
    void Start () {
        currentHealth = startHealth;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
        agent.destination = turret.transform.position;  //임시로 목적지를 한 번만 설정하도록 함(수정 대상)
    }

    public void TakeDamage (float amount) {             //데미지를 입는다
        currentHealth -= amount;           
        healthBar.fillAmount = currentHealth / startHealth;    //헬스바 이미지를 깎는다
        if(healthBar.fillAmount<=0)                     //헬스바 fill값이 0보다 작으면 이 GO를 파괴.
            Destroy(this.gameObject);
    }
}