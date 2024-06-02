using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InteractableShop : MonoBehaviour
{
    const KeyCode KEY_TO_INTERACT = KeyCode.E;
    private const string OUTLINE_ENABLED_PROPERTY = "_OutlineEnabled";
    [SerializeField] GameObject reminder;

    bool playerWithinInteractionRadius;

    public UnityEvent OnPlayerInteract;

    [SerializeField] Material enabledOutline;
    [SerializeField] Material disabledOutline;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // enabledOutline = new MaterialPropertyBlock();
        // disabledOutline = new MaterialPropertyBlock();
        // enabledOutline.SetFloat(OUTLINE_ENABLED_PROPERTY, 1);
        // disabledOutline.SetFloat(OUTLINE_ENABLED_PROPERTY, 0);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        reminder.SetActive(false);
        playerWithinInteractionRadius = false;
    }

    private void Update()
    {

        if (playerWithinInteractionRadius)
        {
            spriteRenderer.material = enabledOutline;
            if (Input.GetKeyDown(KEY_TO_INTERACT)) OnPlayerInteract?.Invoke();
        }
        else
        {
            spriteRenderer.material = disabledOutline;

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerWithinInteractionRadius = true;
            reminder.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerWithinInteractionRadius = false;
            reminder.SetActive(false);
        }
    }
}
