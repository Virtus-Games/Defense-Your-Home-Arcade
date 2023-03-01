using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;


public class DoorManager : Singleton<DoorManager>
{
     public static UnityAction<bool> OnDoorCreat;
     public static UnityAction<bool> OnDoorShared;

     public static List<Door> doors = new List<Door>();

     public Transform DistanceMe(Transform me)
     {
          Transform target = null;
          float distance = 100000f;
          if (doors.Count == 0) return null;
          foreach (Door item in doors)
          {
               if (item != null)
               {
                    float dist = Vector3.Distance(me.position, item.transform.position);
                    if (dist < distance)
                    {
                         distance = dist;
                         item.CenterPivot = target;
                    }
               }
          }
          return target;
     }
     
     private void Start() => CarCreate(true);

     public void CarCreate(bool val) => OnDoorCreat?.Invoke(val);
     public void DoorShared(bool val) => OnDoorShared?.Invoke(val);

     private void OnEnable() => GameManager.OnGameStateChanged += OnGameStateChanged;

     private void OnDisable() => GameManager.OnGameStateChanged -= OnGameStateChanged;

     private void OnGameStateChanged(GAMESTATE obj)
     {
          if (obj == GAMESTATE.START)
          {
               //  CarCreate(true);
          }

          if (obj == GAMESTATE.PLAY)
               CarCreate(false);
     }
}
