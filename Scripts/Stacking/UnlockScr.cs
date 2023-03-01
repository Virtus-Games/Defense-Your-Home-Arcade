using UnityEngine;
using DG.Tweening;
using TMPro;

public class UnlockScr : MonoBehaviour
{
     // Start is called before the first frame update
     public int numToUnlock = 3;
     public GameObject toUnlock;
     public TextMeshProUGUI text;
     private int numAdded;

     public void SetData(int count)
     {
          numAdded = 0;
          isCreate = false;
          numToUnlock = count;
          text.text = string.Format("{0}/{1}", numAdded, numToUnlock);
     }
     int val;
     private void OnTriggerEnter(Collider other)
     {
          if (other.CompareTag("Player")) val = PlayerData.Instance.GetMoney();
     }

     private void OnEnable() {
          isCreate = false;
          isFinish = false;
          
     }

     private void Start() => text.text = string.Format("{0}/{1}", numAdded, numToUnlock);

     private bool isCreate;


     public bool Equals()
     {
          return numAdded != numToUnlock;
     }

     public bool isFinish;
     public void UnlockItem(int num)
     {
          if (numAdded >= numToUnlock && !isCreate)
          {
               isFinish = true;
               return;
          }

          numAdded++;

          text.text = string.Format("{0}/{1}", numAdded, numToUnlock);

          numAdded = Mathf.Min(numAdded, numToUnlock);

          text.DOBlendableColor(Color.green, 0.5f).onComplete = () =>
          {
               text.DOBlendableColor(Color.black, 0.5f);
          };

          if (numAdded >= numToUnlock && !isCreate)
          {
               Debug.Log("Unlock" + numAdded);
               if (GetComponent<TowerPoint>() != null && !isCreate)
                    GetComponent<TowerPoint>().CreateTower();
               else
               {
                    AreaManager.Instance.NextPlan(this);
                    toUnlock.SetActive(false);
               }

               isCreate = true;
               gameObject.SetActive(false);
          }
     }
}
