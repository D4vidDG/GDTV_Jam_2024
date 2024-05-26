
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float knockbackSensitivity = 1f;
    [SerializeField] float drag;

    Rigidbody2D rigidBody;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rigidBody.drag = drag;
    }

    private void FixedUpdate()
    {

    }

    public void ApplyKnockback(float knockback, Vector2 direction)
    {
        rigidBody.AddForce(-direction.normalized * knockback * knockbackSensitivity, ForceMode2D.Impulse);
    }
}
