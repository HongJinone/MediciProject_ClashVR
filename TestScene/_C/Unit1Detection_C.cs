using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//센서 오브젝트 collider에 감지되는 오브젝트를 체크한다. 
//감지된 오브젝트가 타겟인지 판별한다.
//만약 타겟이면 타겟의 위치값을 부모 오브젝트에게 전달한다. 
public class Unit1Detection_C : MonoBehaviour
{
    public bool targetIsDetected;
    Unit1_C unitManager;

    private void Awake() {
        unitManager= GetComponentInParent<Unit1_C>();        
    }
 
   private void OnTriggerStay (Collider other) {
        //만약 내 부모의 레이어가 1P, 감지된 오브젝트의 레이어가 2P이면
        if (unitManager.gameObject.layer == 8) // player1Unit
        {
            if (other.gameObject.layer == 9) {
                //타겟을 감지된 오브젝트로 설정한다.
                ChangeTarget (other.transform);
                targetIsDetected = true;
                //공격상태로 전환한다.
                unitManager.state = unit1_CState.Attack;
            }
        } 
    }

    //타겟이 영역에서 빠져나갔을 때 타겟이 감지되지 않았다는 정보를 저장하고 싶다.
    private void OnTriggerExit(Collider other) {
    MyDebug.Log("UnitDetection1: targetisdetected=false");
        targetIsDetected = false;
    }
    //별개로 타겟이 죽었을 때는?

    //타겟의 위치값을 받고 기존 타겟을 변경한다.
    void ChangeTarget (Transform _target) {
        //타겟의 변화를 부모 오브젝트에게 알린다.
        unitManager.target = _target;
        MyDebug.Log(unitManager.target.transform);

    }
}
