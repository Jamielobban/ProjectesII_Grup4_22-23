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

    private void Awake()
    {
           _canvas = GetComponent<Canvas>();
    }

    private void Update()
    {
        if (playerCheck.isDead)
        {
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
        Invoke("OpenBlackScreen", 0.5f);
    }

    public void OpenBlackScreen()
    {
        StartCoroutine(Transition(2, 0, 1));
        StartCoroutine(setPlayerMovement());
    }

    public void CloseBlackScreen()
    {
        StartCoroutine(Transition(2, 1, 0));
        Invoke("ResetCurrentScene", 3f);
    }

    private void DrawBlackScreen()
    {

        var screenWidth = Screen.width*2;
        var screenHeight = Screen.height*2;
        var playerScreenPos = Camera.main.WorldToScreenPoint(player.position);

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
        mat.SetFloat("_CenterX", _playerCanvasPos.x);
        mat.SetFloat("_CenterY", _playerCanvasPos.y);

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

    private IEnumerator setPlayerMovement()
    {
        yield return new WaitForSeconds(1);
        playerSignal.canMove = true;
    }

    public void ResetCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
