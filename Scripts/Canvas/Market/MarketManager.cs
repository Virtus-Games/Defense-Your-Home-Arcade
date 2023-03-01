using UnityEditor;
using UnityEngine;

public class MarketManager : Singleton<MarketManager>
{
     public MarketItemDataSO marketItemDataSO;
     public Transform headParent;
     public Transform pageParent;

     private TabsUIHorizontal tabsUIHorizontal;
     private void Awake() => Initiziating();

     public void Initiziating()
     {
          marketItemDataSO.Initiliazing(headParent, pageParent);
     }

     public void EnabledTabUI()
     {
          Debug.Log("EnabledTabUI");
          GetComponent<TabsUIHorizontal>().enabled = true;
          GetComponent<TabsUIHorizontal>().Initiazied();
    
     }

     public void BuyItem(Item item, MarketItems marketItems)
     {

          if (PlayerData.playerData.totalMoney >= marketItemDataSO.GetItem(item).money)
          {
               PlayerData.playerData.totalMoney -= item.money;
               PlayerPrefs.SetInt(item.name + item.id, item.id);
               PlayerData.Instance.Save();
               marketItems.Show();
          }
          else
               marketItems.CallAnimAnim();


     }

     internal void Use(Item item)
     {
          // TODO: FIND CHARACKTER
     }

     public bool GetStringAndId(Item item)
     {
          string nameAndId = item.name + item.id;

          if (PlayerPrefs.GetInt(nameAndId) == item.id)
               return true;
          else
               return false;
     }
}


[CustomEditor(typeof(MarketManager))]
public class MarketManagerEditor : Editor
{
     public override void OnInspectorGUI()
     {

          base.OnInspectorGUI();

          MarketManager marketManager = target as MarketManager;
          if (GUILayout.Button(" Debug it"))
               marketManager.Initiziating();

     }
}


