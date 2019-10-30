using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// 타겟 쪽으로 이동하고 싶다.
// - 타겟
// - Navmeshagent
public class AIMovement : MonoBehaviour
{
    // - 타겟
    public Transform target;
    //public GameObject turret;
// - Navmeshagent
    NavMeshAgent agent;
    
    private void Start() {

        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.transform.position;
    }
    public BoxCollider turretBoxCollider;
    // private void OnTriggerEnter(Collider other) {
    //     if(other.gameObject == turretCollider)
    //     Debug.Log("유닛: "+other+"를 근접공격");
    // }

    // private void OnTriggerEnter(Collider other) {
    //     if(other.gameObject == turret){

    //         Debug.Log("유닛: "+turret+" 발견");
    //     }
    // }
    // private void OnCollisionEnter(Collision other) {
    //     if(other.gameObject==turret){
    //         Debug.Log("유닛: "+turret+" 근접공격");
    //     }
    // }
}
