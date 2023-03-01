using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenToweManager : Singleton<WoodenToweManager>
{
     public WoodenTowerManagerSO woodenToweManagerSO;
     public int towers = 0;
     public Transform pivot;
     public GameObject helper;
     GameObject _towers;

     private Kule TowersCreate()
     {
          towers++;
          _towers = Instantiate(woodenToweManagerSO.towers[towers], pivot.position, Quaternion.identity);
          _towers.transform.parent = gameObject.transform;
          return _towers.GetComponentInChildren<Kule>();
     }

     public void TowersController(Kule towers)
     {
          towers.DestroyParent();
          Kule kule = TowersCreate();
          Vector3 pivotPosition = kule.GetPivotCharacterPosition();
          GameObject obj = Instantiate(helper, pivotPosition, Quaternion.identity);
          obj.GetComponent<Helper>().HelperAtTower(kule.pivotPos);
          obj.transform.parent = kule.GetPivot();
          obj.transform.localPosition = kule.GetPivotCharacterPosition();
     }
}


