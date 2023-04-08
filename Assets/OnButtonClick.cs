using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using DG.Tweening;
public class OnButtonClick : MonoBehaviour
{
    // Start is called before the first frame update

    public Button playZoom;
    public CinemachineVirtualCamera thisCam;
    [SerializeField] Animator prisonDoorOpen;
    bool zoomOut;
    bool isDoneOpening;
    public float velocity = 0.005f;
    public float acceleration = 0.00025f;
    public float velocityZoomOut = 0.0025f;
    public float accelerationZoomOut = 0.01f;
    void Start()
    {
        playZoom = GetComponent<Button>();
        playZoom.onClick.AddListener(TaskOnClick);
        thisCam = FindObjectOfType<CinemachineVirtualCamera>();
        zoomOut = false;
        isDoneOpening = false;
    }

    void TaskOnClick()
    {

        StartCoroutine(SetBoolTrue());
        //Debug.Log("Clicked");
    }
    // Update is called once per frame
    void Update()
    {
        if(thisCam.m_Lens.OrthographicSize <= 5.5f && zoomOut)
        {
            //Debug.Log("what");
            thisCam.m_Lens.OrthographicSize += 0.0025f;
            if (thisCam.m_Lens.OrthographicSize > 5.3f)
            {
                prisonDoorOpen.SetBool("hasPressedPlay", true);

            }
            if (thisCam.m_Lens.OrthographicSize > 5.5f)
            {
                //prisonDoorOpen.SetBool("hasPressedPlay", true);
                isDoneOpening = true;
                zoomOut = false;
            }
        }

        if (isDoneOpening && thisCam.m_Lens.OrthographicSize >= 0.3f)
        {
            //Debug.Log("ZoomIN");
            thisCam.m_Lens.OrthographicSize -= velocity;
            velocity += acceleration + (Time.deltaTime/30f);
            if (thisCam.m_Lens.OrthographicSize < 0.3f)
            {
                thisCam.m_Lens.OrthographicSize = 0.29f;
                isDoneOpening = false;
                //Debug.Log("nOW LOAD");
            }
        }
    }

    private IEnumerator SetBoolTrue()
    {
        yield return new WaitForSeconds(0.5f);
        zoomOut = true;
    }
}
