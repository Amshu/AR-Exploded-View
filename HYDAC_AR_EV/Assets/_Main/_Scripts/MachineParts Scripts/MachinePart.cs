using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class MachinePart : MonoBehaviour, IMachinePart
{
    [SerializeField] private SOC_MachinePartInfo m_PartInfo = null;
    public SOC_MachinePartInfo PartInfo { get => m_PartInfo; }

    [SerializeField] private Transform m_ImplodedTransform = null;
    [SerializeField] private Transform m_ExplodedTransform = null;

    private bool m_lock = false;
    private Outline m_outline = null;

    private void Awake()
    {
        m_outline = GetComponent<Outline>();
        m_outline.enabled = false;
    }


    IEnumerator LerpPosition(Transform transform, Vector3 position, float _timeTakenToDest)
    {
        var currentPos = transform.position;
        var t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime / _timeTakenToDest;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }

        m_lock = false;
    }


    #region IMachinePart interface methods

    int IMachinePart.GetAssemblyPosition()
    {
        return m_PartInfo.AssemblyPosition;
    }


    void IMachinePart.Implode(float _timeTakenToDest)
    {
        Debug.Log("#MachinePart#-------------------------Implode :");
        m_PartInfo.PrintInfo();

        if (!m_lock)
        {
            m_lock = true;
            StartCoroutine(LerpPosition(this.transform, m_ImplodedTransform.position, _timeTakenToDest));
        }
    }


    void IMachinePart.Explode(float _timeTakenToDest)
    {
        Debug.Log("#MachinePart#-------------------------Explode");
        if (!m_lock)
        {
            m_lock = true;
            StartCoroutine(LerpPosition(this.transform, m_ExplodedTransform.position, _timeTakenToDest));
        }
    }
    

    void IMachinePart.HighlightPart(bool _toggle, Color _highlightColor)
    {
        m_outline.enabled = _toggle;

        if (_toggle)
            m_outline.OutlineColor = _highlightColor;
    }

    public string GetPartName()
    {
        return m_PartInfo.PartName;
    }

    #endregion

}
