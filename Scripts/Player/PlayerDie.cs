using UnityEngine;
using UnityEngine.Events;

public class PlayerDie : MonoBehaviour
{
   
     public static UnityAction<bool> OnPlayerDie;

     public void Die() => OnPlayerDie?.Invoke(true);
}
