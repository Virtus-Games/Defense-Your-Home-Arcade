using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MarketItems : MonoBehaviour
{
     [SerializeField] private Image currentImage;
     [SerializeField] private GameObject Lock;

     [HideInInspector]

     private Item _item;
     public int id;

     public void Initiliazing(Item item)
     {
          _item = item;
          SetData();
     }


     public void CallAnimAnim()
     {

     }

     private void SetData()
     {
          this.id = _item.id;
          this.currentImage.sprite = _item.sprite;
     }

     public void BuyItem()
     {

          if (MarketManager.Instance.GetStringAndId(_item))
               MarketManager.Instance.Use(_item);
          else
               MarketManager.Instance.BuyItem(_item, this);
     }

     public void Show() => Lock.SetActive(false);

}
