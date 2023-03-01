using UnityEngine;


using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(MarketItemDataSO))]
public class MarketItemDataSOEditor : Editor
{

     public override void OnInspectorGUI()
     {
          base.OnInspectorGUI();
          MarketItemDataSO marketItemDataSO = target as MarketItemDataSO;

          if (GUILayout.Button(" Debug it"))
          {
               //marketItemDataSO.Initiliazing();
          }
     }
}

[CreateAssetMenu(fileName = "New Market Data")]
public class MarketItemDataSO : ScriptableObject
{
     public GameObject TabButtonPrefab;
     public GameObject PageContentPrefab;
     public GameObject Item;
     public MarketItem items;

     public Sprite Lock;

     private List<GameObject> pages = new List<GameObject>();

     public void Initiliazing(Transform headParent, Transform pageParent)
     {
          pages.Clear();

          for (int i = 0; i < items.data.Length; i++)
          {
               GameObject headTabButton = Instantiate(TabButtonPrefab, headParent);
               headTabButton.GetComponent<TabButtonUI>().Initiliazing(items.data[i].tabButtonTitle);
               GameObject page = Instantiate(PageContentPrefab, pageParent);
               pages.Add(page);
          }

          for (int i = 0; i < items.data.Length; i++)
          {
               for (int j = 0; j < items.data[i].items.Length; j++)
               {

                    GameObject itemData = Instantiate(Item, pages[i].transform);

                    Item marketItem = items.data[i].items[j];
                    MarketItems marketItems = itemData.GetComponent<MarketItems>();

                    marketItems.Initiliazing(marketItem);

                    string nameAndId = (marketItem.name + marketItem.id).ToString();

                    if (marketItem.startUse)
                         if (!PlayerPrefs.HasKey(nameAndId))
                              PlayerPrefs.SetInt(nameAndId, marketItem.id);

                    if (MarketManager.Instance.GetStringAndId(marketItem))
                         marketItems.Show();
 
                    if (j == items.data[i].items.Length - 1)
                         MarketManager.Instance.EnabledTabUI();
               }
          }
     }


     private Item _item;
     public Item GetItem(Item item)
     {
          _item = new Item();

          for (int i = 0; i < items.data.Length; i++)
          {
               for (int j = 0; j < items.data[i].items.Length; j++)
               {
                    if (items.data[i].items[j].id == item.id)
                    {
                         _item = items.data[i].items[j];
                         continue;
                    }
               }

          }

          return _item;

     }
}



[System.Serializable]
public class MarketItem
{
     public Data[] data;
}


[System.Serializable]
public class Data
{
     public string tabButtonTitle;
     public Item[] items;

}


[System.Serializable]
public class Item
{

     public string name;

     [HideInInspector]
     public int id;
     public int money;
     public Sprite sprite;
     public GameObject itemPrefab;
     public bool startUse = false;
}
