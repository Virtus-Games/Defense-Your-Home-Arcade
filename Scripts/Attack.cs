using UnityEngine;
using System.Collections;

public enum PlayerType
{
     Player,
     Basic,
     Archer,
     Mage
}

public class Attack : MonoBehaviour
{
     private const string _arrow = "arrow";
     private const string _sword = "sword";
     private const string _mage = "mage";
     private const string _basic = "handle";
     private const string _balista = "balista";
     private AnimationController _anim;
     public Transform pivotBullet;
     public AnimationCurve curve;
     public Transform _target;
     private Manager _manager;
     private PlayerType _playerType;
     private float _timer;
     public float magicScale = 0.137f;
     private Rigidbody _rb;
     private ObjectPooling _objectPooling;
     PlayerMovement playerMovement;

     void Start()
     {

          _objectPooling = GetComponent<ObjectPooling>();
          _manager = GetComponent<Manager>();
          _anim = GetComponent<AnimationController>();

          _timer = _manager.playerManager.attackTimer;
          _playerType = _manager.playerManager.playerType;
          playerMovement = FindObjectOfType<PlayerMovement>();

     }




     public void AttackDo()
     {

          if (!playerMovement.IsMoving())
          {
               if (Timer())
               {

                    switch (_playerType)
                    {

                         case PlayerType.Player:
                              _anim.BoolAnim(_balista);
                              break;
                         case PlayerType.Basic:
                              _anim.Attack(_basic);
                              break;
                         case PlayerType.Archer:
                              _anim.Attack(_arrow);
                              break;
                         case PlayerType.Mage:
                              _anim.Attack(_mage);
                              break;
                         default:
                              break;

                    }

                    if (GetComponent<FeedBack>() != null)
                         GetComponent<FeedBack>().FeedBackDo();
               }


          }
          else
          {
               _timer = _manager.playerManager.attackTimer;
          }
     }

     bool Timer()
     {
          _timer -= Time.deltaTime;
          bool timer = _timer <= 0;

          if (timer)
          {
               _timer = _manager.playerManager.attackTimer;
               return true;
          }
          else
               return false;
     }
     public void CreateBulletAndForward() => Fire();

     void Fire()
     {
          if (_target)
          {

               float dmg = _manager.playerManager.attackDamage;

               float dieTime = _manager.playerManager.dieTime;

               GameObject obj = GetBullet();

               if (obj == null) return;

               Bullet bullet = obj.GetComponent<Bullet>();

               bullet.SetParentPooling(
                         _objectPooling,
                          _manager.playerManager.returnTimePool
               );

               bullet.SetTarget(
                    _manager.playerManager.playerType,
                    Transaction(),
                    dmg, dieTime);

          }
     }

     public void GiveTarget(Transform target) => _target = target;

     private Vector3 Transaction()
     {

          float sped = _manager.playerManager.attackSpeed;
          Vector3 dir = (_target.position - transform.position).normalized * sped;
          return dir;
     }

     public GameObject GetBullet()
     {

          GameObject bullet = _objectPooling.GetObjectPooling(0);

          if (bullet == null) return null;

          bullet.transform.SetParent(pivotBullet);
          bullet.transform.localPosition = Vector3.zero;
          bullet.transform.localRotation = pivotBullet.rotation;
          bullet.transform.SetParent(null);

          if (bullet.name.StartsWith("Arrow"))
               bullet.transform.rotation = Quaternion.Euler(Vector3.forward);

          return bullet;
     }
     public void MagicCarotins() => StartCoroutine(Magic());
     private IEnumerator Magic()
     {
          if (_target != null)
          {

               GameObject obj = GetBullet();

               yield return new WaitForSeconds(0.1f);

               // while (scale < magicScale)
               // {
               //      scale += Time.deltaTime * 2f;
               //      float curveScale = curve.Evaluate(scale);
               // }

               float dmg = _manager.playerManager.attackDamage;
               float dieTime = _manager.playerManager.dieTime;

               if (obj == null) yield break;

               if (_target == null)
                    Destroy(obj);
               else if (obj != null)
               {

                    Bullet bullet1 = obj.GetComponent<Bullet>();

                    bullet1.SetParentPooling(
                        _objectPooling,
                         _manager.playerManager.returnTimePool
                   );

                    bullet1.SetTarget(
                        _manager.playerManager.playerType,
                        Transaction(),
                        dmg,
                        dieTime);


               }
          }
     }
}
