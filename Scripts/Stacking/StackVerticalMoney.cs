using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StackVerticalMoney : Singleton<StackVerticalMoney>
{
     // public float V = 2f;
     // public Transform Parent;
     // public float distance = 0.5f;
     // private Transform _prevObj;
     // private Vector3 lcalPosition;
     // public GameObject moneyPrefab;
     // public List<GameObject> _moneyList = new List<GameObject>();
     
     // public void CreateStack(int val, Transform pos)
     // {
     //      Transform prev = Parent;
     //      for (int i = 0; i < val; i++)
     //           StartStack(prev, pos);

     // }

     // public void StartStack(Transform money, Transform pos)
     // {

     //      if (_prevObj == null)
     //      {
     //           lcalPosition = Parent.localPosition;
     //           _prevObj = Parent;
     //      }

     //      GameObject obj = Instantiate(moneyPrefab, money.transform.position, Quaternion.identity);


     //      obj.transform.parent = Parent;
     //      lcalPosition = _prevObj.localPosition;

     //      lcalPosition.y += distance;
     //      lcalPosition.x = 0;
     //      lcalPosition.z = 0;

     //      obj.transform.localPosition = lcalPosition;
     //      obj.transform.localRotation = Quaternion.identity;

     //      _prevObj = obj.transform;
     //      _moneyList.Add(obj.gameObject);

     //      //StartCoroutine(MoveToPosition(obj.transform,_prevObj));

     // }

     // public void EndPosition(GameObject obj)
     // {
     //      _prevObj = obj.transform;
     //      _moneyList.Add(obj.gameObject);
     // }

     // public void AreaMoney(CoinsAreaController controller, Transform area)
     // {
     //      int speed = 13;

     //      if (_moneyList.Count == 0)
     //           return;

     //      ListReweds(true);

     //      GameObject obj = _moneyList[0].gameObject;

     //      obj.transform.parent = null;

     //      obj.GetComponent<Money>().SetData(area, speed, controller);

     //      speed -= 3;
     //      speed = Mathf.Max(5, speed);
     //      _moneyList.Remove(obj);

     // }

     // bool rewrs;
     // public void ListReweds(bool rewers)
     // {
     //      if (rewrs != rewers)
     //      {
     //           rewrs = rewers;
     //           _moneyList.Reverse();
     //      }
     // }

     // public int GetListCount()
     // {

     //      if (_moneyList.Count > 0)
     //           return _moneyList.Count;
     //      else
     //           return 0;
     // }
     // IEnumerator MoveToPosition(Transform obj, Transform prevObj)
     // {
     //      while (Vector3.Distance(obj.position, prevObj.position) > 0.1f)
     //      {
     //           obj.localPosition = Vector3.Lerp(obj.localPosition, prevObj.position, Time.deltaTime * V);
     //           yield return null;
     //      }
     // }
}