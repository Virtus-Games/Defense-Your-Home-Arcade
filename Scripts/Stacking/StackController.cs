using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class StackController : Singleton<StackController>
{
     private const float jumpDuration = .5f;
     [Range(1f, 1.5f)]
     public float duration = 0.5f;
     public float waitingTime = 0.2f;
     public float yDistance = 0.25f;
     public Transform HolderParent;
     bool onComplete = false;
     bool isDragging = false;
     Vector3 dropPos;
     Coroutine dragging;

     Stack<GameObject> collected = new Stack<GameObject>();
     public GameObject moneyPrefab;

     public void CreateStack(Transform pos)
     {
          GameObject obj = Instantiate(moneyPrefab, pos.position, Quaternion.identity);
          CreatingStack(obj);
     }

     private void CreatingStack(GameObject obj)
     {
          ItemCollectible item = null;
          obj.TryGetComponent(out item);
          if (obj.CompareTag("Money") && item != null && !item.collectible)
          {
               obj.transform.SetParent(HolderParent);
               collected.Push(obj.gameObject);
               onComplete = false;
               dragging = StartCoroutine(ToPos(obj.transform, item));
          }
     }

     private UnlockScr _unlockScr;

     private void OnTriggerEnter(Collider other)
     {

          if (other.CompareTag("Drop"))
          {
               _unlockScr = other.GetComponent<UnlockScr>();
          }
     }

     private void OnTriggerStay(Collider other)
     {

          if (other.CompareTag("Drop") && collected.Count > 0 && _unlockScr.Equals())
          {
               if (!_unlockScr.Equals() && _unlockScr.isFinish) StopAllCoroutines();
               else
               {
                    dropPos = other.transform.position;
                    Drag(other.transform);
               }
          }
     }

     public bool CollectedCount() => collected.Count > 0;

     private void OnTriggerExit(Collider other)
     {
          if (other.CompareTag("Drop"))
          {
               if (dragging != null) StopCoroutine(dragging);
               isDragging = false;
               onComplete = false;
               _unlockScr = null;
          }
     }

     
     

     public void Drag(Transform pos)
     {
          isDragging = true;
          dropPos = pos.position;
          StartCoroutine(DragSlowly(pos));
     }

     IEnumerator DragSlowly(Transform drop)
     {
          while (isDragging)
          {
               yield return new WaitForSeconds(waitingTime);

               if (collected.Count > 0)
               {
                    Transform newItem = collected.Pop().transform;

                    newItem.SetParent(null);

                    if (drop.GetComponent<UnlockScr>() != null)
                    {
                         if (!drop.GetComponent<UnlockScr>().isFinish)
                              drop.GetComponent<UnlockScr>().UnlockItem(1);
                         else{
                              Destroy(newItem.gameObject);
                              Debug.Log("Finish");
                              yield break;
                         }
                    }

                    newItem.DOJump(dropPos, 2f, 1, jumpDuration).OnComplete(()
                    => newItem.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.1f).
                    onComplete = () =>
                    newItem.gameObject.SetActive(false));
               }
          }



          yield return null;
     }

     IEnumerator ToPos(Transform drop, ItemCollectible item)
     {
          while (!onComplete)
          {
               drop.transform.DOLocalMove(new Vector3(0, collected.Count * yDistance, 0.1f), duration).onComplete = () =>
               {
                    item.collectible = true;
                    onComplete = true;
               };

               drop.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.5f);


               yield return new WaitForSeconds(.1f);
          }

          PlayerData.Instance.UpdateMoney(50);
          yield return null;
     }
}
