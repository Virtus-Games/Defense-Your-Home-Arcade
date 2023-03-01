using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
     #region Data
     private const string Music = "Music";
     private const string Vibrate = "Vibrate";
     public GameObject StartPanel, MarketPanel, PlayPanel;
     public GameObject SettingsPanel, VictoryPanel, DefeatPanel;

     [Space(5)]
     [Header("Level and Coins Panel")]
     public TextMeshProUGUI levelName;
     public TextMeshProUGUI CoinText;

     [Header("Coins Panel")]
     [Space(5)]
     public TextMeshProUGUI TotalMoneyText;

     [Tooltip("Victory Panel Coin Movement")]
     public GameObject CoinPrefab;
     public Transform CoinParent;
     public Transform MoneyImage;
     private int _money;
     private int _totalMoney;

     public WinSO winSO;

     #endregion
     private void Start() => UpdateGameState(GAMESTATE.START);
     private void OnEnable()
     {
          GameManager.OnGameStateChanged += UpdateGameState;
          LevelManager.OnLevelLoaded += UpdateLevel;
          PlayerData.onDataChanged += UpdateData;
     }
     private void OnDisable()
     {
          GameManager.OnGameStateChanged -= UpdateGameState;
          LevelManager.OnLevelLoaded -= UpdateLevel;
          PlayerData.onDataChanged -= UpdateData;
     }
     private void UpdateData(PlayerDataContainer arg0) => UpdateCoin();

     #region  UI MANAGER

     private void UpdateLevel(bool arg0)
     {
          if (arg0)
          {

          }
     }

     private void UpdateGameState(GAMESTATE switchState)
     {
          switch (switchState)
          {
               case GAMESTATE.START:
                    NewMethod();

                    break;
               case GAMESTATE.PLAY:
                    PlayMode();
                    break;
               case GAMESTATE.VICTORY:
                    VictoryMode();
                    break;
               case GAMESTATE.MARKET:
                    MarketMode();
                    break;
               case GAMESTATE.DEFEAT:
                    DefautMode();
                    break;
               default:
                    UpdateUI(null);
                    break;

          }
     }

     private void MarketMode()
     {
          UpdateUI(MarketPanel);
          BannerController(false);
     }

     private void NewMethod()
     {
          _money = 0;
          _totalMoney = 0;
          UpdateUI(StartPanel);
          int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
          levelName.SetText("Level " + (currentLevel).ToString());
     }

     private void VictoryMode()
     {
          UpdateUI(VictoryPanel);
          _totalMoney = _money;
          CoinsGoToMoneyBox();
          BannerController(true);
     }

     private void DefautMode()
     {
          //AdmonController.Instance.DefeatIntersitial();
          UpdateUI(DefeatPanel);
          BannerController(true);
     }

     private void PlayMode()
     {
          UpdateUI(PlayPanel);
          BannerController(false);
     }

     public void UpdateUI(GameObject obj)
     {
          StartPanel.SetActive(false);
          MarketPanel.SetActive(false);
          PlayPanel.SetActive(false);
          VictoryPanel.SetActive(false);
          DefeatPanel.SetActive(false);
          SettingsPanel.SetActive(false);

          if (obj != null)
               obj.SetActive(true);

     }

     #endregion

     #region Coins
     public void UpdateCoin()
     {
          _totalMoney = GetCoin();
          CoinText.text = _totalMoney.ToString();
     }
     public int GetCoin() => PlayerData.playerData.totalMoney;
     public void SetMoneyCalculate(int getMoney) => _money += getMoney;
     private void SetCoin(int v)
     {
          _totalMoney += v;
          CoinText.SetText((GetCoin() + v).ToString());
     }
     public void CoinsGoToMoneyBox()
     {
          TotalMoneyText.SetText(_totalMoney.ToString());

          UpdateCoin();

          for (int i = 0; i < 10; i++)
          {

               GameObject obj = Instantiate(CoinPrefab, CoinParent.transform);

               float xPosition = Random.Range(-40, 10f);
               float yPosition = Random.Range(-10, 10f);

               obj.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);

               obj.transform.DOLocalMove(new Vector2(yPosition, xPosition), 0.1f);

               float minDuration = Random.Range(1f, 2f);

               obj.transform.
                   DOMove(MoneyImage.transform.position, minDuration).SetEase(Ease.InCubic).
                   OnComplete(() =>
                   {
                        VibrateController(7);
                        Destroy(obj);
                   });
          }

          // AdmonController.Instance.ShowReward();
     }

     public void SetLevelText()
     {
          int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
          levelName.SetText("Level " + (currentLevel).ToString());
     }
     public void MoneyController()
     {
          if (PlayerData.playerData.totalMoney == 0)
          {
               CoinText.DOBlendableColor(Color.red, 0.5f).onComplete = () =>
              CoinText.DOBlendableColor(Color.black, 0.5f);
          }

          CoinText.text = PlayerData.playerData.totalMoney.ToString();

     }


     #endregion

     #region Sound Vibrate
     public void SoundController(AudioClip clip)
     {
          if (PlayerPrefs.GetInt(Vibrate) == 1)
               VibrateController(15);
          if (PlayerPrefs.GetInt(Music) == 1)
          {
               // FindObjectOfType<CameraManager>().GunPlaySource(clip);
          }

     }

     private void VibrateController(int val) => Vibrator.Vibrate(val);



     public bool SettingsDataController(SettingsIcon icon)
     {
          if (icon.dataType == dataType.MUSIC)
               return PlayerPrefs.GetInt(Music) == 1 ? true : false;
          else
               return PlayerPrefs.GetInt(Vibrate) == 1 ? true : false;
     }
     #endregion

     #region Banner
     public void BannerController(bool isShow)
     {

          // if (isShow)
          //     AdmobManager.Instance.bannerView.Show();
          // else
          //     AdmobManager.Instance.bannerView.Hide();


     }

     public void RequestReward()
     {

     }

     #endregion

     #region WinMode Gift
     public Image WinModeImage;
     public TextMeshProUGUI WinModeText;
     public Image volumeImage;
     private int volume;

     public void Win()
     {
          volume = PlayerPrefs.GetInt("volume", 0);

          volumeImage.fillAmount = volume;

          float total = 2 / 10f;
          float volumex = volumeImage.fillAmount + total;

          volumeImage.fillAmount = Mathf.Lerp(volumeImage.fillAmount, volumex, Time.deltaTime * 2);

          if (volumeImage.fillAmount == 1)
          {
               // Give Gift
               PlayerData.playerData.currentWinGiftItem++;
               PlayerData.Instance.Save();

          }

          WinItem item = winSO.GetItem();


     }



     private IEnumerator BeautifulBarLerpRoutine()
     {

          while (true)
          {
               // if (Mathf.Approximately(image.fillAmount, _beatfiulBarValue))
               //      yield return false;

               // yield return new WaitForEndOfFrame();
          }
     }

     #endregion

}
