using UnityEngine;
using System;
using System.Collections;

[System.Serializable]
public struct PlayerManager
{

     public PlayerType playerType;
     public GameObject Bullet;
     public float attackRange;
     public float returnTimePool;
     public float attackSpeed;
     public float attackDamage;
     public float health;
     public float dieTime;
     public float attackTimer;
     public float offset;

}

public class Manager : MonoBehaviour, HealthControl
{
     private const string _speedKey = "Speed";
     private const string _damageKey = "Damage";
     public PlayerManager playerManager;
     public Color dieColor;
     public SkinnedMeshRenderer meshRenderer;
     public Heath heath;
     public GameObject diePrefab;
     public ParticleSystem[] upgradeParticles;
     PlayerMovement _playerMovement;
     [SerializeField] private float returnTimePool = 3f;
     bool isCharge;

     private float _health;
     private void Start()
     {
          playerManager.returnTimePool = returnTimePool;
          _health = playerManager.health;
          if(playerManager.playerType == PlayerType.Player)
          {
             playerManager.attackSpeed += PlayerPrefs.GetInt(_speedKey, 5);
                playerManager.attackDamage += PlayerPrefs.GetInt(_damageKey, 5);
          }
     }

     public void Die()
     {
          // GameObject obj = Instantiate(diePrefab, transform.position, Quaternion.identity, transform);
          // obj.transform.SetParent(null);
          EnemyManager.Instance.EnemyStatus(gameObject);
          Destroy(gameObject);
     }

     private void Update()
     {

          if (playerManager.playerType == PlayerType.Player && _health < 1)
          {
               _health += Time.deltaTime * 2f;
               _health = Mathf.Clamp(_health, 0, playerManager.health);
          }
     }

     public void Health(float attackDamage)
     {

          _health -= attackDamage;

          _health = Mathf.Max(0, Mathf.Min(_health, playerManager.health));

          if (playerManager.playerType == PlayerType.Player)
          {
               heath.HealthBar(_health);
               StartCoroutine(Charge());
          }
          else if(_health <=0 && GetComponent<Helper>() != null){
               string type = playerManager.playerType.ToString();
               BuyHelperManager.SavePlayer(type, -1);
          }
          else if (_health <= 0)
          {
               _playerMovement = FindObjectOfType<PlayerMovement>();
               
               _playerMovement.TargetNull(gameObject, transform);
               Die();
          }

     }

     IEnumerator Charge()
     {
          isCharge = !isCharge;

          if (!isCharge)
          {
               isCharge = false;
               yield return new WaitForSeconds(5f);
               isCharge = true;
          }
     }

     public PlayerType GetPlayerType() => playerManager.playerType;

     public void Damage()
     {
          PlayParticles();
          playerManager.attackDamage += 2;
          PlayerPrefs.SetFloat(_damageKey, playerManager.attackDamage);

     }

     public void DieFeedBack(PlayerMovement movement) => _playerMovement = movement;

     public void Speed()
     {
          PlayParticles();
          playerManager.attackSpeed += 2f;
          PlayerPrefs.SetFloat(_speedKey, playerManager.attackSpeed);
     }

     void PlayParticles()
     {
          foreach (var item in upgradeParticles)
               item.Play();
     }

     public bool isPlacesPlayer = true;
     private void OnTriggerEnter(Collider other) {
          if(playerManager.playerType == PlayerType.Player && other.CompareTag("Places")){
               isPlacesPlayer = true;
          }
     }

     private void OnTriggerExit(Collider other) {
          if(playerManager.playerType == PlayerType.Player && other.CompareTag("Places")){
               isPlacesPlayer = false;
          }
     }
}



public interface HealthControl
{
     void Health(float attackDamage);
}