using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public int damageToGive = 5;        // Daño que inflige el ataque
    public float attackRange = 1f;     // Rango de ataque

    private Animator animator;
    private bool hasAttacked = false;  // Bandera para controlar si ya atacó

    private bool muerto;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Si el jugador está en la animación de ataque y aún no ha atacado
        if (animator.GetBool("Atacando") && !hasAttacked)
        {
            Attack();
            hasAttacked = true; // Marcar que ya atacó
        }

        // Restablecer la bandera cuando la animación termine
        if (!animator.GetBool("Atacando"))
        {
            hasAttacked = false;
        }
    }

    void Attack()
    {
        // Obtener la dirección del ataque según la animación
        Vector2 attackDirection = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));

        if (attackDirection != Vector2.zero)
        {
            // Determinar la posición del ataque
            Vector2 attackPosition = (Vector2)transform.position + attackDirection;

            // Buscar todos los objetos en el rango de ataque
            Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPosition, attackRange);

            foreach (Collider2D obj in hitObjects)
            {
                // Verificar si el objeto tiene la etiqueta "Enemy"
                if (obj.CompareTag("Enemy"))
                {
                    EnemyHealth eHealthMan = obj.GetComponent<EnemyHealth>();
                    SlimeEnemyController slime = obj.GetComponent<SlimeEnemyController>();
                    HurtPlayer slimeHurt = obj.GetComponent<HurtPlayer>();
                    if (eHealthMan != null)
                    {
                        eHealthMan.HurtEnemy(damageToGive);
                        slime.ReceiveDamage();
                        slimeHurt.Damaged(true);

                    }
                }
            }
        }
    }

    /*private void OnDrawGizmosSelected()
    {
        // Mostrar el rango de ataque en la escena para depuración
        Gizmos.color = Color.red;

        if (animator != null)
        {
            Vector2 attackDirection = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
            if (attackDirection != Vector2.zero)
            {
                Vector2 attackPosition = (Vector2)transform.position + attackDirection;
                Gizmos.DrawWireSphere(attackPosition, attackRange);
            }
        }
    }*/
}

