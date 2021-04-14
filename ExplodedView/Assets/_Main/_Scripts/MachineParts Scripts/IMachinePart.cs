using UnityEngine;

public interface IMachinePart
{
    int GetAssemblyPosition();
    string GetPartName();
    void Implode(float timeToDest);
    void Explode(float timeToDest);
    void HighlightPart(bool _toggle, Color _highlightColor);
}
