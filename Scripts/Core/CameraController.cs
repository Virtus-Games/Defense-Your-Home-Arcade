using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   #region Serialized Fields
   [SerializeField] float followSpeed;
   [SerializeField] Transform target;
     #endregion

     private void Update() => transform.position = Vector3.Slerp(transform.position, target.position, followSpeed * Time.deltaTime);
}
