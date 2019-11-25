using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이 스크립트는 Unit-DetectingCensor에 달려 있다.
public class Unit2Detection_C : MonoBehaviour
{
   
    public bool targetIsDetected;
    Unit2_C unitManager;

    private void Awake() {
        unitManager= GetComponentInParent<Unit2_C>();        
    }
    
   private void OnTriggerStay (Collider other) {
        if (unitManager.gameObject.layer == 9) // player2Unit
        {
            if (other.gameObject.layer == 8) {
                ChangeTarget (other.transform);
                targetIsDetected = true;
                unitManager.state = unit2_CState.Attack;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        MyDebug.Log("UnitDetection: targetisdetected=false");
        targetIsDetected = false;
    }
    //별개로 타겟이 죽었을 때는?

    void ChangeTarget (Transform _target) {

        //타겟의 변화를 갱신한다.
        unitManager.target = _target;
        MyDebug.Log("UnitDetection2: "+unitManager.target.transform);

    }
}
