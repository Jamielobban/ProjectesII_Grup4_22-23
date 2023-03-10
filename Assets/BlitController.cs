using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using DG.Tweening;
public class BlitController : MonoBehaviour
{
    //public RenderPipelineAsset exampleAssetA;
    //public RenderPipelineAsset exampleAssetB;

    public float _Percentage;
    public float _Size;

    public float _Intensity;
    public bool isExpanding;
    public Material _Mat;
    public Material _HitMaterial;
    private CircleCollider2D circle;

    public PlayerMovement player;
    private void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
        circle = GetComponent<CircleCollider2D>();
        circle.enabled = false;
        _Intensity = 0f;
        _HitMaterial.SetFloat("_VignetteIntensity", _Intensity);

    }
    void Update()
    {
        //if(!_Fired && (_Percentage >= 0))
        //{
        //    _Percentage = 0;
        //    _Mat.SetFloat("_Percent", _Percentage);
        //}
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

        if (isExpanding)
        {
            circle.enabled = true;
            circle.radius += 0.5f;
            _Size = 0.1f;
            _Mat.SetFloat("_Size", _Size);
            _Percentage += Time.deltaTime * 2;
            _Mat.SetFloat("_Percent", _Percentage);
            if (_Percentage > 1)
            {
                circle.radius = 1;
                circle.enabled = false;
                _Percentage = 0;
                _Mat.SetFloat("_Percent", _Percentage);
                _Size = 0;
                _Mat.SetFloat("_Size", _Size);
                isExpanding = false;
            }
        }

        if (player.isHit)
        {
            _HitMaterial.DOFloat(0.902f, "_VignetteIntensity", 0.2f);
            StartCoroutine(WaitForGoDown());
            
        }

        //_Percentage = 0;
        //_Mat.SetFloat("_Percent", _Percentage);


        //}
        //if (Input.GetKeyDown("space"))
        //{
        //    _Percentage = 0;
        //    _Fired = true;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            collision.GetComponent<CircleCollider2D>().enabled = false;
            Destroy(collision.gameObject, 0.1f);

        }
    }

    private IEnumerator WaitForGoDown()
    {
        yield return new WaitForSeconds(0.21f);
        _HitMaterial.DOFloat(0f, "_VignetteIntensity", 0.1f);
    }
}