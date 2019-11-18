using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum UnitType
    {
        Range,
        Melee,
        Mage
    }
public class Player2_A : MonoBehaviour {
    public static Player2_A instance;
    public GameObject rangePrefab;
    public GameObject meleePrefab;
    public GameObject magePrefab;

    public GameObject unit2Prefab;
    public Vector3 offset;
    public Transform spawnPoint_1;
    ScoreManager_A scoreManager;
    private void Start () {
        scoreManager = GameObject.Find ("ScoreManager").GetComponent<ScoreManager_A> ();
    }
    // private void Update () {

    // }
    
    // 1. 어떤 유닛을 만들지 선택하는 UI
    public void SelectUnitUI(int type)
    {
//        InstantiateUnit (rangePrefab, position);
        // 어떤 유닛을 선택했는지 기억해야 한다.
        unitType = (UnitType)type;

        // 2. 소환 버튼을 누르면 위치 선정하는 버튼이 나온다.
        positionButton.SetActive(true);
        unitSelectButton.SetActive(false);
    }

  

    // 2. 어디서 생성할지 선택하는 UI
    public void SelectPositionUI(int index)
    {
    //  - 선택한 위치에서 내가 원하는 유닛을 생성하고 싶다.
        unitPosition= positions[index];
    //  - 유닛 종류에 따라서 게임 설정값을 달리하고 싶다.
        if(unitType == UnitType.Range)
        {
            SummonRange();
        } else if(unitType==UnitType.Melee)
        {
            SummonMelee();
        } else if(unitType==UnitType.Mage)
        {
            SummonMage();
        }
    }
    
    public void SummonRange () {
        if (scoreManager.fp >= 100) {
            Debug.Log ("Player2_A: Range 소환");
            InstantiateUnit (rangePrefab, unitPosition);
            scoreManager.fp -= 100;
        } else {
            Debug.Log ("Range을 소환하기 위한 열의가 부족합니다.");
        }
        positionButton.SetActive(false);
        unitSelectButton.SetActive(true);
        
    }
    public void SummonMelee () {
        if (scoreManager.fp >= 150) {
            Debug.Log ("Player2_A: Range 소환");
            InstantiateUnit (meleePrefab, unitPosition);
            scoreManager.fp -= 150;
        } else {
            Debug.Log ("Melee을 소환하기 위한 열의가 부족합니다.");
        }
        positionButton.SetActive(false);
        unitSelectButton.SetActive(true);

    }
    public void SummonMage () {
        if (scoreManager.fp >= 200) {
            Debug.Log ("Player2_A: Range 소환");
            InstantiateUnit (magePrefab, unitPosition);
            scoreManager.fp -= 200;
        } else {
            Debug.Log ("Mage을 소환하기 위한 열의가 부족합니다.");
        }
        positionButton.SetActive(false);
        unitSelectButton.SetActive(true);
    }

    public void InstantiateUnit (GameObject _unitPrefab, Transform _unitPosition) {
        GameObject unitPrefab = _unitPrefab;
        Transform unitPosition= _unitPosition;
        Debug.Log ("Player1_A: unitPrefab= " + unitPrefab);
        GameObject go = Instantiate (unitPrefab, unitPosition.position, Quaternion.identity);
    }
     // 3. 위치 버튼을 누르면 그 장소에 1.의 유닛을 소환한다.

    // 1. 원거리 유닛 소환 버튼을 누르면 위치 선정을 하고싶다.
    //    근거리 
    //    마법사 
    // (위치는 모두가 같은 위치를 공유한다.)
    //  생성될 위치     
    //  유닛 종류
    public Transform[] positions;
    public Transform unitPosition;

    public GameObject positionButton;
    public GameObject unitSelectButton;

    UnitType unitType;
    // 어떤 유닛을 선택했는지 기억해야 한다.
    
    // 2. 소환 버튼을 누르면 위치 선정하는 버튼이 나온다.

    // 3. 버튼이 나오면 1.의 유닛을 소환한다.
    // 
}