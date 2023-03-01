using UnityEngine;

public class UpdatePanelManager : MonoBehaviour
{
     public GameObject UpdatePanel;

     private void OnTriggerEnter(Collider other)
     {
          if (other.gameObject.CompareTag("Player"))
               UpdatePanel.SetActive(true);
     }

     private void OnTriggerExit(Collider other)
     {
          if (other.gameObject.CompareTag("Player"))
               UpdatePanel.SetActive(false);
     }
}