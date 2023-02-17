
using UnityEngine;
using UnityEngine.Rendering;

public class BlitController : MonoBehaviour
{
    //public RenderPipelineAsset exampleAssetA;
    //public RenderPipelineAsset exampleAssetB;

    public float _Percentage;
    public float _Size;
    public bool isExpanding;
    public Material _Mat;
    private CircleCollider2D circle;
    private void Start()
    {
        circle = GetComponent<CircleCollider2D>();
        circle.enabled = false;
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
        if (collision.gameObject.CompareTag("EnemyBullet")){
            collision.GetComponent<EnemyProjectile>().DestroyProjectile();
        }
        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    Debug.Log("hIT ENEMY");
        //    collision.GetComponent<Entity>().rb.AddForce((-1*(collision.GetComponent<Entity>().vectorToPlayer).normalized)*100000, ForceMode2D.Impulse);
        //}
    }
}

