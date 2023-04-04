using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CircleTransition : MonoBehaviour
{

    public Transform player;
    private Canvas _canvas;
    public Image _image;
    public PlayerMovement playerCheck;
    private Vector2 _playerCanvasPos;
    public PlayerMovement playerSignal;

    public PlayerMovement playerCheckpoints;

    //public static CircleTransition Instance { get; private set; }

    private void Awake()
    {
        playerCheckpoints = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        _canvas = GetComponent<Canvas>();
        //if (Instance != null && Instance != _canvas)
        //{
        //    Destroy(_canvas);
        //}
        //else
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(_canvas);
        //}
    }

    private void Update()
    {
        if (playerCheckpoints.restart)
        {
            playerCheckpoints.restart = false;

            CloseBlackScreen();
        }
        //else if (Input.GetKeyDown((KeyCode.Alpha2)))
        //{
        //    CloseBlackScreen();
        //}
    }

    private void Start()
    {
        DrawBlackScreen();
        _image.material.SetFloat("_Radius", 1);
        //Invoke("OpenBlackScreen", 0.5f);
        StartCoroutine(SetPlayerMovement());
    }

    public void OpenBlackScreen()
    {
        StartCoroutine(Transition(0.4f, 0.062f, 1));
        //StartCoroutine(setPlayerMovement());
    }

    public void CloseBlackScreen()
    {
        StartCoroutine(Transition(0.5f, 1, 0.062f));
        Invoke("OpenBlackScreen", 0.75f);
        //Invoke("ResetCurrentScene", 3f);
    }

    private void DrawBlackScreen()
    {

        var screenWidth = Screen.width*2;
        var screenHeight = Screen.height*2;
        var playerScreenPos = Camera.main.WorldToScreenPoint(player.position);
        //Debug.Log(playerScreenPos);

        var canvasRect = _canvas.GetComponent<RectTransform>().rect;
        var canvasWidth = canvasRect.width;
        var canvasHeight = canvasRect.height;


        _playerCanvasPos = new Vector2
        {
            x = (playerScreenPos.x / screenWidth) * canvasWidth,
            y = (playerScreenPos.y / screenHeight) * canvasHeight,
        };


        var squareValue = 0f;
        if (canvasWidth > canvasHeight)
        {
            squareValue = canvasWidth;
            _playerCanvasPos.y += (canvasWidth - canvasHeight) * 0.5f;
        }
        else
        {
            squareValue = canvasHeight;
            _playerCanvasPos.x += (canvasHeight - canvasWidth) * 0.5f;
        }

        _playerCanvasPos /= squareValue;

        var mat = _image.material;
        mat.SetFloat("_CenterX", 0.5f);
        mat.SetFloat("_CenterY", 0.5f);

        _image.rectTransform.sizeDelta = new Vector2(squareValue, squareValue);
    }

    private IEnumerator Transition(float duration, float beginRadius, float endRadius)
    {
        var time = 0f;
        while (time <= duration)
        {
            time += Time.deltaTime;
            var t = time / duration;
            var radius = Mathf.Lerp(beginRadius, endRadius, t);

            _image.material.SetFloat("_Radius", radius);

            yield return null;
        }
    }

    private IEnumerator SetPlayerMovement()
    {
        yield return new WaitForSeconds(1f);
        //Debug.Log("Im moving");
        playerSignal.canMove = true;
    }

    public void ResetCurrentScene()
    {

            if (!playerCheckpoints.endTutorial)
            {

                if (GameObject.FindGameObjectWithTag("Potion") != null)
                    PlayerPrefs.SetInt("Potions", 0);

                playerCheck.isDead = true;
                PlayerPrefs.SetInt("isDead", (true ? 1 : 0));
                playerCheckpoints.Reaparecer();
         

            }
            else
            {
                //Establecer el spawn al salir por primera vez de la sala principal
                PlayerPrefs.SetInt("IDCheckpoints", 1);
                PlayerPrefs.SetInt("IDScene", 3);
                playerCheckpoints.SpawnSalaPrincipal();
            }
        

    }
}
