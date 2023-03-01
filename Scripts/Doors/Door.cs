using System;
using UnityEngine;

public class Door : MonoBehaviour, TargetType
{
     public Transform Pivot;
     public GameObject DoorPrefab;
     private BoxCollider _boxCollider;
     public GameObject settings;
     private float _health;
     public GameObject repairCanvas;
     GameObject door;

     private HealthDoor _healthDoor;
     public void CreateDoor()
     {
          if (door != null)
               Destroy(door);

          door = Instantiate(DoorPrefab, Pivot.position, Quaternion.identity);
          _healthDoor = door.GetComponent<HealthDoor>();
          _healthDoor.Door(this);
          door.transform.parent = Pivot;
          door.transform.localScale = Vector3.one;
          door.transform.localRotation = Pivot.localRotation;
          ShowSettings(false);
     }
     public void DestroyDoor()
     {
          _healthDoor = null;
          Destroy(Pivot.GetChild(0).gameObject);
     }

     public HealthDoor GetHealthDoor()
     {
          if (_healthDoor == null)
               return _healthDoor;
          else
               return null;
     }

     public void ReloandDoor()
     {
          DestroyDoor();
          CreateDoor();
     }
     public void ShowSettings(bool val)
     {
          settings.SetActive(val);
          _boxCollider.enabled = val;
     }
     private void OnTriggerEnter(Collider other)
     {
          if (other.tag == "Player")
          {
               if (door != null)
                    Doors();
          }
     }

     private void Doors()
     {
          HealthDoor[] col = FindObjectsOfType<HealthDoor>();

          for (int i = 0; i < col.Length; i++)
               Destroy(col[i].gameObject);

          DoorManager.Instance.CarCreate(true);
          DoorManager.doors.Add(this);
     }

     private void OnEnable()
     {
          _boxCollider = GetComponent<BoxCollider>();
          DoorManager.OnDoorCreat += OnDoorOpen;
     }

     private void OnDisable()
     {
          DoorManager.OnDoorCreat -= OnDoorOpen;
     }
     private void OnDoorOpen(bool arg0)
     {
          if (arg0)
          {
               ShowSettings(true);
               CreateDoor();
          }
          else
               ShowSettings(false);
     }
     public Transform CenterPivot;

     public bool targetType = true;
     public bool IsTarget { get => targetType; }

     internal Transform GetPivot() => CenterPivot;
}


public interface TargetType
{
     public bool IsTarget { get; }
}


