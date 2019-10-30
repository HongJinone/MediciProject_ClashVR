using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackTest : MonoBehaviour {
    [Header ("Attributes")]
    public float range = 3f; //대상의 공격 사거리
    public float fireRate = 1f; //공격 딜레이, 높을 수록 공속 증가 
    private float fireCountdown = 0f;

    [Header ("Shooting")]
    Transform target; //유닛을 인식하고 싶다
    public Transform firePoint; //총알이 나오는 곳
    public GameObject arrowPrefab; //총알 프리팹

    [Header ("UnitySettingField")]
    public string unitTag = null; //유닛 태그 변수
    void Start () {
        InvokeRepeating ("UpdateTarget", 0f, 0.5f); //2초에 한 번 범위 내 타겟을 인식함
    }
    void UpdateTarget () {
        GameObject[] units = GameObject.FindGameObjectsWithTag (unitTag); //태그로 유닛 식별, 대상은 배열로 저장

        float shortestDis = Mathf.Infinity;
        GameObject nearestUnit = null; //가장 가까운 유닛변수 생성 초기화
        foreach (GameObject unit in units) { //유닛 배열 안에서 터렛이랑 가장 가까운 녀석을 찾는 포문
            float disToUnit = Vector3.Distance (transform.position, unit.transform.position);
            //터렛과 유닛 사이의 거리값
            if (disToUnit < shortestDis) { //더 작은 값이 shortestDis에 저장되도록 구성
                shortestDis = disToUnit;
                nearestUnit = unit;
            }
        }
        if (nearestUnit != null && shortestDis <= range) { //유닛이 존재하고 사거리 내에 위치했을 때
            target = nearestUnit.transform; //가장 가까운 적을 타겟으로 한다.
        } else
            target = null; //그렇지 않으면 타겟은 없는 것이다
        
    }
    void Update () {
        if (target == null) //타겟이 없으면 아래를 수행하지 않는다
            return;
        //총쏘기에 일정한 딜레이(1초)를 주기 위해
        if (fireCountdown <= 0f) { //Shoot을 실행시킨 후 카운트 값에 변화를 준다
            Shoot ();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }
    void Shoot () {
        Debug.Log ("shoot(melee).");
        GameObject arrowGO = (GameObject) Instantiate (arrowPrefab, firePoint.position, firePoint.rotation);
        //투사체를 생성한다
        Arrow arrow = arrowGO.GetComponent<Arrow> (); //Bullet.cs의 어떤 기능을 이 매서드 내에서 사용하겠습니다

        if (arrow != null) //투사체가 생겼다면 타겟 정보를 Bullet.cs에 전달한다.
            arrow.TargetSetter (target);
    }
}