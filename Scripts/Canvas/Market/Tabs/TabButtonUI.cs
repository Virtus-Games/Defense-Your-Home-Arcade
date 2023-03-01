using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabButtonUI : MonoBehaviour
{
     public Button uiButton;
     public Image uiImage;
     public LayoutElement uiLayoutElement;
     public TextMeshProUGUI title;

     public void Initiliazing(string title)
     {
          this.title.text = title;
     }

     public void Clicked()
     {
          //MarketManager.Instance.ButtonIsClicked();
     }


}
