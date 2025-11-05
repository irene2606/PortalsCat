/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;


public enum GameState{Playing, Waiting, Dialog};
public class GameManager : MonoBehaviour
{

    GameState state;

    [SerializeField] PlayerMovement playerController;

    public void Start()
    {
        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };

        DialogManager.Instance.OnHideDialog += () =>
        {
            if (state == GameState.Dialog)
                state = GameState.Playing;
        };
    }

    private void Update()
    {
        if (state == GameState.Playing)
        {

        }else if(state == GameState.Waiting)
        {

        }else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate(); 
        }
    }

    



}*/





