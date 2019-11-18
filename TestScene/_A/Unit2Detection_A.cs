﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이 스크립트는 Unit-DetectingCensor에 달려 있다.
public class Unit2Detection_A : MonoBehaviour
{
   
    public bool targetIsDetected;
    Unit2_A unitManager;

    private void Start() {
        unitManager= GetComponentInParent<Unit2_A>();        
    }
    
   private void OnTriggerStay (Collider other) {
        if (unitManager.gameObject.layer == 8) // player1Unit의 레이어(this.gameobject(센서)로 지정하면 안 된다))
        {
            if (other.gameObject.layer == 9) {
                ChangeTarget (other.transform); //타겟을 재지정
                targetIsDetected = true;
                unitManager.state = unit2_AState.Attack;
            }
        } else if (unitManager.gameObject.layer == 9) // player2Unit
        {
            if (other.gameObject.layer == 8) {
                ChangeTarget (other.transform);
                targetIsDetected = true;
                unitManager.state = unit2_AState.Attack;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("UnitDetection: targetisdetected=false");
        targetIsDetected = false;
    }
    //별개로 타겟이 죽었을 때는?

    void ChangeTarget (Transform _target) {
        Debug.Log("유닛디텍션:" + _target.transform);
        //타겟의 변화를 갱신한다.
        unitManager.target = _target;
        Debug.Log(unitManager.target.transform);

    }
}