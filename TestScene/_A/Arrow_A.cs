using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//화살은 적과 터렛, 성채를 대상으로 한다.성채는 공격 기능이 없는 터렛이다.
public class Arrow_A : MonoBehaviour {
    [Header ("Attributes")]
    public float speed;
    public int damage;

    [Header ("Destination")]
    Transform target;
    Rigidbody rb;
    public float force = 10;
    public GameObject impactEffect;
    public void TargetSetter (Transform _target) {
        target = _target;
        Debug.Log ("화살: 타겟은 "+target+"입니다.");
        rb = GetComponent<Rigidbody> ();
        AddforceToTarget ();
    }

    void AddforceToTarget () {
        Vector3 dir = target.position - transform.position;
        rb.AddForce (dir.normalized * force, ForceMode.Impulse);
    }

    private void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag != "Censor")
        { //부딪힌 대상이 타겟과 같으면
            if (other.gameObject == target.gameObject) {

                HP_A targetHP = other.gameObject.GetComponentInChildren<HP_A> ();
                if (targetHP != null) {
                    targetHP.GetDamage (damage);
                }
                GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(effectIns, 2f);
                Destroy (this.gameObject);
                //this.gameObject.SetActive(false);
            } else {
                Destroy (this.gameObject, 2f);
            }
        }

    }
    void Update () {

        // rb의 velocity(속도)의 방향으로 화살의 forward를 일치시키고 싶다
        if(target!=null)    
        {
            transform.forward=rb.velocity.normalized;
        } else 
        {   
            Destroy (this.gameObject);
        }
        // if(target)
        // //Vector3 dir = target.position - transform.position; //총알의 방향 설정
        // float disThisFrame = speed * Time.deltaTime;        //총알의 힘

        // transform.Translate (dir.normalized * disThisFrame, Space.World);
        //                                     //총알이 파괴되거나 어디 맞지 않았으면 
        //                                     //주어진 방향과 힘으로 이동한다

    }
    void HitTarget () {
        //만약 화살이 닿은 대상이 타겟이라면
        //타겟에게 데미지를 주고 디스트로이
        //타겟에게 닿지 않았다면
        //2초 후에 디스트로이
    }

}