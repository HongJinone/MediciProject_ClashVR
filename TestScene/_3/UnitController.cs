using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 유닛이 목적지를 찾아서 이동 후 공격하고 물리치면 다음 목적지을 찾고 이동 후 공격을 반복적으로 수행하고싶다.
// 목적지
// 나의체력
public class UnitController : MonoBehaviour {
    public HPController hp;
    NavMeshAgent agent;
    float speed = 1f;
    Transform nexus;
    Transform target;
    public Transform GetTarget () { return target; }
    HPController targetHP;
    public GameObject arrowPrefab;
    public Transform attackPoint;
    ArrowTest arrowTest;

    public float attackRange = 3f;
    public enum eState {
        Move,
        Attack,
    }
    eState state;
    public void SetState (eState next) {
        state = next;
        if (next == eState.Move) {
            agent.speed = speed;
        } else {
            agent.speed = 0;
        }
    }
    void Start () {
        agent = GetComponent<NavMeshAgent> ();
        SetState (eState.Move);
        nexus = GameObject.Find ("Nexus1P").transform;
        currentTime = searchTime;

    }

    private void Update () {
        Debug.Log ("유닛: " + state);

        switch (state) {
            case eState.Move:
                UpdateSearch ();
                UpdateMove ();
                break;
            case eState.Attack:
                UpdateAttack ();
                //Shoot();
                break;
        }
    }
    float searchTime = 0.1f;
    void UpdateSearch () {
        // 시간이 흐르다가 검색 시간이 되었을때 검색을 하고 시간 초기화. 너무 빈번한 검색을 하지 않기 위해.. 
        currentTime += Time.deltaTime;
        if (currentTime <= searchTime) {
            return;
        }
        currentTime = 0;
        // 내 반경 3M 내의 적의 목록을 이용해서 가장 가까운 적을 목적지로 설정하고싶다.
        int layer = 1 << LayerMask.NameToLayer ("Turret");
        Collider[] cols = Physics.OverlapSphere (transform.position, 5f, layer);
        if (cols == null || cols.Length == 0) {
            // 내 반경 3M내에 적이없다.
            target = nexus;
            targetHP = nexus.GetComponent<HPController> ();
        } else {
            //적이 있다. 나랑 가장 가까이 있는 것을 찾고싶다.
            float distance = 999999999999f;
            int selectIndex = -1;
            for (int i = 0; i < cols.Length; i++) {
                if (transform != cols[i].transform) {
                    // 거리를 구하고
                    float temp = Vector3.Distance (transform.position, cols[i].transform.position);
                    // 저장된 거리와 비교해서 더 가까우면 그녀석을 선택하자.
                    if (temp < distance) {
                        distance = temp;
                        selectIndex = i;
                    }
                }
            }
            if (selectIndex == -1) {
                target = nexus;
                targetHP = nexus.GetComponent<HPController> ();

            } else {
                target = cols[selectIndex].transform;
                targetHP = target.GetComponent<HPController> ();
            }
        }
    }

    void UpdateMove () {
        if (target == null)
            return;
        // 타겟 위치로 움직인다.
        else
            agent.destination = target.position;
        // 대상과 일정 거리 이상 가까워지면 
        if ((Vector3.Distance (target.position, transform.position) < attackRange)) {
            //공격 상태로   
            SetState (eState.Attack);
        }
    }

    float currentTime;
    float attackTime = 1;
    void UpdateAttack () {
        //시간차 공격을 하고 싶다(공격 - 공격대기)
        // 시간이 흐르다가
        currentTime += Time.deltaTime;
        // 만약 시간이 공격시간을 초과하면
        if (currentTime > attackTime) {
            // 공격을 시도한다.
            Shoot ();
           // 1. 상대방 체력을 확인
            if (targetHP.currentHealth <= 0) {
                // 1-1. 만약 상대방의 체력이 0 이하이면 새로운 타겟을 검색(setstate->search)
                // target의 레이어를 NonTurret으로 변경한다.
                //Destroy (target.gameObject);
                target = null;
                targetHP = null;
                SetState (eState.Move);
            } else {
                // 1-2. 만약 상대방의 체력이 0 이상이면 
                // 1-2-2. 만약 사정거리 밖이면 그 타겟한테 이동(setstate->move)
                if ((Vector3.Distance (target.position, transform.position) >= attackRange)) {
                    //공격 상태로   
                    SetState (eState.Move);
                } else {
                    // 1-2-1. 만약 사정거리 안에 있으면 그 타겟을 공격(setstate->attack)
                }
            }

            // 시간을 0으로 되돌린다.
            currentTime = 0;
            Debug.Log ("유닛의 타겟: " + target);
        }

    }

    void Shoot () {
        //targetHP.GetDamage(10f);
        // Transform targetInfo = target;

        Instantiate(arrowPrefab, transform.position, Quaternion.identity);

        Debug.Log ("유닛의 공격");
        //at.Seek (target);

    }

    // void OnAttackCall () {
    //     Debug.Log ("유닛: 공격 신호를 받았습니다");
    //     Debug.Log ("유닛: target은 " + target);
    //     float dist = Vector3.Distance (target.transform.position, transform.position);

    //     if (dist <= stopDistance)
    //     // 공격하고싶다.
    //     {
    //         agent.speed = 0;
    //         state = eState.Attack;
    //     } else {
    //         agent.speed = 1;
    //         state = eState.Move;
    //     }
    // }
}