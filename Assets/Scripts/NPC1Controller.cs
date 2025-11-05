using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1Controller : MonoBehaviour, Interactable
{
    public float speed;
    private bool isMoving;
    private Vector2 input;

    private Animator animator;

    public LayerMask solidObjectsLayer;
    public LayerMask islandLayer;
    public LayerMask interactableLayer;
    public LayerMask playerLayer;

    public Dialog1 dialog;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(RandomMove());
    }

    private void Update()
    {
        
        if (!isMoving)
        {                         

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var playerPos = transform.position;
                playerPos.x += input.x;
                playerPos.y += input.y;

                if (!isSolid(playerPos) && !DialogManager1.Instance.isOpenDialog() && isInIsland(playerPos))// if (!isSolid(playerPos) && isInIsland(playerPos) && !DialogManager1.Instance.isOpenDialog())
                {                    
                    StartCoroutine(Move(playerPos));
                }
            }
        }
        animator.SetBool("isMoving", isMoving);
        
    }

    void GetInput () {
        input.x = Mathf.RoundToInt(UnityEngine.Random.Range(-1.0f, 1.0f)); 
        if (input.x != 0) input.y = 0;
        else input.y = Mathf.RoundToInt(UnityEngine.Random.Range(-1.0f, 1.0f));
    }

    void Stop () {
        input.x = 0; 
        input.y = 0;
    }

    IEnumerator RandomMove()
    {
        for (;;)
        {
            int counter = Mathf.RoundToInt(UnityEngine.Random.Range(1.0f, 5.0f));
            
            // Comprobamos que DialogManager1.Instance no sea null
            if (DialogManager1.Instance == null)
            {
                Debug.LogError("DialogManager1.Instance es null. Asegúrate de que está inicializado correctamente.");
                yield break;
            }

            if (!DialogManager1.Instance.IsTyping())
            {
                GetInput();
                while (counter > 0)
                {
                    yield return new WaitForSeconds(1f);
                    counter--;
                }
            }
            else
            {
                Stop();
                counter = Mathf.RoundToInt(UnityEngine.Random.Range(1.0f, 5.0f));
                while (counter > 0)
                {
                    yield return new WaitForSeconds(1f);
                    counter--;
                }
            }
        }
    }

    IEnumerator Move(Vector3 playerPos)
    {
        Debug.Log($"Iniciando movimiento hacia: {playerPos}");
        isMoving = true;

        while ((playerPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = playerPos;
        isMoving = false;
        Debug.Log("Movimiento completado.");
    }


    private bool isSolid(Vector3 playerPos)
    {
        if (Physics2D.OverlapCircle(playerPos, 0.2f, solidObjectsLayer | interactableLayer | playerLayer) != null)
        {
            return true;
        }
        return false;
    }

    private bool isInIsland(Vector3 playerPos)
    {
        if (Physics2D.OverlapCircle(playerPos, 0.2f, islandLayer) != null)
        {
             return true;
        }
        return false;
    }

    public void Interact()
    {
        StartCoroutine(DialogManager1.Instance.ShowDialog(dialog));
    }



}

