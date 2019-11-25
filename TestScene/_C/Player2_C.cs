using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PosIndex
    {
        Left,
        Mid,
        Right
    }

public enum UnitType_C
    {
        Melee,
        Mage,
        Range
    }
public class Player2_C : MonoBehaviour {
    public static Player2_C instance;
    public Vector3 offset;
    ScoreManager_C scoreManager;
    PosIndex posIndex;
    UnitType_C unitType;
    public GameObject[] summonTypes;
    public Transform[] positions;
    public int meleeCost=150;
    public int mageCost=200;
    public int rangeCost=100;

    private void Start () {
        scoreManager = GameObject.Find ("ScoreManager").GetComponent<ScoreManager_C> ();
    }
    // private void Update () {

    // }
    
    // 1. 어떤 유닛을 만들지 선택하는 UI
//     public void SelectUnitUI(int type)
//     {
// //        InstantiateUnit (rangePrefab, position);
//         // 어떤 유닛을 선택했는지 기억해야 한다.
//         unitType = (UnitType)type;

//         // 2. 소환 버튼을 누르면 위치 선정하는 버튼이 나온다.
//         positionButton.SetActive(true);
//         unitSelectButton.SetActive(false);
//     }

    // 2. 어디서 생성할지 선택하는 UI
    public void SelectPositionUI(int index)
    {
    //  - 선택한 위치에서 내가 원하는 유닛을 생성하고 싶다.
        posIndex = (PosIndex)index;
    //  - 유닛 종류에 따라서 게임 설정값을 달리하고 싶다.
    }
    
    public void SelectUnitUI (int _type) {
        //유닛 버튼을 누르면 유닛의 int 번호를 저장하고싶다
        unitType =(UnitType_C)_type;
        if(scoreManager.fp>=meleeCost&&unitType==UnitType_C.Melee)
        {
            InstantiateUnit(summonTypes[0],positions[(int)posIndex]);
            scoreManager.fp-=meleeCost;
        } else if(scoreManager.fp>=mageCost&&unitType==UnitType_C.Mage)
        {
            InstantiateUnit(summonTypes[1],positions[(int)posIndex]);
            scoreManager.fp-=mageCost;
        } else if(scoreManager.fp>=rangeCost&&unitType==UnitType_C.Range)
        {
            InstantiateUnit(summonTypes[2],positions[(int)posIndex]);
            scoreManager.fp-=rangeCost;
        } else {
            Debug.Log("유닛을 소환하기 위한 열의가 부족합니다.");
        }
    }

    public void InstantiateUnit (GameObject _unitPrefab, Transform _unitPosition) {
        GameObject unitPrefab = _unitPrefab;
        Transform unitPosition= _unitPosition; 
       //MyDebug.Log ("Player1_A: unitPrefab= " + unitPrefab);
        GameObject go = Instantiate (unitPrefab, unitPosition.position, Quaternion.identity);
    }
     // 3. 위치 버튼을 누르면 그 장소에 1.의 유닛을 소환한다.

    // 1. 원거리 유닛 소환 버튼을 누르면 위치 선정을 하고싶다.
    //    근거리 
    //    마법사 
    // (위치는 모두가 같은 위치를 공유한다.)
    //  생성될 위치     
    //  유닛 종류


    // 어떤 유닛을 선택했는지 기억해야 한다.
    
    // 2. 소환 버튼을 누르면 위치 선정하는 버튼이 나온다.

    // 3. 버튼이 나오면 1.의 유닛을 소환한다.
    // 

}