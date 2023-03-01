using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{

     #region  Variables
     private const string _isMovingTag = "moving";
     private const string _helperTag = "Helper";
     private const string _playerTag = "Player";
     private const string _doorTag = "Door";
     private AnimationController _anim;
     private Manager _manager;
     private Attack _attack;
     private Transform _target;
     private PlayerManager _playerManager;

     #endregion
     public float rotationSpeed;
     public float speed;
     private bool stop;
     public float timer;
     public Transform door;
     PlayerMovement _playerMovement;
     bool isPlaces;
     public NavMeshAgent agent;
     void Start()
     {
          agent.updateRotation = false;
          _anim = GetComponent<AnimationController>();
          _manager = GetComponent<Manager>();
          _attack = GetComponent<Attack>();
          _playerManager = _manager.playerManager;
          _playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
          if (_target == null)
               SearchDoor();
     }
     private void SearchDoor()
     {
          Door[] doors = GameObject.FindObjectsOfType<Door>();

          foreach (var item in doors)
          {
               _target = item.transform;
               door = item.transform;
               break;
          }
     }
     void Update()
     {
          if (GameManager.Instance.isPlay)
          {
               if (stop)
                    return;

               Searching();
          }
     }
     private void MoveTarget(Transform target)
     {
          float dis = Vector3.Distance(target.position, transform.position);
          Vector3 dir = target.position;
          dir.y = transform.position.y;

          if (dis > _playerManager.offset)
          {
               Rotation(target);
               Moving(dir);
               _anim.Moving(true);
          }
          else
          {
               _anim.Moving(false);
               _attack.GiveTarget(target);
               _attack.AttackDo();
          }
     }
     private void Moving(Vector3 target)
     {
          float spd = speed;
          agent.speed = spd;
          agent.SetDestination(target);
     }
     private void Rotation(Transform target)
     {
          Vector3 dir = target.position - transform.position;
          Quaternion lookRotation = Quaternion.LookRotation(dir);
          float rot = Time.deltaTime * rotationSpeed;
          Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, rot).eulerAngles;
          transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
     }
     private void Searching()
     {
          if (_target != null)
               MoveTarget(_target);


          if (door != null)
          {
               float dist = Vector3.Distance(door.position, transform.position);

               if (dist < 1.5f)
               {
                    door = null;
                    _target = null;
               }

               return;
          }

          float dis = 100000;

          Collider[] targets = Physics.OverlapSphere(transform.position,
                                                     _playerManager.attackRange);

          foreach (var item in targets)
          {

               float distance = Vector3.Distance(item.transform.position, transform.position);

               if ((item.CompareTag(_helperTag) ||
                  item.CompareTag(_playerTag) ||
                  item.CompareTag(_doorTag)) && distance < dis)
               {

                    if (item.GetComponent<Door>() != null && !isPlaces && _playerMovement.isPlaces)
                    {
                         Door door = item.GetComponent<Door>();
                         if (door.GetHealthDoor() != null)
                              _target = item.GetComponent<Door>().GetPivot();

                    }

                    else if (item.CompareTag(_helperTag) && _playerMovement.isPlaces)
                    {

                         if (item.GetComponent<Kule>() != null)
                              _target = item.GetComponent<Kule>().GetPivot();
                         else
                              _target = item.transform;

                    }

                    else if (item.CompareTag(_playerTag))
                         _target = item.transform;

                    dis = distance;



               }
          }
     }
     private void OnEnable()
     {
          DoorManager.OnDoorShared += DoorOpen;

     }
     private void OnDisable()
     {
          DoorManager.OnDoorShared -= DoorOpen;
     }
     private void DoorOpen(bool arg0)
     {
          if (arg0)
               SearchDoor();
     }
     public void DoorDie()
     {
          _target = null;
          door = null;
          _attack.GiveTarget(null);
     }
     private void OnDrawGizmosSelected()
     {
          Gizmos.color = Color.red;
          Gizmos.DrawWireSphere(transform.position, 26);
     }
     private void OnTriggerEnter(Collider other)
     {
          if (other.CompareTag("Places"))
               isPlaces = true;
     }
     private void OnTriggerExit(Collider other)
     {
          if (other.CompareTag("Places"))
               isPlaces = false;
     }
}
