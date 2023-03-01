using System;
using UnityEngine;

[System.Serializable]
public enum HealthType
{
     DOOR,
     TOWER
}

public class HealthDoor : MonoBehaviour, HealthControl
{
     public float _health = 100;
     public HealthType healthType;

     private void Start()
     {
          if (healthType == HealthType.DOOR)
               DoorManager.Instance.DoorShared(true);


     }
     public void Health(float attackDamage)
     {

          _health -= attackDamage;
          _health = Mathf.Max(0, Mathf.Min(_health, 100));

          // Play Music

          if (_health <= 0)
          {
               if (healthType == HealthType.DOOR)
               {
                    AiController[] aiMoves = FindObjectsOfType<AiController>();
                    foreach (AiController item in aiMoves)
                         item.DoorDie();
                    _door.ShowSettings(true);
                    gameObject.SetActive(false);
                    DoorManager.doors.Remove(_door);
               }
               else if (healthType == HealthType.TOWER)
                    GetComponent<Kule>().DestroyTower();
          }
     }

     Door _door;
     public void Door(Door door) => _door = door;


}
