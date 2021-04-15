using System.Linq;
using UnityEngine;
using HYDAC_EView._Scripts.MPart;

namespace HYDAC_EView._Scripts
{
    public class MainManager : MonoBehaviour
    {
        [Range(0.1f, 2.0f)]
        [SerializeField] private float mPositionTimeChange = 5.0f;

        [SerializeField] private Color mPreviousAssemblyColor;
        [SerializeField] private Color mCurrentAssemblyColor;
        [SerializeField] private Color mNextAssemblyColor;

        [SerializeField] private int mNoOfAssemblies = 0;
        public int NoOfAssemblies { get => mNoOfAssemblies; }
        
        public IMachinePart[] MachineParts { get; private set; } = null;

        private void Awake()
        {
            GetMachineParts();
        }


        private void GetMachineParts()
        {
            // Get all machine parts
            MachineParts = FindObjectsOfType<MachinePart>() as IMachinePart[];

            var parts = MachineParts.ToList();

            // Sort parts by assembly position
            MachineParts = parts.OrderBy(x => x.GetAssemblyPosition()).ToArray();

            // Get total number of assemblies
            mNoOfAssemblies = MachineParts[MachineParts.Length - 1].GetAssemblyPosition();
        }


        public void OnUIChangeAssemblyPosition(float positionToChangeTo)
        {
            Debug.Log("#MainManager#-------------------------Changing assembly position to: " + positionToChangeTo);

            for (var i = 0; i < MachineParts.Length; i++)
            {
                var part = MachineParts[i];
                var partPosition = part.GetAssemblyPosition();

                if (partPosition <= positionToChangeTo)
                {
                    part.Explode(mPositionTimeChange);

                    // Highlight previous part
                    if (partPosition == positionToChangeTo - 1)
                    {
                        part.HighlightPart(true, mPreviousAssemblyColor);
                    }
                    // Highlight current part
                    else if(partPosition == positionToChangeTo)
                    {
                        part.HighlightPart(true, mCurrentAssemblyColor);
                    }
                    else
                    {
                        part.HighlightPart(false, mCurrentAssemblyColor);
                    }
                }
                else
                {
                    part.Implode(mPositionTimeChange);

                    // Highlight next part
                    if(partPosition == positionToChangeTo + 1)
                    {
                        part.HighlightPart(true, mNextAssemblyColor);
                    }
                    else
                    {
                        part.HighlightPart(false, mCurrentAssemblyColor);
                    }
                }
            }
        }
    }
}
