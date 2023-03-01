using UnityEngine;

public class Towers : MonoBehaviour
{
    public Kule kule;

    public Transform GetPivotPos(){
        return kule.pivotPos;
    }

    public void DestroyGameObject(){
        Destroy(gameObject);
    }

}
