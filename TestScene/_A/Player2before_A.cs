using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2before_A : MonoBehaviour {
    public static Player2_A instance;
    public GameObject rangePrefab;
    public GameObject unit2Prefab;
    public Vector3 offset;
    public Transform spawnPoint_1;
    ScoreManager_A scoreManager;
    private void Start () {
        scoreManager = GameObject.Find ("ScoreManager").GetComponent<ScoreManager_A> ();
    }
    // private void Update () {

    // }

    void InstantiateUnit (GameObject _unitPrefab) {
        GameObject unitPrefab = _unitPrefab;
        Debug.Log ("Player1_A: unitPrefab= " + unitPrefab);
        GameObject go = Instantiate (unitPrefab, spawnPoint_1.position, Quaternion.identity);
    }
    public void SummonRange () {
        // if (scoreManager.fp >= 100) {
        //     Debug.Log ("Player2_A: Range 소환");
        //     InstantiateUnit (rangePrefab);
        //     scoreManager.fp -= 100;
        // } else {
        //     Debug.Log ("유닛을 소환하기 위한 열의가 부족합니다.");
        // }
    }

}