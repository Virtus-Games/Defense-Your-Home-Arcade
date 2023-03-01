using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public enum GAMESTATE
{
     START,
     PLAY,
     VICTORY,
     MARKET,
     SETTINGS,
     DEFEAT,
     TEASER
}

[ExecuteInEditMode]
public class GameManager : Singleton<GameManager>
{

     #region  Value Settings

     [HideInInspector]
     public static event Action<GAMESTATE> OnGameStateChanged;
     [HideInInspector]
     public GAMESTATE gameState;
     private GameObject _player;

     public bool isPlay { get; private set; }
     public static event Action<bool> OnPlayerHaveInGame;

     #endregion

    //  private void OnValidate() {
    //         OnValidate();
    //         if(gameState == GAMESTATE.PLAY)
    //           isPlay = true;
                 
    //  }
     public void UpdateGameState(GAMESTATE state)
     {
          gameState = state;

          switch (gameState)
          {
               case GAMESTATE.START:
                    HandleStartAction();
                    break;
               case GAMESTATE.PLAY:
                    HandlePlayAction();
                    break;
               case GAMESTATE.VICTORY:
                    HandleVictoryAction();
                    break;
               case GAMESTATE.MARKET:
                    HandleMarketAction();

                    break;
               case GAMESTATE.DEFEAT:
                    HandleDefeatAction();
                    break;
               case GAMESTATE.SETTINGS:
                    HandleSettingsAction();
                    break;
          }

          OnGameStateChanged?.Invoke(state);
     }
     private void Awake()
     {
          UpdateGameState(GAMESTATE.START);
     }

     #region Update States
     private void HandleSettingsAction()
     {

     }

     private void HandleStartAction()
     {

     }

     private void HandlePlayAction()
     {

          isPlay = true;
     }

     private void HandleDefeatAction()
     {
          isPlay = false;
     }

     private void HandleVictoryAction()
     {
          isPlay = false;
     }

     private void HandleMarketAction()
     {
          isPlay = false;
     }

     #endregion

     #region  Player Status Manager

     public void UpdatePlayerStatus(bool isHave) => OnPlayerHaveInGame?.Invoke(isHave);


     #endregion


}



[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
     public GAMESTATE state;

     public override void OnInspectorGUI()
     {
          base.OnInspectorGUI();
          GameManager gameManager = target as GameManager;

          state = (GAMESTATE)EditorGUILayout.EnumPopup("Game State", state);
          if (GUILayout.Button("Update Game State"))
          {
               gameManager.UpdateGameState(state);
          }
     }

}