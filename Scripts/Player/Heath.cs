using UnityEngine;
using UnityEngine.UI;

public class Heath : MonoBehaviour
{
     public Image healthBar;
     public Vector3 offset;
     public Transform target;

     public PlayerDie playerDie;
     public void HealthBar(float val)
     {
          healthBar.fillAmount = val / 100;

          if(healthBar.fillAmount <= 0){
               playerDie.Die();
               GameManager.Instance.UpdateGameState(GAMESTATE.DEFEAT);
               Destroy(transform.parent.gameObject);
          }

     }
     private void FixedUpdate()
     {

          transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.fixedDeltaTime * 100);
          transform.LookAt(Camera.main.transform);
     }
}
