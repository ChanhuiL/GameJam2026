using UnityEngine;

public abstract class SelectEffect : ScriptableObject
{
    public string displayName;
    [TextArea] public string aftermathDialog;

    public abstract void Select();
    public virtual string Get_AfterMathDialog()
    {
        return aftermathDialog;
    }
}
