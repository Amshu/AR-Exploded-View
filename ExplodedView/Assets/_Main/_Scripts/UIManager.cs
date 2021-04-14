using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MainManager))]
public class UIManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "MachinePart";

    [Header("Control UI Elements")]
    [SerializeField] private Slider m_TimelineSlider = null;
    [SerializeField] private Button m_ImplodeBtn = null;
    [SerializeField] private Button m_ExplodeBtn = null;

    [Header("Info UI Elements")]
    [SerializeField] private Transform m_PartButtonParent = null;
    [SerializeField] private GameObject m_PartButtonPrefab = null;

    private MainManager m_MainManager = null;
    private MachinePart m_currentSelectedPart = null;

    private void Awake()
    {
        m_MainManager = GetComponent<MainManager>();
    }

    private void Start()
    {
        m_TimelineSlider.maxValue = m_MainManager.NoOfAssemblies;
        CreateUIButtons(m_MainManager.MachineParts);
    }

    private void OnEnable()
    {
        m_TimelineSlider.onValueChanged.AddListener(OnTimeLineChanged);
        m_ImplodeBtn.onClick.AddListener(OnImplodeBtnClick);
        m_ExplodeBtn.onClick.AddListener(OnExplodeBtnClick);
    }


    private void OnDisable()
    {
        m_TimelineSlider.onValueChanged.RemoveListener(OnTimeLineChanged);
        m_ImplodeBtn.onClick.RemoveListener(OnImplodeBtnClick);
        m_ExplodeBtn.onClick.RemoveListener(OnExplodeBtnClick);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                if (selection.CompareTag(selectableTag))
                {
                    m_currentSelectedPart = selection.GetComponent<MachinePart>();
                    m_TimelineSlider.value = m_currentSelectedPart.PartInfo.AssemblyPosition;
                }
            }
        }
    }

    private void CreateUIButtons(IMachinePart[] parts)
    {
        for (int i = 0; i < parts.Length; i++)
        {
            GameObject temp = Instantiate(m_PartButtonPrefab, m_PartButtonParent);
            temp.GetComponent<BTN_MachinePart>().Initialize(parts[i].GetAssemblyPosition(), parts[i].GetPartName());
        }
    }


    private void OnImplodeBtnClick()
    {
        // Update timeline slider
        m_TimelineSlider.value = 0;
    }

    private void OnExplodeBtnClick()
    {
        // Update timeline slider
        m_TimelineSlider.value = m_MainManager.NoOfAssemblies;
    }

    private void OnTimeLineChanged(float assemblyNumber)
    {
        m_MainManager.OnUIChangeAssemblyPosition(assemblyNumber);
    }
}
