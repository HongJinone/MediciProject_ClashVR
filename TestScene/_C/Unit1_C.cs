using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using Photon.Pun;

//유닛이 목적지를 찾아서 이동한다.
//도중 적을 만나면 물리친다. 
//적이 없으면 원래 목적지을 찾아 이동한다.

public enum unit1_CState {         //State는 움직임+탐색, 공격
    MoveAndSearch,
    Attack,
}
public class Unit1_C : MonoBehaviour {
    public unit1_CState state;

    [Header("Public References")]
    public Transform target;
    public GameObject goal; // == Enemy NEXUS 
    public GameObject arrowPrefab;
    public Transform arrowPoint; //
    
    [Header("References")]
    NavMeshAgent navMeshagent;
    Unit1Detection_C unitDetection;
    HP_C hp;
    AudioSource myAudio;
    SphereCollider viewSightCollider;

    [Header("Attributes")]
    public float attackRange;
    public float viewDistance;
    float currentTime;
    float attackTime = 1;
    Animator anim;

    #region UnityMonoBehaviour
    void Awake () {
        navMeshagent = GetComponent<NavMeshAgent> ();
        viewSightCollider = GetComponentInChildren<SphereCollider> ();
        hp = GetComponentInChildren<HP_C> ();
        unitDetection = GetComponentInChildren<Unit1Detection_C>();
        goal= GameObject.FindGameObjectWithTag("Player2Nexus");
        anim = GetComponentInChildren<Animator> ();
        myAudio = GetComponent<AudioSource>();
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
        //타겟이 있고
        if (target) {
            //타겟이 시야 안에 있다면
            if (state == unit1_CState.MoveAndSearch) {
                //타겟의 위치로 이동한다.
                Move (target.position);
            //공격상태이며 타겟이 시야 안에 있다면
            } else if (state == unit1_CState.Attack && unitDetection.targetIsDetected) {
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
        anim.SetTrigger("Move");
    }

    void Attack (Vector3 _targetPostion) {
        currentTime += Time.deltaTime;
        if (!target) {
            return;
        }
        float dis = Vector3.Distance (transform.position, target.position);
        if (dis <= attackRange) {
            navMeshagent.SetDestination (this.transform.position);
            if (currentTime > attackTime) {        
                anim.SetTrigger("Attack");
                transform.LookAt(target);
                //Shoot();
                currentTime = 0;
            }
        }
        else if (dis > attackRange) {
            navMeshagent.SetDestination (target.position);
            Move (target.position);
        }
    }
    #region Collision
    
    public void Shoot () {
        MyDebug.Log("유닛1: 공격");
        //1 총알을 생성
        GameObject go = Instantiate (arrowPrefab, arrowPoint.position, Quaternion.identity);
        Arrow_C arrow = go.GetComponent<Arrow_C> ();

        myAudio.Play();

        //2 화살에게 타겟 정보를 보낸다
        arrow.TargetSetter (target);
        //GetFeverPoint(); 
    }
    #endregion
    //기본상태, 타겟, 시야 설정
    void Initialize () {
        state = unit1_CState.MoveAndSearch;
        target = goal.transform;
        viewSightCollider.radius = viewDistance;
    }
    #region "GET SET"
    public Transform GetTarget () { return target; }
    #endregion

}