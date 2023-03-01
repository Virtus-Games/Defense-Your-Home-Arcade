using UnityEngine;

public enum MoneyType
{
     EnemyDie,
     CoinsArea
}

public class Money : MonoBehaviour
{
     // public float speed;
     // public Transform target;
     // public CoinsAreaController controller;
     // public MoneyType type = MoneyType.CoinsArea;


     // public void SetData(Transform target, float speed, CoinsAreaController controller)
     // {
     //      this.target = target;
     //      this.speed = speed;
     //      this.controller = controller;
     // }

     // public void SetType(MoneyType type,Transform target)
     // {
     //      this.target = target;
     //      this.type = type;
     // }

     // void Update()
     // {
     //      if (target == null) return;

     //      if (controller == null)
     //           Destroy(gameObject);
               

     //      transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

     //      if (Vector3.Distance(transform.position, target.position) < 0.2f)
     //      {
     //           if (controller == null)
     //                Destroy(gameObject);
     //           switch (type)
     //           {
     //                case MoneyType.EnemyDie:
                         
     //                     break;
     //                case MoneyType.CoinsArea:
     //                     controller.RemoveMoney();
     //                     Destroy(gameObject);
     //                     break;
     //           }
     //      }
     // }
}
