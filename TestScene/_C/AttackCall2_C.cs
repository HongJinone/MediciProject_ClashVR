using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Unit2_A 에 있는 Shoot 함수를 호출한다.
public class AttackCall2_C : MonoBehaviour
{
    // Unit2_A 에 있는 Shoot 함수를 호출한다.
    public void CallShoot()
    {
        //Unit2의 
        transform.parent.GetComponent<Unit2_C>().Shoot();
    }
}
