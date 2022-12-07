using UnityEngine;
using UnityEngine.Rendering;

public class BlitController : MonoBehaviour
{
    //public RenderPipelineAsset exampleAssetA;
    //public RenderPipelineAsset exampleAssetB;

    public float _Percentage;
    public bool _Fired = false;

    public Material _Mat;
    private void Start()
    {
    }
    void Update()
    {
        if(!_Fired && (_Percentage >= 0))
        {
            _Percentage = 0;
        }
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    //GraphicsSettings.renderPipelineAsset = exampleAssetA;
        //    //Debug.Log("Default render pipeline asset is: " + GraphicsSettings.renderPipelineAsset.name);
        //_Percentage += Time.deltaTime / 1;
        //_Mat.SetFloat("_Percent", _Percentage);
        //}
        //else if (Input.GetKeyDown(KeyCode.B))
        //{
        //    //GraphicsSettings.renderPipelineAsset = exampleAssetB;
        //    //Debug.Log("Default render pipeline asset is: " + GraphicsSettings.renderPipelineAsset.name);
        //}
        //if (_Fired)
        //{
        //    _Percentage += Time.deltaTime * 2;
        //    _Mat.SetFloat("_Percent", _Percentage);
        //    if (_Percentage > 1)
        //    {
        //        _Percentage = 0;
        //        _Fired = false;
        //    }
        //}
        //if (Input.GetKeyDown("space"))
        //{
        //    _Percentage = 0;
        //    _Fired = true;
        //}
    }
}
