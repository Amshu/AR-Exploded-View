using System;
using UnityEngine;

[CreateAssetMenu(fileName = "00_INFO_PartName", menuName = "MachinePart/Info", order = 1)]
public class SOC_MachinePartInfo : ScriptableObject
{
    public string PartName = "";
    public int AssemblyPosition = 0;

    [TextArea]
    public string PartInfo = "";

    public void PrintInfo()
    {
        Debug.LogFormat("#SOC_MachinePartInfo#-------------------------{0}{1}\nPartInfo: {2}", 
            AssemblyPosition, PartName , PartInfo);
    }
}