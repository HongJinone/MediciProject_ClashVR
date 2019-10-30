using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 유닛이 목적지를 찾아서 이동 후 공격하고 물리치면 다음 목적지을 찾고 이동 후 공격을 반복적으로 수행하고싶다.
// 목적지
// 나의체력
public class TurretController : MonoBehaviour {
    [Header ("Attack")]
    Transform target;
    public float attackRange = 3f;

    [Header ("Status")]
    public HPController myHp;
    HPController targetHP;

    public enum eState {
        Search,
        Attack,
    }
    eState state;
    public void SetState (eState next) {
        state = next;
    }
    void Start () {
        SetState (eState.Search);
        currentTime = searchTime;
    }

    private void Update () {
        Debug.Log ("터렛:" +state);
        switch (state) {
            case eState.Search:
                UpdateSearch ();
                break;
            case eState.Attack:
                UpdateAttack ();
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
        int layer = 1 << LayerMask.NameToLayer ("Unit");
        Collider[] cols = Physics.OverlapSphere (transform.position, 3f, layer);
        if (cols == null || cols.Length == 0) {
            // 내 반경 3M내에 적이없다.
            return;
        } else {
            Debug.Log("적이 있네?");
            //적이 있다. 나랑 가장 가까이 있는 것을 찾고싶다.
            float distance = 999999999999f;
            int selectIndex = -1;
            for (int i = 0; i < cols.Length; i++) {
                if (transform != cols[i].transform) //내 위치를 찾아선 안 되고
                {
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
                return;
            } else {
                target = cols[selectIndex].transform;
            
                targetHP = target.GetComponent<HPController> ();
                //터렛이 club을 인식하는 문제//
                Debug.Log("터렛: "+target);
                SetState(eState.Attack);
            }
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
                Destroy (target.gameObject); //나중에 투사체에 넣어야.
                target = null;
                targetHP = null;
                SetState (eState.Search);
            } else {
                // 1-2. 만약 상대방의 체력이 0 이상이면 
                // 1-2-2. 만약 사정거리 밖이면 다른 타겟을 탐색(setstate->search)
                if ((Vector3.Distance (target.position, transform.position) > attackRange)) {
                    SetState (eState.Search);
                } else {
                    // 1-2-1. 만약 사정거리 안에 있으면 그 타겟을 공격(setstate->attack)
                }
            }
            // 시간을 0으로 되돌린다.
            currentTime = 0;
        }
    }

    void Shoot () {
        //targetHP.GetDamage (10f);
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