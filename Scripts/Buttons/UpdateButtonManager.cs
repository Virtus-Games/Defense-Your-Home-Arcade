using UnityEngine;
using TMPro;
public class UpdateButtonManager : MonoBehaviour
{
     private const string _damageTextKey = "Upgrade Damage \n";
     private const string _upgradeSpeedKey = "Upgrade Speed \n";
     private const string _damageKey = "Damage";
     private const string _speedKey = "Speed";
     public GameObject UpdateWithXScreen, UpdateScreenDesign;
     public TextMeshProUGUI damageText, speedText;
     private Manager _manager;
     public int speedMoney = 25;
     public int damageMoney = 25;

     private void Start()
     {
          _manager = PlayerMovement.Instance.GetComponent<Manager>();
          Data();
     }

     private void Data()
     {
          string playerAttack = (PlayerPrefs.GetInt(_speedKey, speedMoney)).ToString();
          speedText.SetText(_upgradeSpeedKey + playerAttack + "$");
          string PlayerDamage = (PlayerPrefs.GetInt(_damageKey), damageMoney).ToString();
          damageText.SetText(_damageTextKey + PlayerDamage + "$");
     }

     public void UpdateSpeed()
     {
          if (!UpdateMoney(speedMoney, _speedKey)) return;
          _manager.Speed();
     }

     public void UpdateDamage()
     {
          if (!UpdateMoney(damageMoney, _damageKey)) return;
          _manager.Damage();
     }

     public bool UpdateMoney(int money, string tag)
     {
          if (PlayerData.playerData.totalMoney >= money)
          {
               PlayerData.playerData.totalMoney -= money;
               PlayerData.Instance.Save();
               UIManager.Instance.UpdateCoin();
               PlayerPrefs.SetInt(tag, PlayerPrefs.GetInt(tag) + money);
               Data();
               return true;
          }
          else
               return false;
     }

     public void CloseUpdateScreen()
     {
          UpdateWithXScreen.SetActive(false);
          UpdateScreenDesign.SetActive(false);
     }
}