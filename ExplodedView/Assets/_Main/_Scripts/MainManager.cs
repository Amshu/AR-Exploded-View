using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class MainManager : MonoBehaviour
{
    [Range(0.1f, 2.0f)]
    [SerializeField] private float m_PositionTimeChange = 5.0f;

    [SerializeField] private Color m_PreviousAssemblyColor;
    [SerializeField] private Color m_CurrentAssemblyColor;
    [SerializeField] private Color m_NextAssemblyColor;

    [SerializeField] private int m_NoOfAssemblies = 0;
    public int NoOfAssemblies { get => m_NoOfAssemblies; }

    private IMachinePart[] m_MachineParts = null;
    public IMachinePart[] MachineParts { get => m_MachineParts; }

    private void Awake()
    {
        GetMachineParts();
    }


    private void GetMachineParts()
    {
        // Get all machine parts
        m_MachineParts = FindObjectsOfType<MachinePart>() as IMachinePart[];

        List<IMachinePart> parts = new List<IMachinePart>();

        for (int i = 0; i < m_MachineParts.Length; i++)
        {
            parts.Add(m_MachineParts[i]);
        }

        // Sort parts by assembly position
        m_MachineParts = parts.OrderBy(x => x.GetAssemblyPosition()).ToArray();

        // Get total number of assemblies
        m_NoOfAssemblies = m_MachineParts[m_MachineParts.Length - 1].GetAssemblyPosition();
    }


    public void OnUIChangeAssemblyPosition(float positionToChangeTo)
    {
        Debug.Log("#MainManager#-------------------------Changing assembly position to: " + positionToChangeTo);

        for (int i = 0; i < m_MachineParts.Length; i++)
        {
            IMachinePart part = m_MachineParts[i];
            int partPosition = part.GetAssemblyPosition();

            if (partPosition <= positionToChangeTo)
            {
                part.Explode(m_PositionTimeChange);

                // Highlight previous part
                if (partPosition == positionToChangeTo - 1)
                {
                    part.HighlightPart(true, m_PreviousAssemblyColor);
                }
                // Highlight current part
                else if(partPosition == positionToChangeTo)
                {
                    part.HighlightPart(true, m_CurrentAssemblyColor);
                }
                else
                {
                    part.HighlightPart(false, m_CurrentAssemblyColor);
                }
            }
            else
            {
                part.Implode(m_PositionTimeChange);

                // Highlight next part
                if(partPosition == positionToChangeTo + 1)
                {
                    part.HighlightPart(true, m_NextAssemblyColor);
                }
                else
                {
                    part.HighlightPart(false, m_CurrentAssemblyColor);
                }
            }
        }
    }
}
