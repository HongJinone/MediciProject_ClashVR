﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

// 유닛이 목적지를 찾아서 이동 후 공격하고 물리치면 다음 목적지을 찾고 이동 후 공격을 반복적으로 수행하고싶다.
// 목적지
// 나의체력
public enum unit1_10State {
    MoveAndSearch,
    Attack,
}
public class Unit1_10 : MonoBehaviour {

    public unit1_10State state;

    NavMeshAgent navMeshagent;
    public Transform target;
    public float viewDistance;
    public GameObject goal; // == Enemy NEXUS 
    public GameObject arrowPrefab;
    public Transform arrowPoint; //

    Unit1Detection_10 unitDetection;
    HP_8 hp;

    SphereCollider viewSightCollider;
    public float attackRange;
    float currentTime;
    float attackTime = 1;

    #region UnityMonoBehaviour
    void Awake () {
        navMeshagent = GetComponent<NavMeshAgent> ();
        viewSightCollider = GetComponentInChildren<SphereCollider> ();
        hp = GetComponentInChildren<HP_8> ();
        unitDetection = GetComponentInChildren<Unit1Detection_10>();
        goal= GameObject.FindGameObjectWithTag("Player2Nexus");
    }
    void Start () {
        Initialize ();
    }
    void FixedUpdate () {
        //본인이 사망하면 아래를 업데이트하지 않는다.
        if (hp.isDead) {
            return;
        }

        Debug.Log ("Unit State :: " + state);
        //타겟이 있으면
        if (target) {
            //타겟이 시야 내 감지되었으면
            if (state == unit1_10State.MoveAndSearch) {
                Move (target.position);
                //SearchTarget ();
            } else if (state == unit1_10State.Attack && unitDetection.targetIsDetected) {
                Attack (target.position);
            }
        } else {
            Initialize ();
        }
    }
    // private void Update () {
    // }

    #endregion

    void Move (Vector3 _targetPostion) {
        navMeshagent.SetDestination (_targetPostion);
    }
    // void SearchTarget () // ==> SetTarget 
    // {
    //     //공격해서 적이 죽었을 때
    //     //다음 적을 찾는다
    //     //만약 적이 사정거리 밖으로 나가면
    //     //ChangeTarget
    // }

    void Attack (Vector3 _targetPostion) {
        currentTime += Time.deltaTime;
        //타겟이 없다면 타겟=넥서스로 초기화
        if (!target) {

            return;
        }
        //만약 타겟이 있고, 타겟이 죽지 않았다면, 사거리 내에 있으면
        float dis = Vector3.Distance (transform.position, target.position);
        if (dis <= attackRange) {
            //이동을 멈춘다
            navMeshagent.SetDestination (this.transform.position);
            // 시간차 공격을 시도한다.
            if (currentTime > attackTime) {
                Shoot ();
                currentTime = 0;
            }
        }
        //만약 사거리 바깥에 있을 때에만
        else if (dis > attackRange) {
            //타겟에게 이동한다.
            Debug.Log ("사거리 바깥입니다. 이동");
            navMeshagent.SetDestination (target.position);
            Move (target.position);
        }
    }

    #region Collision

    void Shoot () {
        //1 총알을 생성
        GameObject go = Instantiate (arrowPrefab, arrowPoint.position, Quaternion.identity);
        //애로우인데 어떤 애로우냐? 프리팹에 들어있는 애로우cs이다
        Arrow_10 arrow = go.GetComponent<Arrow_10> ();
        //2 화살에게 타겟 정보를 보낸다
        arrow.TargetSetter (target);
    }

    // private void OnTriggerStay (Collider other) {
    //     Debug.Log ("Collision" + other.name);

    //     if (this.gameObject.layer == 8) // player1Unit
    //     {
    //         if (other.gameObject.layer == 9) {
    //             ChangeTarget (other.transform);
    //             targetIsDetected = true;
    //             state = eState.Attack;
    //         }
    //     } else if (this.gameObject.layer == 9) //player2Unit
    //     {
    //         if (other.gameObject.layer == 8) {
    //             ChangeTarget (other.transform);
    //             targetIsDetected = true;
    //             state = eState.Attack;
    //         }
    //     } else{
    //         targetIsDetected = false;
    //     }

    // }
    #endregion

    void Initialize () {
        state = unit1_10State.MoveAndSearch;
        target = goal.transform;
        viewDistance = 5.0f;
        viewSightCollider.radius = viewDistance;
    }
    #region "GET SET"
    public Transform GetTarget () { return target; }
    #endregion

}