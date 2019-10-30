using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestTest : MonoBehaviour {
    public GameObject unitPrefab;

    public Vector3 offset;
    // private void Update() {
    //     if(Input.GetKeyDown(KeyCode.Mouse0)){
    //         Vector3 worldPoint=Camera.main.ScreenToWorldPoint
    //         (Input.mousePosition,Camera.MonoOrStereoscopicEye.Mono);
    //         Vector3 adjustZ=new Vector3(worldPoint.x,worldPoint.y, unitPrefab.transform.position.z);

    //         Spawn(unitPrefab,adjustZ);
    //     }
    // }
    // void Spawn(GameObejct _obj ,Vector3 _position){
    //     Instantiate(_obj,_position,Quaternion.identity);

    // }

    private void Update () {

        Vector3 mousePos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0f);
        if (Input.GetMouseButtonDown (0)) {
            Vector3 wordPos;
            Ray ray = Camera.main.ScreenPointToRay (mousePos);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit, 1000f)) {
                wordPos = hit.point;
                 Instantiate (unitPrefab, wordPos + offset, Quaternion.identity);
            } else {
                wordPos = Camera.main.ScreenToWorldPoint (mousePos);
            }
           
            //or for tandom rotarion use Quaternion.LookRotation(Random.insideUnitSphere)
        }

        
    }

}