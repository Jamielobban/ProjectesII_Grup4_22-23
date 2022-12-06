using UnityEngine;
using UnityEngine.Rendering;

public class BlitController : MonoBehaviour
{
    public RenderPipelineAsset exampleAssetA;
    public RenderPipelineAsset exampleAssetB;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            GraphicsSettings.renderPipelineAsset = exampleAssetA;
            Debug.Log("Default render pipeline asset is: " + GraphicsSettings.renderPipelineAsset.name);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            GraphicsSettings.renderPipelineAsset = exampleAssetB;
            Debug.Log("Default render pipeline asset is: " + GraphicsSettings.renderPipelineAsset.name);
        }
    }
}