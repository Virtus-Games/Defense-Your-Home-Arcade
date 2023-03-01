using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
     public static event UnityAction<bool> OnLevelLoaded;
     private const string _level = "Level ";
     private Scene _lastLoadedScene;
     public GameObject LoadingBar;
     private int currentLevel;

     private void Awake()
     {
          //AdmobManager.Instance.InitiliazedAds();
          LevelLoad();
     }

     private void LevelLoad()
     {
          currentLevel = PlayerData.playerData.currentLevel;
          LoadingBar.SetActive(true);
          SceneLoader(currentLevel.ToString());
     }

     private void OnEnable() => PlayerData.onDataChanged += UptadeData;
     private void OnDisable() => PlayerData.onDataChanged -= UptadeData;

     private void UptadeData(PlayerDataContainer arg0)
     {

     }

     public void SetCurrentLevel()
     {
          currentLevel++;

          if (currentLevel >= SceneManager.sceneCountInBuildSettings)
               currentLevel = 1;

          PlayerData.playerData.currentLevel = currentLevel;
          PlayerData.Instance.Save();
          SceneLoader(currentLevel.ToString());
     }


     public void SceneLoader(string name) => ChangeScene(name);

     void ChangeScene(string sceneName)
     {
          LoadingBar.SetActive(true);
          StartCoroutine(SceneController(_level + sceneName));
     }

     IEnumerator SceneController(string sceneName)
     {
          OnLevelLoaded?.Invoke(false);

          if (_lastLoadedScene.IsValid())
          {
               SceneManager.UnloadSceneAsync(_lastLoadedScene);
               bool isUnloadScene = false;
               while (!isUnloadScene)
               {
                    isUnloadScene = !_lastLoadedScene.IsValid();
                    yield return new WaitForEndOfFrame();

               }
          }

          SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

          bool isSceneLoaded = false;

          while (!isSceneLoaded)
          {
               _lastLoadedScene = SceneManager.GetSceneByName(sceneName);
               isSceneLoaded = _lastLoadedScene != null && _lastLoadedScene.isLoaded;

               yield return new WaitForEndOfFrame();
          }

          OnLevelLoaded?.Invoke(true);
          LoadingBar.SetActive(false);

     }
     public void NextLevel()
     {
          SetCurrentLevel();
     }
     public void RestartLevel()
     {
          LevelLoad();
     }
}


[CustomEditor(typeof(LevelManager))]
public class LevelManagerCustom : Editor
{
     public override void OnInspectorGUI()
     {
          base.OnInspectorGUI();

          if (GUILayout.Button("Next Level"))
          {
               LevelManager.Instance.NextLevel();
          }


     }
}