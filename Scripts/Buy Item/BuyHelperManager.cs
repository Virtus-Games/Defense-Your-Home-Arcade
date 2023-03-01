using UnityEngine;

public class BuyHelperManager : Singleton<BuyHelperManager>
{
     private const string _TagArcher = "Archer";
     private const string _TagMage = "Mage";
     public GameObject BuyHelperPanel;
     public GameObject Archer;
     public GameObject Mage;
     public Transform pointsHelper;
     public Transform[] pointsTowers;
     public int indexTowers;
     GameObject _helper;


     public static void SavePlayer(string tag, int plusOrNegative)
     {
          int val = PlayerPrefs.GetInt(tag) + plusOrNegative;

          PlayerPrefs.SetInt(tag, val);
     }

     public void BuyArcher()
     {
          if (PlayerData.playerData.totalMoney < 100) return;

          PlayerData.Instance.UpdateMoney(-100);

          SavePlayer(_TagArcher, 1);
          CreateHelper(Archer);
     }

     public void BuyMage()
     {
          if (PlayerData.playerData.totalMoney < 200)
               return;

          PlayerData.Instance.UpdateMoney(-200);

          SavePlayer(_TagMage, 1);

          CreateHelper(Mage);
     }

     private void CreateHelper(GameObject helper)
     {
          Vector3 pos = GetRandomPos();
          GameObject obj = Instantiate(helper, pos, Quaternion.identity, pointsHelper);
          obj.transform.SetParent(null);
          obj.transform.localScale = Vector3.one;
          obj.GetComponent<Helper>().SetPoint(pos);
     }

     private Vector3 GetRandomPos()
     {
          float x = pointsHelper.position.x;
          float z = pointsHelper.position.z ;

          float randomX = Random.Range(x - 3, x + 3);
          float randomZ = Random.Range(z - 3, z + 3);

          Vector3 pos = new Vector3(randomX, pointsHelper.position.y, randomZ);

          return pos;
     }

     private void Start() => StartCreating();
     private void StartCreating()
     {

          for (int i = 0; i < PlayerPrefs.GetInt(_TagArcher); i++)
               CreateHelper(Archer);

          for (int i = 0; i < PlayerPrefs.GetInt(_TagMage); i++)
               CreateHelper(Mage);
     }

     // Button On Clicking 1 
     public void BuyHelper(GameObject helper) => _helper = helper;
     // Button On Cliking 2 
     public void BuyTower(GameObject prefab)
     {
          if (indexTowers >= pointsTowers.Length) return;

          Vector3 pos = GetRandomPos();
          GameObject obj = Instantiate(prefab, pos, Quaternion.identity);
          obj.GetComponentInChildren<Kule>().CreateCharacter(_helper);
          obj.transform.SetParent(pointsTowers[indexTowers]);
          indexTowers++;
     }
     public void TowerCreate(GameObject tower, Transform pos)
     {
          GameObject obj = Instantiate(tower, pos.position, Quaternion.identity);
          obj.GetComponentInChildren<Kule>().CreateCharacter(Archer);
          obj.transform.SetParent(pos);
          obj.SetActive(true);
     }
}
