/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

/** About graphics
* -> Toggles between high and low graphics
*/

public class graphics : MonoBehaviour
{
    public RenderPipelineAsset[] qualityLevels;
    private TMP_Dropdown dropdown;
    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.value = QualitySettings.GetQualityLevel();
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        QualitySettings.renderPipeline = qualityLevels[qualityIndex];
    }
}
