using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//데미지를 보내는 기능 추가
public enum turretCState {
    Idle,
    Attack,
}
public class Turret_C : MonoBehaviour {
    public turretCState state;
    NavMeshAgent navMeshagent;
    public Transform target;
    public float viewDistance;
    public GameObject bulletPrefab;
    public Transform bulletPoint; //
    public int myDmg=20;
    public HP_C hp;
    AudioSource myAudio;
    public ScoreManager_C scoreManager;
    

    TurretDetection_C turretDetection;
    SphereCollider viewSightCollider;
    public float attackRange = 3;
    float currentTime;
    float attackTime = 1;

    #region UnityMonoBehaviour
    void Awake () {
        viewSightCollider = GetComponentInChildren<SphereCollider> ();
        //hp = GetComponentInChildren<HP_C> ();
        turretDetection = GetComponentInChildren<TurretDetection_C>();
        myAudio = GetComponent<AudioSource>();
    }
    void Start () {
        Initialize ();
    }
    void FixedUpdate () {
        //본인이 사망하면 아래를 업데이트하지 않는다.
        if (hp.isDead) {
            return;
        }

        MyDebug.Log ("Turret State :: " + state);
        //타겟이 있으면
        if (state == turretCState.Attack && turretDetection.targetIsDetected) {
            if(target)
            {
                Attack (target.position);
            }    
        } else {
            Idle (); //애니메이션용?
        }
    }
    #endregion


    void Attack (Vector3 _targetPostion) {
        MyDebug.Log("turret: target=" + target);
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
        Arrow_C bullet = go.GetComponent<Arrow_C> ();
        //2 화살에게 타겟 정보를 보낸다

        myAudio.Play();

        bullet.TargetSetter (target);
        bullet.damage=myDmg;
        if(scoreManager!=null)
        {
            scoreManager.fp+=20;
        }
    }

    #endregion

    void Initialize () {
        state = turretCState.Idle;
        //targetIsDetected=true;
        //viewDistance = 14.0f;
        viewSightCollider.radius = viewDistance;
    }
    void Idle()
    {
        //
    }
    //터렛 업그레이드 전용
    public void SetDamage(int _value)
    {
        myDmg = myDmg + _value;
    }
    #region "GET SET"
    public Transform GetTarget () { return target; }
    #endregion

}