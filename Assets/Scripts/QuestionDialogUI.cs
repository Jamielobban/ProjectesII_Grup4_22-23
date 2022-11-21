using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class QuestionDialogUI : MonoBehaviour
{
    public static QuestionDialogUI Instance { get; private set; }

    private TextMeshProUGUI textMeshPro;
    private Button yesBtn;
    private Button noBtn;

    private void Awake()
    {
        Instance = this;

        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        yesBtn = transform.Find("YesBtn").GetComponent<Button>();
        noBtn = transform.Find("NoBtn").GetComponent<Button>();

        Hide();
        //ShowQuestion("Do yo want to do this?", () => {
        //    Debug.Log("Yes");
        //}, () => {
        //    Debug.Log("No");
        //});
    }

    public void ShowQuestion(string questionText, Action yesAction, Action noAction)
    {
        gameObject.SetActive(true);

        textMeshPro.text = questionText;
        yesBtn.onClick.AddListener( () =>{
            Hide();
            yesAction();
        });
        noBtn.onClick.AddListener(() => {
            Hide();
            noAction();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
