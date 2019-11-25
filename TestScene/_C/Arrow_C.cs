using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//화살은 적과 터렛, 성채를 대상으로 한다.성채는 공격 기능이 없는 터렛이다.
public class Arrow_C : MonoBehaviour {
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
        MyDebug.Log ("화살: 타겟은 "+target+"입니다.");
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

                HP_C targetHP = other.gameObject.GetComponentInChildren<HP_C> ();
                if (targetHP != null) {
                    targetHP.GetDamage (damage);
                }
                if(impactEffect)
                {
                    GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
                    Destroy(effectIns, 2f);
                }
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

    }
}