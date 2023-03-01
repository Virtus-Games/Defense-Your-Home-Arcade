using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
     public Rigidbody rb;
     private PlayerType _playerType;
     private float _damage;
     private Coroutine _restartPool;

     private void OnEnable()
     {
          rb.isKinematic = true;
     }

     public void SetTarget(
          PlayerType type,
          Vector3 target,
          float attackDamage,
          float dieTime)
     {
          _playerType = type;
          GetComponent<Collider>().isTrigger = true;
          _damage = attackDamage;
          rb.isKinematic = false;
          rb.AddForce(target, ForceMode.Impulse);
     }

     private ObjectPooling _objectPooling;
     private bool isShoot = false;
     public void SetParentPooling(ObjectPooling obj, float time)
     {
          rb.velocity = Vector3.zero;
          isShoot = false;
          if (gameObject.activeSelf)
          {
               _objectPooling = obj;
               _restartPool = StartCoroutine(RestartPool(time));
               isShoot = true;
          }

     }
     IEnumerator RestartPool(float time)
     {
          yield return new WaitForSeconds(time);

          if (gameObject.activeSelf)
               _objectPooling.SetObjectPool(gameObject, 0);

          else StopCoroutine(_restartPool);
     }

     private void OnTriggerEnter(Collider other)
     {
          if (!isShoot) return;

          if (_playerType != PlayerType.Player)
               TagController(other, "Player");

          TagController(other, "Enemy");
          TagController(other, "Door");
          TagController(other, "Helper");
     }

     void TagController(Collider other, string val)
     {
          if (other.gameObject.tag == val)
          {
               other.gameObject.GetComponent<HealthControl>().Health(_damage);
               if (gameObject.activeSelf)
                    _objectPooling.SetObjectPool(gameObject, 0);
          }
     }
}