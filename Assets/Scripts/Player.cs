using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    [Header("Rellenos")]
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    Vector2 rawInput;
    Vector2 minBounds; // Parte inferior izquierda de la pantalla normalizada
    Vector2 maxBounds; // Parte superior derecha de la pantalla normalizada

    Shooter shooter;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        InitBounds();
    }

    void Update()
    {
        Move();
    }

    void InitBounds()
    {
        // ViewportToWorldPoint() convierte la posición normalizada de la cámara en la posición mundial en Vector3
        // En nuestro caso nos interesa las esquinas (0,0) y (1,1)
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(Vector2.zero);
        maxBounds = mainCamera.ViewportToWorldPoint(Vector2.one);
    }

    void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;

        // Limitamos con Clamp() la posición del jugador con un mínimo y máxima distancia que puede recorrer
        Vector2 limits = new Vector2();
        limits.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        limits.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        
        transform.position = limits;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}
