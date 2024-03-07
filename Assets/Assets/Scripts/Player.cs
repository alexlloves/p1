using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float sumaCantidad = 1.1f; // Cantidad de reducción en cada paso
    public float MaxScale = 4.0f; // Escala maxima
    public float powerUpDuration = 10f;
  
    private bool isPowerUpActive = false;
    private Coroutine powerUpCoroutine;


    [SerializeField] private float speed;
   
    [SerializeField] private float bound = 4.5f; // x axis bound 

    private Vector2 originalScale; // Posición inicial del jugador


    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.position; // Guardamos la posición inicial del jugador
    }

    // Update is called once per frame
    void Update()
    {
       PlayerMovement();
    }

    void PlayerMovement()
    {
         float moveInput = Input.GetAxisRaw("Horizontal");
        // Controlaríamo el movimiento de la siguiente forma de no ser el rigidbody quinemático
        // transform.position += new Vector3(moveInput * speed * Time.deltaTime, 0f, 0f);

        Vector2 playerPosition = transform.position;
        // Mathf.Clamp nos permite limitar un valor entre un mínimo y un máximo
        playerPosition.x = Mathf.Clamp(playerPosition.x + moveInput * speed * Time.deltaTime, -bound, bound);
        transform.position = playerPosition;
    }

    public void ResetPlayer()
    {
        transform.position = originalScale; // Posición inicial del jugador
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("powerUp")) // Si colisionamos con un powerUp
        {
            Destroy(collision.gameObject); // Lo destruimos
            GameManager.Instance.AddLife(); // Añadimos una vida
        }
        if (collision.CompareTag("powerAzul")) // Si colisionamos con un powerUp
        {
            Destroy(collision.gameObject); // Lo destruimos
            GameManager.Instance.LoseLife(); // Añadimos una vida
        }
        if (collision.CompareTag("powerVerde") && !isPowerUpActive)
        {
            if (powerUpCoroutine != null)
                StopCoroutine(powerUpCoroutine);

            powerUpCoroutine = StartCoroutine(ActivatePowerUp());
            //Destroy(collision.gameObject); // Elimina el objeto del power-up al recogerlo

        }
    }

    private IEnumerator ActivatePowerUp()
    {
        isPowerUpActive = true;

        hacerGrande(); // Llamar a la función hacerGrande() aquí para agrandar al jugador

        // Esperar la duración del power-up
        yield return new WaitForSeconds(powerUpDuration);

        // Volver al estado inicial (restaurar la escala original)
        transform.localScale = originalScale;

        isPowerUpActive = false;
    }

    public void hacerGrande()
    {
        Vector3 currentScale = transform.localScale;

        Vector3 newScale = currentScale + new Vector3(currentScale.x, 0, 0);

        // Asegurar que la nueva escala no sea menor que la escala mínima
        newScale = Vector3.Min(newScale, new Vector3(MaxScale, MaxScale, 0));

        // Aplicar la nueva escala al objeto
        transform.localScale = newScale;
    }

   

}
