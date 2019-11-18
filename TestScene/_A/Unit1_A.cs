using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

//유닛이 목적지를 찾아서 이동한다.
//도중 적을 만나면 물리친다. 
//적이 없으면 원래 목적지을 찾아 이동한다.

public enum unit1_AState {         //State는 움직임+탐색, 공격
    MoveAndSearch,
    Attack,
}
public class Unit1_A : MonoBehaviour {
    public unit1_AState state;

    [Header("Public References")]
    public Transform target;
    public GameObject goal; // == Enemy NEXUS 
    public GameObject arrowPrefab;
    public Transform arrowPoint; //
    
    [Header("References")]
    NavMeshAgent navMeshagent;
    Unit1Detection_A unitDetection;
    HP_A hp;
    SphereCollider viewSightCollider;

    [Header("Attributes")]
    public float attackRange;
    public float viewDistance;
    float currentTime;
    float attackTime = 1;

    #region UnityMonoBehaviour
    void Awake () {
        navMeshagent = GetComponent<NavMeshAgent> ();
        viewSightCollider = GetComponentInChildren<SphereCollider> ();
        hp = GetComponentInChildren<HP_A> ();
        unitDetection = GetComponentInChildren<Unit1Detection_A>();
        goal= GameObject.FindGameObjectWithTag("Player2Nexus");
    }
    void Start () {
        Initialize ();
        navMeshagent.Warp(transform.position);
    }
    void FixedUpdate () {
        if (hp.isDead) {
            return;
        }
        //유닛의 상태
        Debug.Log ("Unit State :: " + state);
        //타겟이 있고
        if (target) {
            //타겟이 시야 안에 있다면
            if (state == unit1_AState.MoveAndSearch) {
                //타겟의 위치로 이동한다.
                Move (target.position);
            //공격상태이며 타겟이 시야 안에 있다면
            } else if (state == unit1_AState.Attack && unitDetection.targetIsDetected) {
                //타겟을 공격한다.
                Attack (target.position);
            }
        } else {
            //그밖의 상황에서는 처음으로 돌아간다.
            Initialize ();
        }
    }
    #endregion

    void Move (Vector3 _targetPostion) {
        navMeshagent.SetDestination (_targetPostion);
    }

    void Attack (Vector3 _targetPostion) {
        currentTime += Time.deltaTime;
        if (!target) {
            return;
        }
        
        float dis = Vector3.Distance (transform.position, target.position);
        //만약 타겟과의 거리가 공격범위 이내이면
        if (dis <= attackRange) {
            //이동을 멈춘다
            navMeshagent.SetDestination (this.transform.position);
            //시간차 공격을 한다
            if (currentTime > attackTime) {
                transform.LookAt(target);
                Shoot ();
                currentTime = 0;
            }
        }
        //만약 타겟이 사거리 바깥에 있을 때에는
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
        Arrow_A arrow = go.GetComponent<Arrow_A> ();
        //2 화살에게 타겟 정보를 보낸다
        arrow.TargetSetter (target);
    }
    #endregion
    //기본상태, 타겟, 시야 설정
    void Initialize () {
        state = unit1_AState.MoveAndSearch;
        target = goal.transform;
        viewSightCollider.radius = viewDistance;
    }
    #region "GET SET"
    public Transform GetTarget () { return target; }
    #endregion

}