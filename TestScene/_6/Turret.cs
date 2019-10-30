using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 유닛이 목적지를 찾아서 이동 후 공격하고 물리치면 다음 목적지을 찾고 이동 후 공격을 반복적으로 수행하고싶다.
// 목적지
// 나의체력
public class Turret : MonoBehaviour {

    public enum eState {
        Idle,
        Attack,
    }
    eState state;
    NavMeshAgent navMeshagent;
    Transform target;
    public float viewDistance;
    public GameObject bulletPrefab;
    public Transform bulletPoint; //
    bool targetIsDetected;
    HP hp;

    SphereCollider viewSightCollider;

    #region UnityMonoBehaviour
    void Awake () {
        viewSightCollider = GetComponent<SphereCollider> ();
        hp = GetComponent<HP> ();
    }
    void Start () {
        Initialize ();
    }
    void FixedUpdate () {
        //본인이 사망하면 아래를 업데이트하지 않는다.
        if (hp.isDead) {
            return;
        }

        Debug.Log ("State :: " + state);
        //타겟이 있으면
        if (target && state == eState.Attack && targetIsDetected) {
            Attack (target.position);
        } else {
            Idle (); //애니메이션용?
        }
    }

    #endregion

    // void SearchTarget () // ==> SetTarget 
    // {
    //     //공격해서 적이 죽었을 때
    //     //다음 적을 찾는다
    //     //만약 적이 사정거리 밖으로 나가면
    //     //ChangeTarget
    // }

    void ChangeTarget (Transform _target) {
        //타겟의 변화를 갱신한다.
        target = _target;
    }

    public float attackRange = 3;
    float currentTime;
    float attackTime = 1;
    void Attack (Vector3 _targetPostion) {
        currentTime += Time.deltaTime;
        //타겟이 없다면 타겟=넥서스로 초기화
        if (!target) {

            return;
        }

        // 시간차 공격을 시도한다.
        if (currentTime > attackTime) {
            Shoot ();
            currentTime = 0;
        }

        Debug.Log ("Attack");
    }

    #region Collision

    void Shoot () {
        //1 총알을 생성
        GameObject go = Instantiate (bulletPrefab, bulletPoint.position, Quaternion.identity);
        //애로우인데 어떤 애로우냐? 프리팹에 들어있는 애로우cs이다
        Arrow bullet = go.GetComponent<Arrow> ();
        //2 화살에게 타겟 정보를 보낸다
        bullet.TargetSetter (target);
    }

    private void OnTriggerStay (Collider other) {
        if (this.gameObject.layer == 8) // player1Unit
        {
            if (other.gameObject.layer == 9) {
                ChangeTarget (other.transform);
                targetIsDetected = true;
                state = eState.Attack;
            }
        } else if (this.gameObject.layer == 9) //player2Unit
        {
            if (other.gameObject.layer == 8) {
                ChangeTarget (other.transform);
                targetIsDetected = true;
                state = eState.Attack;
            }
        } else {
            targetIsDetected = false;
        }

    }
    #endregion

    void Initialize () {
        state = eState.Idle;
        //targetIsDetected=true;
        viewDistance = 5.0f;
        viewSightCollider.radius = viewDistance;
    }
    void Idle()
    {
        //
    }
    #region "GET SET"
    public Transform GetTarget () { return target; }
    #endregion

}