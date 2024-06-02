using UnityEngine;

public abstract class UIShop : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] bool canAccessShop;

    bool opened;

    private void Start()
    {
        opened = false;
        ToggleAccess(true);
    }

    public virtual void Open()
    {
        if (!canAccessShop) return;
        GameManager.instance.ToggleControl(false);
        opened = true;
        panel.SetActive(true);

    }

    public virtual void Close()
    {
        GameManager.instance.ToggleControl(true);
        opened = false;
        panel.SetActive(false);
    }

    public bool IsOpened()
    {
        return opened;
    }


    public void ToggleAccess(bool toggle)
    {
        canAccessShop = toggle;
    }
}