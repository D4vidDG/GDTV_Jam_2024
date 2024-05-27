
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float knockbackSensitivity = 1f;
    [SerializeField] float drag;

    Rigidbody2D rigidBody;
    Animator animator;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        rigidBody.drag = drag;
    }

    private void LateUpdate()
    {
        animator.SetFloat("Speed", rigidBody.velocity.magnitude);
    }

    public void ApplyKnockback(float knockback, Vector2 direction)
    {
        rigidBody.AddForce(-direction.normalized * knockback * knockbackSensitivity, ForceMode2D.Impulse);
    }
}
