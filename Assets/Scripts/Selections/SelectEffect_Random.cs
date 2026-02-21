using UnityEngine;

[CreateAssetMenu(menuName = "Selection/Random")]
public class SelectEffect_Random : SelectEffect
{
    public float     successRate = 0.5f;

    public SelectEffect failedSelection;
    public SelectEffect successSelection;

    bool isSuccess = false;

    public override void Select()
    {
        if (Random.Range(0f, 1f) < successRate)
        {
            isSuccess = false;
            failedSelection.Select();
        }
        else
        {
            isSuccess = true;
            successSelection.Select();
        }
    }

    public override string Get_AfterMathDialog()
    {
        return isSuccess ? successSelection.Get_AfterMathDialog() : failedSelection.Get_AfterMathDialog();
    }
}