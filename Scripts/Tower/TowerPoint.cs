using UnityEngine;

public class TowerPoint : MonoBehaviour
{

     public Transform point;
     public GameObject  tower;
     public GameObject Helper;
     private CoinsNewValue _coinsNewValue;
     public int id;

     private void Start()
     {
          TowerPointCreatingController();
     }

     public void TowerPointCreatingController()
     {
          if (PlayerPrefs.GetInt("Tower " + id) == 1)
          {
               BuyHelperManager.Instance.TowerCreate(tower, point);
               gameObject.SetActive(false);
          }
     }

     public void SetData(int status)
     {
          PlayerPrefs.SetInt("Tower " + id, status);
     }

     public void CreateTower()
     {
          BuyHelperManager.Instance.TowerCreate(tower, point);
          SetData(1);
     }
}





