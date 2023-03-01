using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
     [Header("Arrow Manager")]
     public List<GameObject> arrows = new List<GameObject>();
     public List<Transform> points = new List<Transform>();
     public Transform exitPoint;

     [Header("Stack Manager")]
     [SerializeField] private float waitSeconds;
     [SerializeField] private int maxCount;
     int index = 0;

     [Header("General Settings")]
     [SerializeField] private float speed;

     [SerializeField] private bool create;
     public ObjectPooling pooling;

     private void Start() => StartCoroutine(CreateAndTranslateToPoint());

     IEnumerator CreateAndTranslateToPoint()
     {
          Vector3 pos = exitPoint.position;
          while (true)
          {

               if (arrows.Count <= maxCount)
               {
                    GameObject obj = pooling.GetObjectPooling(0);

                    if (obj == null) yield return null;

                    obj.transform.rotation = Quaternion.Euler(90, 0, 0);

                    Debug.Log("Create " + obj.name);


                    arrows.Add(obj);

                    StartCoroutine(MoveToPos(obj.transform, points[index].position));

                    obj.transform.rotation = Quaternion.Euler(90, 0, 0);

               }
               yield return new WaitForSeconds(waitSeconds);
          }
     }

     Transform target;
     IEnumerator MoveToPos(Transform arrowObj, Vector3 point)
     {
          if (index >= points.Count)
               index = 0;

          target = arrowObj;
          if (target == null)
          {
               Debug.Log("Target is null");
               yield break;
          }

          Vector3 POS = point;
          while (Vector3.Distance(target.position, POS) > 0.1f)
          {
               target.position = Vector3.MoveTowards(target.position, POS, speed * Time.deltaTime);
               yield return null;
          }

          target.SetParent(points[index]);

          index++;

          if (index >= points.Count)
               index = 0;
          target = null;
     }

     IEnumerator MoveToPivot(Vector3 point, HelperArrow helperArrow)
     {

          Transform target = arrows[0].transform;
          Vector3 Pos = point;



          if (arrows.Count > 0)
          {
               helperArrow.Add();
               pooling.SetObjectPool(target.gameObject, 0);
               arrows.Remove(arrows[0]);
          }

          yield return null;

     }


     private void OnTriggerStay(Collider other)
     {
          if (other.CompareTag("HelperArrow"))
          {

               HelperArrow helperArrow = other.GetComponent<HelperArrow>();

               if (helperArrow.arrows.Count >= 9) return;

               if (arrows.Count < 9) return;

               if (arrows.Count > 0) StartCoroutine(MoveToPivot(helperArrow.pivot.position, helperArrow));

          }
     }
}