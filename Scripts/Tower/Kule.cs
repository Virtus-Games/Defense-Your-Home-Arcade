using UnityEngine;
using TMPro;

public enum HelperType
{
     Tower,
     Solider
}

public class Kule : MonoBehaviour
{
     [SerializeField] private TextMeshProUGUI StackCountText;
     public Towers towers;
     public Transform pivotCharacter;
     public Transform pivotCharacterBackArrow;
     public GameObject Canvas;
     private GameObject _character;
     public Transform pivotPos;
     bool isCreated = false;
     [SerializeField] private float damage;
     public HelperType _helperType;
     private TowerPoint _towerPoint;
     private int stackCount;
     private Helper _helper;

     [Range(130, 150)]
     public float attackRange = 100;

     public int StackCount
     {
          get { return stackCount; }
          set { stackCount = value; }
     }

     public void SetStackController()
     {
          StackCount++;
          StackCountText.text = StackCount.ToString();
          _helper.AttackCount = stackCount;

     }

     public void SetStackAtHelper()
     {
          StackCount = _helper.AttackCount;
          StackCountText.text = StackCount.ToString();
     }

     private void Start()
     {
          Transform parent = GetComponentInParent<Transform>();
          _towerPoint = parent.GetComponentInParent<TowerPointManager>().GetTowerPoint();
     }
     public void CreateCharacter(GameObject helper)
     {
          if (!isCreated)
          {
               _character = Instantiate(helper, pivotCharacter.position, Quaternion.identity);
               _helper = _character.GetComponent<Helper>();

               _helper.HelperAtTower(pivotPos);
               _helper.SetKule(this);
               _helper.GetPivotAtParent(towers.GetPivotPos());
               _character.transform.SetParent(p: pivotCharacter);
               _character.transform.localPosition = Vector3.zero;
               isCreated = true;
          }
     }

     internal void DestroyParent() => Destroy(gameObject.transform.parent);

     private void OnTriggerEnter(Collider other)
     {
          if (other.tag == "Enemy")
               GetComponent<HealthDoor>().Health(damage);
     }
     private void OnTriggerStay(Collider other)
     {
          if (other.CompareTag("HelperArrow"))
          {
               HelperArrow helperArrow = other.GetComponent<HelperArrow>();
               helperArrow.GetArrow();


          }
     }
     public GameObject GetCharacter()
     {
          if (isCreated)
               return _character;
          else
               return null;
     }
     private void OnEnable() => GameManager.OnGameStateChanged += OnGameStateChanged;
     private void OnDestroy() => GameManager.OnGameStateChanged -= OnGameStateChanged;
     private void OnGameStateChanged(GAMESTATE obj)
     {
          if (obj == GAMESTATE.START)
          {

          }

          if (obj == GAMESTATE.PLAY)
          {

          }
     }
     public void DestroyTower()
     {
          _towerPoint.gameObject.SetActive(true);
          _towerPoint.SetData(0);
          towers.DestroyGameObject();
     }
     private void CanvasShow(bool val) => Canvas.SetActive(val);
     public Vector3 GetPivotCharacterPosition() => pivotCharacter.localPosition;
     public Transform GetPivot() => pivotPos;
}