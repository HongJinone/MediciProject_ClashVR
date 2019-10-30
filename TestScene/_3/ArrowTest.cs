using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTest : MonoBehaviour

{
    [SerializeField]
    UnitController unitController;
    [SerializeField]
    Transform target;
    public void SetTarget (Transform _value) {
        Debug.Log ("총알: 타겟은 " + target.position);
        target = _value;
    }
    public float speed = 70f;
    public int damage = 20;
    // public void Seek (Transform _target) {

    //     target = _target;
    // }

    private void Awake () {

    }

    private void Start () {
        Init ();
    }

    private void FixedUpdate () {
        Move ();
        
    }
    void Update () {
        // if (target == null) { //목표 없는 총알은 없애버리겠다
        //     Debug.Log("타겟은 "+ target + ", 빼에에엥");
        //     Destroy (gameObject);
        //     return;
        // }
    
    }

    public void Init () {
        unitController = GameObject.FindWithTag ("Player").GetComponent<UnitController> ();
        target = unitController.GetTarget ();
    }
    void Move () {
        Vector3 direction = target.position - transform.position;
     
        this.transform.Translate(target.position * speed * Time.deltaTime);
        if (direction.magnitude <= speed * Time.deltaTime) { //대상과의 거리차이가 굉장히 작아지면
            HitTarget ();
            return;
        }
      
        Debug.Log ("Arrow , target position : " + target.position);
        Debug.Log ("Arrow , target position : " + target.name);
        // transform.LookAt (target);
        // transform.Translate (target.position * speed * Time.deltaTime);
    }
    void HitTarget () {
        Debug.Log ("맞았어");
        //Destroy(target.gameObject);
        //Damage (target);
        Destroy (this.gameObject);

        //파티클이펙트 일단 스킵:https://youtu.be/QKhn2kl9_8I 22:38~
    }
    //          아래에 트랜스폼 변수가 들어갈 것이다(target의 속성이기도 하다)
    void Damage (Transform unit) {
        HPController targetHP = unit.GetComponent<HPController> ();
        if (targetHP != null) {
            targetHP.GetDamage (damage);
        }
    }
}
// {
//     [Header ("Attributes")]
//     public float speed = 5;
//     public float damage = 20f;

//     [Header("Destination")]
//     Transform target;

//     public void Seek (Transform enemyPosition) { //Turret, Unit에서 호출되어 실행되는 매서드
//         target = enemyPosition;
//     }

//     void Update () {
//         if(target==null) {
//             Destroy(this.gameObject);   //타겟이 사라지거나 없으면 총알도 바로 사라지게
//             return;
//         }    
//         Vector3 dir = target.position - transform.position; //총알의 방향 설정
//         float disThisFrame = speed * Time.deltaTime;        //총알의 힘

//         transform.Translate (dir.normalized * disThisFrame, Space.World);
//                                             //총알이 파괴되거나 어디 맞지 않았으면 
//                                             //주어진 방향과 힘으로 이동한다
//         //지정된 데미지값을 전달한다

//         Destroy(this.gameObject, 2f);
//     }
//     private void OnCollisionEnter(Collision other) {
//         if(other.gameObject.tag=="Turret"){

//             Destroy(this.gameObject);
//         }
//     }
// }