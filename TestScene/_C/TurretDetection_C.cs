using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDetection_C : MonoBehaviour {
    public bool targetIsDetected;
    Turret_C turretManager;
    private void Start () {
        turretManager = GetComponentInParent<Turret_C> ();
    }

    private void OnTriggerStay (Collider other) {
        //Debug.Log ("TurretDetection: " + other.gameObject + ", " + other.gameObject.layer);
        if (turretManager.gameObject.layer == 8) // player1Unit
        {
            if (other.gameObject.layer == 9) {
                ChangeTarget (other.transform);
                targetIsDetected = true;
                turretManager.state = turretCState.Attack;
            }
        } else if (turretManager.gameObject.layer == 9) //player2Unit
        {
            if (other.gameObject.layer == 8) {
                ChangeTarget (other.transform);
                targetIsDetected = true;
                turretManager.state = turretCState.Attack;
            }
        } else {
            targetIsDetected = false;
        }
    }

    void ChangeTarget (Transform _target) {
        //타겟의 변화를 갱신한다.
        turretManager.target = _target;
        MyDebug.Log ("TurretDetection: "+turretManager.target.transform);

    }
}