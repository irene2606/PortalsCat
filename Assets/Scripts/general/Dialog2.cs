using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog2 : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)]private string[] dialogLines;

    private float typingTime = 0.05f;

    private bool isPlayerInRange;
    private bool didDialogStart;
    private int lineIndex;

    // Update is called once per frame
    void Update()
    {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.E)){
            if(!didDialogStart){
                StartDialog();
            }
        }
    }

    private void StartDialog(){
        didDialogStart=true;
        dialoguePanel.SetActive(true);
        lineIndex=0;
        StartCoroutine(ShowLine());
    }

    private IEnumerator ShowLine(){
        dialogueText.text=string.Empty;

        foreach(char ch in dialogLines[lineIndex]){
            dialogueText.text +=ch;
            yield return new WaitForSeconds(typingTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Gato")){
            isPlayerInRange=true;
            Debug.Log("se puede iniciar dialogo");
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision) {

        if(collision.gameObject.CompareTag("Gato")){
            isPlayerInRange=false;
            Debug.Log("no se puede iniciar dialogo");
        }
    }
}