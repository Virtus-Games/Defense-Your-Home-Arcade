using UnityEngine;
using TMPro;

public class PlayerDataItem : MonoBehaviour
{
     private void OnEnable() => PlayerData.onDataChanged += UpdateData;

     private void UpdateData(PlayerDataContainer arg0)
     {
          // Uptade Item Here Demo
          
     }

     private void OnDisable() => PlayerData.onDataChanged -= UpdateData;
}
