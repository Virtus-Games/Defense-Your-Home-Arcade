using UnityEngine;

public class AiMove : MonoBehaviour
{
     private const string _isMovingTag = "moving";
     [SerializeField] private float chaseSpeed = 5;
     public float rotateSpeed = 50;
     private Manager _manager;
     public Attack _attack;
     private Rigidbody _rb;
     float distance = 100000f;
     private Animator _anim;
     public Transform Target;
     public bool stop;

     void Start() => Started();
     private void Started()
     {
          _rb = GetComponent<Rigidbody>();
          _anim = gameObject.GetComponent<Animator>();
          _attack = GetComponent<Attack>();
          _manager = GetComponent<Manager>();
          SearchDoorNear();
     }
     private void Update()
     {
          if (GameManager.Instance.isPlay)
          {
               if (stop) return;

               EnemyAi();
          }

     }
     private void EnemyAi() => NearDoor();
     private void NearDoor()
     {
          SearchingTarget();

          if (Target != null)
               MoveTarget(Target.transform);

     }
     private void SearchingTarget()
     {
          Collider[] colliders = Physics.OverlapSphere(transform.position,
               _manager.playerManager.attackRange);

          if (Target != null)
               return;

          foreach (var item in colliders)
          {
               if (item.CompareTag("Helper") ||
                   item.CompareTag("Player") ||
                   item.CompareTag("Door"))
               {
                    if (item.CompareTag("Door"))
                         Target = item.transform;
                    else
                         SearchPlayer(item);

               }
          }
     }

     public void GiveTarget(Transform target)
     {
          if (target.GetComponent<IPlaces>() != null)
               if (target.GetComponent<IPlaces>().IsPlaces == true)
               {
                    Target = DoorManager.Instance.DistanceMe(transform);
               }

          if (Target == null)
               Target = target;

     }
     private void SearchPlayer(Collider item)
     {
          distance = 1000f;
          float dis = Vector3.Distance(transform.position, item.transform.position);

          if (item.CompareTag("Helper"))
          {
               if (dis < distance)
               {

                    distance = dis;
                    if (item.GetComponent<Kule>() != null)
                        GiveTarget(item.transform);
                    else
                         Target = item.transform;
               }
          }
          else if (item.CompareTag("Player"))
          {
               Manager manager = item.GetComponent<Manager>();
               if (manager.isPlacesPlayer)
               {
                    bool status = SearchDoorNear();
                    if (!status) Target = item.transform;
               }
               else
                    Target = item.transform;
          }


     }
     private bool SearchDoorNear()
     {

          float minDistance = 1000;
          Collider[] colliders = Physics.OverlapSphere(transform.position, 120);
          foreach (var item in colliders)
          {
               float dis = Vector3.Distance(transform.position, item.transform.position);


               if (item.GetComponent<TargetType>() != null && dis < minDistance)
               {
                    Door door = item.GetComponent<Door>();
                    if (door.GetHealthDoor() != null)
                    {
                         minDistance = dis;
                         Target = item.GetComponent<Door>().GetPivot();
                         return true;
                    }
                    else
                    {
                         return false;
                    }
               }
          }

          return false;
     }
     void MoveTarget(Transform target)
     {
          if (target != null)
          {

               _anim.SetBool(_isMovingTag, true);
               Rotate(target);


               float dis = Vector3.Distance(target.position, transform.position);



               if (dis > _manager.playerManager.offset)
               {
                    Vector3 dir = Target.position;
                    dir.y = transform.position.y;

                    transform.position = Vector3.MoveTowards(transform.position,
                                                             dir,
                                                             chaseSpeed *
                                                             Time.deltaTime);

               }
               else
               {
                    if (target.GetComponentInParent<TargetType>() != null)
                    {
                         SearchingTarget();
                         target = null;
                    }
                    else
                    {
                         _anim.SetBool(_isMovingTag, false);
                         _attack.GiveTarget(target);
                         _attack.AttackDo();
                         //agent.SetDestination(transform.position);
                    }
               }
          }
          else
          {
               _anim.SetBool(_isMovingTag, false);
               // agent.SetDestination(transform.position);
          }

     }
     public void Rotate(Transform target)
     {
          Vector3 dir = target.position - transform.position;
          Quaternion lookRotation = Quaternion.LookRotation(dir);
          Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
          transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
     }
     private void OnEnable()
     {
          DoorManager.OnDoorShared += DoorOpen;
          global::PlayerDie.OnPlayerDie += PlayerDie;
     }
     private void PlayerDie(bool arg0)
     {
          if (arg0)
          {
               Target = null;
               _attack.GiveTarget(null);
          }
     }
     public void DoorDie()
     {
          Target = null;
          _attack.GiveTarget(null);
     }
     private void OnDisable()
     {
          DoorManager.OnDoorShared -= DoorOpen;
          global::PlayerDie.OnPlayerDie -= PlayerDie;
     }
     private void DoorOpen(bool arg0)
     {
          if (arg0)
               FindsDistanceDoor(50);
     }
     private void FindsDistanceDoor(float radius)
     {
          Transform disTrans = DoorManager.Instance.DistanceMe(transform);
          if (disTrans != null) Target = disTrans;
     }
     private void OnDrawGizmosSelected()
     {
          Gizmos.color = Color.red;
          Gizmos.DrawWireSphere(transform.position, 26);
     }

     private bool isPlaces;
     private void OnTriggerEnter(Collider other) {
          if (other.CompareTag("Places"))
          {
               isPlaces = true;
          }
     }
     private void OnTriggerExit(Collider other) {
          if (other.CompareTag("Places"))
               isPlaces = false;
     }
}

