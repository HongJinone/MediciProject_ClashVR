using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 유닛이 목적지를 찾아서 이동 후 공격하고 물리치면 다음 목적지을 찾고 이동 후 공격을 반복적으로 수행하고싶다.
// 목적지
// 나의체력
public enum turret7State {
    Idle,
    Attack,
}
public class Turret_7 : MonoBehaviour {
    public turret7State state;
    NavMeshAgent navMeshagent;
    public Transform target;
    public float viewDistance;
    public GameObject bulletPrefab;
    public Transform bulletPoint; //

    HP hp;
    TurretDetection turretDetection;
    SphereCollider viewSightCollider;
    public float attackRange = 3;
    float currentTime;
    float attackTime = 1;

    #region UnityMonoBehaviour
    void Awake () {
        viewSightCollider = GetComponentInChildren<SphereCollider> ();
        hp = GetComponentInChildren<HP> ();
        turretDetection = GetComponentInChildren<TurretDetection>();
    }
    void Start () {
        Initialize ();
    }
    void FixedUpdate () {
        //본인이 사망하면 아래를 업데이트하지 않는다.
        if (hp.isDead) {
            return;
        }

        Debug.Log ("Turret State :: " + state);
        //타겟이 있으면
        if (state == turret7State.Attack && turretDetection.targetIsDetected) {
            Attack (target.position);
        } else {
            Idle (); //애니메이션용?
        }
    }
    #endregion


    void Attack (Vector3 _targetPostion) {
        Debug.Log("turret: target=" + target);
        currentTime += Time.deltaTime;
        //타겟이 없다면 타겟=넥서스로 초기화
        if (!target) 
        {
            return;
        }
        // 시간차 공격을 시도한다.
        float dis = Vector3.Distance (transform.position, target.position);
        if (dis <= attackRange && currentTime > attackTime) 
        {
            Shoot ();
            Debug.Log("Turret: Shoot");
            currentTime = 0;
        } else if (dis > attackRange) {
            //터렛은 다음 타겟을 찾는다 > 업데이트인데 어떻게 할까?
            //타겟이 없으면 가만히 있는다.
            Idle();
        }
    }

    #region Collision

    void Shoot () {
        //1 총알을 생성
        GameObject go = Instantiate (bulletPrefab, bulletPoint.position, Quaternion.identity);
        //애로우인데 어떤 애로우냐? 프리팹에 들어있는 애로우cs이다
        Arrow_7 bullet = go.GetComponent<Arrow_7> ();
        //2 화살에게 타겟 정보를 보낸다
        bullet.TargetSetter (target);
    }

    #endregion

    void Initialize () {
        state = turret7State.Idle;
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