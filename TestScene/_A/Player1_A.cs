using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_A : MonoBehaviour {
    public GameObject unit1Prefab;
    public GameObject unit2Prefab;

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
        
        Vector3 mousePos = Input.mousePosition;
        int layer = 1 << LayerMask.NameToLayer("Floor");
        if (Input.GetMouseButtonDown (0)) 
        {
          
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit, 1000f, layer)) {         
                GameObject go = Instantiate (unit1Prefab, hit.point, Quaternion.identity);
                go.transform.position = hit.point;
            } 
           
            //or for tandom rotarion use Quaternion.LookRotation(Random.insideUnitSphere)
        } else if(Input.GetKeyDown("space"))
        {     
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit, 1000f, layer)) {
        
                 Instantiate (unit2Prefab, hit.point, Quaternion.identity);
            } 
        }

        
    }

}