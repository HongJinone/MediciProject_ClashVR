using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusDetection_A : MonoBehaviour
{
    public Nexus1HP_A nexus1;
    public Nexus2HP_A nexus2;

    private void OnTriggerEnter (Collider other) {
        //1. 오직 해당 영역에 충돌한 주체가 적 유닛일 때만 엔딩 진행
        if (other.gameObject.layer == 9) 
        {            
            HP_A otherHp= other.GetComponent<HP_A>();
            nexus1.GetNexusDamage();
            otherHp.currentHealth=0;
        } else if(other.gameObject.layer == 8)
        {
            HP_A otherHp= other.GetComponent<HP_A>();
            nexus2.GetNexusDamage();
            otherHp.currentHealth=0;
        //사라진 자리에 파티클 생성
        }
    }
}
