using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDetection : MonoBehaviour
{
    public bool targetIsDetected;
    Turret_7 turretManager;
    private void Start() {
        turretManager= GetComponentInParent<Turret_7>();        
    }

    private void OnTriggerStay (Collider other) {
        Debug.Log(other.gameObject+", "+other.gameObject.layer);
        if (turretManager.gameObject.layer == 8) // player1Unit
        {
            if (other.gameObject.layer == 9) {
                ChangeTarget (other.transform);
                targetIsDetected = true;
                turretManager.state = turret7State.Attack;
            }
        } else if (turretManager.gameObject.layer == 9) //player2Unit
        {
            if (other.gameObject.layer == 8) {
                ChangeTarget (other.transform);
                targetIsDetected = true;
                turretManager.state = turret7State.Attack;
            }
        } else {
            targetIsDetected = false;
        }
    }

    void ChangeTarget (Transform _target) {
        Debug.Log("유닛디텍션:" + _target.transform);
        //타겟의 변화를 갱신한다.
        turretManager.target = _target;
        Debug.Log(turretManager.target.transform);

    }
}
