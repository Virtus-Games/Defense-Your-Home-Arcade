using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buy : MonoBehaviour
{
     public GameObject BuyHelperPanel;

     private void OnTriggerEnter(Collider other)
     {
          if (other.gameObject.CompareTag("Player"))
               BuyHelperPanel.SetActive(true);
     }

     private void OnTriggerExit(Collider other)
     {
          if (other.gameObject.CompareTag("Player"))
               BuyHelperPanel.SetActive(false);
     }

}
