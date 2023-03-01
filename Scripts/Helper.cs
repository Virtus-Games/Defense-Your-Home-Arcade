using UnityEngine;

public interface FeedBack
{
     void FeedBackDo();
}

public class Helper : MonoBehaviour, FeedBack,IPlaces
{
     private string _isMovingTag = "moving";
     public float rotateSpeed = 100f;
     public HelperType helperType;
     private Transform _target;
     private Manager _manager;
     private Attack _attack;
     private Animator _anim;
     private bool isTower;
     private Transform _pivot;
     [SerializeField] private float speed = 15;
     public int attackCount;
     private float _attackRange;
     private Kule _kule;
     public int AttackCount
     {
          get { return attackCount; }
          set
          {
               attackCount = value;
               attackCount = Mathf.Max(attackCount, 0);
          }
     }

     public bool IsPlaces { get => isPlaces; set => isPlaces = value; }

     // okçu tower da olduğıu için düşman karekterleri gelemiyor. 
     // Düşman karekterlerini gelmesi için pivot pos noktası vererek oraya yönlendiriyoruz
     public void HelperAtTower(Transform kulePivotPos)
     {
          helperType = HelperType.Tower;

          GetComponent<CapsuleCollider>().center = new Vector3(0, 2, 0);

          _anim.SetBool(_isMovingTag, false);

          transform.position = Vector3.down * 100 * Time.deltaTime;
          
          if(helperType == HelperType.Tower) isPlaces = true;
     }

     public void SetKule(Kule kule) => _kule = kule;
     public void SetPoint(Vector3 point) => transform.position = point;
     private void OnEnable()
     {
          _manager = GetComponent<Manager>();
          _attack = GetComponent<Attack>();
          _anim = GetComponent<Animator>();
     }
     private void Start()
     {
          _attackRange = _manager.playerManager.attackRange;
     }

     private void Update() => Attack();
     private void Attack()
     {

          if (attackCount == 0 && helperType == HelperType.Tower)
               return;

          if (_target == null)
          {
               _anim.SetBool(_isMovingTag, false);
               Collider[] hitColliders = Physics.OverlapSphere(transform.position, _attackRange);

               foreach (var hitCollider in hitColliders)
               {
                    if (hitCollider.gameObject.CompareTag("Enemy"))
                    {
                         _target = hitCollider.transform;
                         break;
                    }
               }
          }
          else
          {
               float dis = Vector3.Distance(transform.position,
                                            _target.position);
               if (dis < _manager.playerManager.attackRange)
               {
                    if (_target == null)
                         return;

                    if (attackCount == 0 && helperType == HelperType.Tower)
                         return;

                    _anim.SetBool(_isMovingTag, false);
                    _attack.GiveTarget(_target);
                    _attack.AttackDo();


                    RotateToTarget(_target);

               }
               else
               {

                    if (helperType == HelperType.Tower)
                         return;

                    _anim.SetBool(_isMovingTag, true);
                    transform.position = Vector3.MoveTowards(
                         transform.position,
                         _target.position,
                         speed * Time.deltaTime);
               }
          }
     }

     public void FeedBackDo()
     {
          if (helperType == HelperType.Tower)
          {
               attackCount--;
               attackCount = Mathf.Max(0, attackCount);
               _kule.SetStackAtHelper();
          }
     }


     public void TargetNull()
     {
          _attack.GiveTarget(null);
          _target = null;
     }

     private void RotateToTarget(Transform target)
     {
          Vector3 direction = (target.position - transform.position).normalized;

          Vector3 forward = new Vector3(direction.x, 0, direction.z);
          Quaternion lookRotation = Quaternion.LookRotation(forward);

          float t = Time.deltaTime * rotateSpeed;
          transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, t);
     }

     public void GetPivotAtParent(Transform pivot) => _pivot = pivot;

     public Transform GetPivotPos() => _pivot;

     private bool isPlaces = false;
     private void OnTriggerEnter(Collider other) {
         if (other.gameObject.CompareTag("Places"))
             isPlaces = true;
     }

     private void OnTriggerExit(Collider other) {
         if (other.gameObject.CompareTag("Places"))
             isPlaces = false;
     }


}
