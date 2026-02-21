using UnityEngine;

[CreateAssetMenu(menuName = "Selection/Random")]
public class Selection_Random : SelectEffect
{
    public float     successRate = 0.5f;

    public SelectEffect failedSelection;
    public SelectEffect successSelection;

    public override void Select()
    {
        if (Random.Range(0f, 1f) < successRate)
            failedSelection.Select();
        else 
            successSelection.Select();
    }
}