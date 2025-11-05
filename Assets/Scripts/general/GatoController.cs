using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatoController : MonoBehaviour, Interactable
{
    private Animator animator;
    private bool isWalking = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (!isWalking)
        {
            StartCoroutine(WalkRight());
        }
    }

    private IEnumerator WalkRight()
    {
        isWalking = true;

        // Activar animación de caminar
        animator.SetBool("isWalking", true);

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + Vector3.right * 1; // Se mueve 2 unidades a la derecha

        while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 2f * Time.deltaTime);
            yield return null;
        }

        // Detener animación de caminar
        animator.SetBool("isWalking", false);
        isWalking = false;
    }
}

