using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WinSO", menuName = "Main Project/WinSO", order = 0)]
public class WinSO : ScriptableObject
{
     public WinSOData winSOData;

     public WinItem GetItem()
     {
          return winSOData.data[PlayerData.playerData.currentWinGiftItem];
     }

}

public class WinSOData
{
     public List<WinItem> data;
}

public class WinItem
{
     [HideInInspector]
     public int id;

     public string title;

     public Sprite image;

}
