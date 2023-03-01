using UnityEngine;
using TMPro;


public class CoinsArea : MonoBehaviour
{

     // private void Start()
     // {
     //      coinsCount = (int)coins / 2;
     //      textCoins.text = coins.ToString();
     // }
     
     // public void SetData(CoinsNewValue newValue)
     // {
     //      _coinsNewValue = newValue;
     //      coins = _coinsNewValue.coins;
     //      coinsCount = (int)coins / 2;
     //      textCoins.text = coins.ToString();
     // }
     //public void RemoveMoney() => AreaManager.Instance.NextPlan();

     // private void OnTriggerStay(Collider other)
     // {

     //      if (other.gameObject.CompareTag("Player"))
     //      {

     //           if (PlayerData.Instance.GetMoney() && coins > 0)
     //           {
     //                StackVerticalMoney stackVerticalMoney = other.GetComponent<StackVerticalMoney>();

     //                int whileCount = stackVerticalMoney.GetListCount();

     //                if (coinsCount > 0 && whileCount > 0)
     //                {
     //                     stackVerticalMoney.AreaMoney(this,PivotMoney);
     //                     coinsCount--;
     //                }
     //           }
     //      }
     // }

     // private void OnTriggerExit(Collider other)
     // {
     //      if (other.gameObject.CompareTag("Player"))
     //      {
     //           StackVerticalMoney stackVerticalMoney = other.GetComponent<StackVerticalMoney>();
     //           stackVerticalMoney.ListReweds(true);
     //      }
     // }
}
