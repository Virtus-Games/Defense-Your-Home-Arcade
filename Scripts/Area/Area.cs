using UnityEngine;

public class Area : MonoBehaviour
{
     public Transform coinsPoint;
     public GameObject CloseDoor;
     public Door door;
     public Transform points;
     public Transform GetCoinsPoint() => coinsPoint;
     public Transform GetPointEnemyForThisArea() => points;
     public void Close() => CloseDoor.SetActive(false);
     public void DoorOpen()
     {
          if (door == null) return;
          door.CreateDoor();
     }
}
