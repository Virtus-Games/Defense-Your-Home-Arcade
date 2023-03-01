using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyManager : Singleton<EnemyManager>
{
     [SerializeField] private LevelsEnemy[] levelsEnemies;
     List<GameObject> _enemies = new List<GameObject>();
     [SerializeField] private List<Transform> _enemyPoints = new List<Transform>();
     [SerializeField] private int timerEnemySpawn = 5;
     int currentSpams;
     private bool isStarting;

     private void OnEnable() => GameManager.OnGameStateChanged += OnGameStateChanged;

     private void OnGameStateChanged(GAMESTATE obj)
     {
          if (obj == GAMESTATE.START)
          {

          }
          if (obj == GAMESTATE.PLAY)
               CreateEnemy(PlayerData.playerData.currentEnemyLevel, false);
     }

     public void Add(Transform pointArea) => _enemyPoints.Add(pointArea);

     public void CreateEnemy(int currentLevel, bool isStarting)
     {
          currentSpams = 0;
          _enemies.Clear();

          for (int i = 0; i < levelsEnemies[currentLevel].enemies.Length; i++)
          {
               int rand = Random.Range(0, _enemyPoints.Count);
               float xPos = _enemyPoints[rand].transform.position.x;
               float zPos = _enemyPoints[rand].transform.position.z;

               while (levelsEnemies[currentLevel].enemies[i].count > 0)
               {
                    float xRange = UnityEngine.Random.Range(xPos + 2, xPos - 2);
                    float zRange = UnityEngine.Random.Range(zPos + 2, zPos - 2);

                    Vector3 dir = new Vector3(xRange, _enemyPoints[0].position.y, zRange);
                    GameObject enemy = levelsEnemies[currentLevel].enemies[i].enemy;
                    GameObject obj = Instantiate(enemy, dir, Quaternion.identity, transform);

                    obj.transform.SetParent(null);
                    currentSpams++;
                    levelsEnemies[currentLevel].enemies[i].count--;
                    _enemies.Add(obj);
               }
          }
     }

     public void EnemyStatus(GameObject enemy)
     {

          _enemies.Remove(enemy);

          if (_enemies.Count == 0)
          {
               int levelCount = LevelController();
               PlayerData.playerData.currentEnemyLevel = levelCount;
               PlayerData.Instance.Save();
               StartCoroutine(SpawnEnemyTime(timerEnemySpawn, levelCount));
          }
     }

     private int LevelController()
     {
          PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel", 1) + 1);
          UIManager.Instance.SetLevelText();
          int levelCount = PlayerData.playerData.currentEnemyLevel;

          if (levelCount < levelsEnemies.Length - 1)
               levelCount += 1;
          else
               levelCount = levelsEnemies.Length - 1;

          return levelCount;
     }

     IEnumerator SpawnEnemyTime(float time, int count)
     {
          yield return new WaitForSeconds(time);
          CreateEnemy(count, true);
     }

     private void OnDestroy() => GameManager.OnGameStateChanged -= OnGameStateChanged;
}



[System.Serializable]
public struct LevelsEnemy
{
     public Enemy[] enemies;
}

[System.Serializable]
public struct Enemy
{
     public int count;
     public GameObject enemy;
}



[CustomEditor(typeof(EnemyManager))]
public class EnemyManagerEditor : Editor
{
     public override void OnInspectorGUI()
     {
          base.OnInspectorGUI();

          EnemyManager enemyManager = (EnemyManager)target;
          if (GUILayout.Button("Spawn Enemies"))
          {
               enemyManager.CreateEnemy(0, false);
          }

     }
}