using System;
using UnityEngine;

[System.Serializable]
public struct CoinsNewValue
{
     public int coins;
}


public class AreaManager : Singleton<AreaManager>
{
     private const string _TagArea = "Area";
     public GameObject[] plans;
     private CoinsNewValue[] coins;
     public GameObject coinsArea;
     public int current = 0;
     public EnemyManager enemyManager;

     internal void NextPlan(UnlockScr unlockScr)
     {
          PlayerPrefs.SetInt(_TagArea, PlayerPrefs.GetInt(_TagArea) + 1);
          AreaController(unlockScr);
     }

     private void Awake() => ValueCountController();

     private void ValueCountController()
     {
          coins = new CoinsNewValue[10];

          int val = 30;

          for (int i = 0; i < coins.Length; i++)
          {
               coins[i].coins = val;
               val += 1;
          }

          for (int i = 0; i < PlayerPrefs.GetInt(_TagArea); i++)
               AreaController(null);
     }

     public void AreaController(UnlockScr unlockScr)
     {
          plans[current].GetComponent<Area>().Close();

          current++;
          
          if (current >= plans.Length) { coinsArea.SetActive(false); return; }

          plans[current].SetActive(true);

          Area area = plans[current].gameObject.GetComponent<Area>();

          Transform point = area.GetCoinsPoint();

          enemyManager.Add(area.GetPointEnemyForThisArea());

          area.DoorOpen();

          NextCoinArea(point);

     }

     private void NextCoinArea(Transform point)
     {
          coinsArea.gameObject.SetActive(true);
          coinsArea.transform.SetParent(point);
          coinsArea.transform.localPosition = Vector3.zero;
          coinsArea.GetComponent<UnlockScr>().SetData(coins[current].coins);
     }
}
