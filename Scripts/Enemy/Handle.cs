using UnityEngine;

public class Handle : MonoBehaviour
{
     private Manager _manager;
     private float damage;
     private void Start()
     {
          _manager = GetComponent<Manager>();
          damage = _manager.playerManager.attackDamage;
     }
     private void OnTriggerStay(Collider other)
     {

          if (other.gameObject.tag == "Player")
               other.gameObject.GetComponent<Manager>().Health(damage);
          if (other.gameObject.tag == "Door")
               other.gameObject.GetComponent<HealthDoor>().Health(damage);

     }
}
