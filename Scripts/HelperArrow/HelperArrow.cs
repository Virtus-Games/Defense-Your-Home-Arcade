using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HelperArrow : MonoBehaviour
{
     [Header("Stack Manager")]
     public List<int> arrows = new List<int>();
     public Transform pivot;

     [Header("General Settings")]
     private AnimationController _anim;
     [SerializeField] private int rotationSpeed = 10;
     [SerializeField] private Transform _targetarrowPoint;
     [SerializeField] private float distance;
     private Kule kule;
     private bool isKule = false;
     [SerializeField] private float range = 150;
     [Range(0, 1)][SerializeField] private float radius;
     [Range(0, 1)][SerializeField] private float distanceFactory;
     private float waitTime;
     public float WaitTime
     {
          get { return waitTime; }
          set
          {
               waitTime = value;
               if (waitTime == 0)
                    waitTime = _waitTime;
          }
     }
     private float _waitTime;
     private NavMeshAgent _agent;
     private void Start()
     {
          _agent = GetComponent<NavMeshAgent>();
          _waitTime = WaitTime;
          _agent.updateRotation = false;
          _anim = GetComponent<AnimationController>();
          SearchArrowPoints();
     }
     private void SearchArrowPoints()
     {
          float maxDistance = 10000;
          GameObject[] arrowPoints = GameObject.FindGameObjectsWithTag("ArrowPoint");

          foreach (GameObject arrowPoint in arrowPoints)
          {
               if (Vector3.Distance(transform.position, arrowPoint.transform.position) < maxDistance)
               {
                    maxDistance = Vector3.Distance(transform.position, arrowPoint.transform.position);
                    _targetarrowPoint = arrowPoint.transform;
               }
          }
     }
     void FormatArrow()
     {
          if (arrows.Count == 0)
               return;

          for (int i = 0; i < arrows.Count; i++)
          {
               var x = distanceFactory * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
               var z = distanceFactory * Mathf.Sqrt(i) * Mathf.Sin(i * radius);
               var newPos = new Vector3(x, 0, z);
               
          }
     }
     private void SearchKule()
     {
          if (arrows.Count >= 9) // *
          {

               if (isKule)
                    return;

               Collider[] colliders = Physics.OverlapSphere(transform.position, range);

               foreach (Collider collider in colliders)
               {

                    if (collider.TryGetComponent<Kule>(out Kule kuleX))
                    {
                         float dis = Vector3.Distance(transform.position, collider.transform.position);

                         if (kuleX.StackCount >= 0 && kuleX.StackCount <= 15)
                         {
                              isKule = true;
                              this.kule = kuleX;
                              _targetarrowPoint = kule.GetPivot();
                              float x = _targetarrowPoint.position.x;
                              float z = _targetarrowPoint.position.z;
                              Vector3 dir = new Vector3((float)x, 0, (float)z);
                              _targetarrowPoint.position = dir;
                         }
                    }
               }

               FormatArrow();
          }
     }
     private void Update()
     {

          if (GameManager.Instance.isPlay)
          {

               SearchKule();

               if (_targetarrowPoint == null)
                    return;

               if (Vector3.Distance(_targetarrowPoint.position, transform.position) < distance)
               {
                    _anim.Carrying();
                    return;
               }

               if (isKule == true && kule == null)
               {
                    isKule = false;
                    _targetarrowPoint = null;
                    SearchKule();
                    _anim.BoolAnim("idle");

               }
               _anim.BoolAnim("moving");

               _agent.SetDestination(_targetarrowPoint.position);

               Vector3 rotLook = transform.position - _targetarrowPoint.position;

               rotLook.y = 0;

               rotLook.Normalize();

               transform.rotation = Quaternion.Slerp(transform.rotation,
                                                     Quaternion.LookRotation(-rotLook),
                                                     rotationSpeed * Time.deltaTime);
          }
     }
     public void GetArrow()
     {

          WaitTime -= Time.deltaTime;
          WaitTime = Mathf.Max(0, WaitTime);

          if (WaitTime > 0)
               return;

          if (arrows.Count == 0) // *
          {
               isKule = false;
               _targetarrowPoint = null;
               SearchArrowPoints();
               return;

          }

          // *
         
          kule.SetStackController(); // * IMPORTANT
          arrows.RemoveAt(0); // *
          FormatArrow();
          return;
     }
     public void Add() => arrows.Add(1);

}

