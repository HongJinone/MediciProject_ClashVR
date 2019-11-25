using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using Photon.Pun;

// 유닛이 목적지를 찾아서 이동 후 공격하고 물리치면 다음 목적지을 찾고 이동 후 공격을 반복적으로 수행하고싶다.
// 목적지
// 나의체력
public enum unit2_CState {
    MoveAndSearch,
    Attack,
}
public class Unit2_C : MonoBehaviour {

    public unit2_CState state;

    NavMeshAgent navMeshagent;
    public Transform target;
    public float viewDistance;
    public GameObject goal; // == Enemy NEXUS 
    public GameObject arrowPrefab;
    public Transform arrowPoint; //
    public ScoreManager_C scoreManager;
    Unit2Detection_C unitDetection;
    HP_C hp;
    AudioSource myAudio; 

    SphereCollider viewSightCollider;
    public float attackRange;
    float currentTime;
    float attackTime = 1;
    Animator anim;

    #region UnityMonoBehaviour
    void Awake () {
        navMeshagent = GetComponent<NavMeshAgent> ();
        viewSightCollider = GetComponentInChildren<SphereCollider> ();
        hp = GetComponentInChildren<HP_C> ();
        unitDetection = GetComponentInChildren<Unit2Detection_C>();
        goal= GameObject.FindGameObjectWithTag("Player1Nexus");
        anim = GetComponentInChildren<Animator> ();
        scoreManager= GameObject.Find("ScoreManager").GetComponent<ScoreManager_C>();
        myAudio = GetComponent<AudioSource>();
    }
    void Start () {
        Initialize ();
        navMeshagent.Warp(transform.position);
    }
    void FixedUpdate () {
        //본인이 사망하면 아래를 업데이트하지 않는다.
        if (hp.isDead) {
            return;
        }
        //타겟이 있으면
        if (target) {
            //타겟이 시야 내 감지되었으면
            if (state == unit2_CState.MoveAndSearch) {
                Move (target.position);
            } else if (state == unit2_CState.Attack && unitDetection.targetIsDetected) {
                Attack (target.position);
            }
        } else {
            Initialize ();
        }
    }

    #endregion

    void Move (Vector3 _targetPostion) {
        navMeshagent.SetDestination (_targetPostion);
        //이동 애니메이션
        anim.SetTrigger("Move");
    }

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
                //애니메이션, 발사
                anim.SetTrigger("Attack");
                transform.LookAt(target);
                //Shoot ();
                currentTime = 0;
            }
        }
        //만약 사거리 바깥에 있을 때에만
        else if (dis > attackRange) {
            //타겟에게 이동한다.
            navMeshagent.SetDestination (target.position);
            Move (target.position);
        }
    }

    #region Collision

    public void Shoot () {
        MyDebug.Log("유닛2: 공격");
        //1 총알을 생성
        GameObject go = Instantiate (arrowPrefab, arrowPoint.position, Quaternion.identity);
        //애로우인데 어떤 애로우냐? 프리팹에 들어있는 애로우cs이다
        Arrow_C arrow = go.GetComponent<Arrow_C> ();
        //3 화살의 소리를 재생
        myAudio.Play();
        //2 화살에게 타겟 정보를 보낸다
        arrow.TargetSetter (target);
        GetFeverPoint();
    }
    void GetFeverPoint()
    {
        MyDebug.Log("GetFeverPoint");
        if(this.tag=="Range")
        {
            scoreManager.fp+=5;
        } else if(this.tag=="Melee")
        {
            scoreManager.fp+=10;
        } else if(this.tag=="Mage")
        {
            scoreManager.fp+=20;
        } else 
        {
            MyDebug.Log("not found");
            return;
        }
    }

    #endregion

    void Initialize () {
        MyDebug.Log("unit2: 이니셜라이즈");
        state = unit2_CState.MoveAndSearch;
        target = goal.transform;
        viewSightCollider.radius = viewDistance;
    }
    #region "GET SET"
    public Transform GetTarget () { return target; }
    #endregion

}