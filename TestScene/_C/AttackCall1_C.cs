using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Unit1_A 에 있는 Shoot 함수를 호출한다.
public class AttackCall1_C : MonoBehaviour
{
    // Unit1_A 에 있는 Shoot 함수를 호출한다.
    public void CallShoot()
    {
        //Unit1의 
        transform.parent.GetComponent<Unit1_C>().Shoot();
    }
}
