using UnityEngine;

public abstract class UIShop : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] bool canAccessShop;

    bool opened;

    protected ShopItem[] items;

    private void Awake()
    {
        items = GetComponentsInChildren<ShopItem>(true);
        Initialize();
    }

    protected abstract void Initialize();

    protected void OnEnable()
    {
        PauseMenu.OnPause += OnPause;

        foreach (ShopItem item in items)
        {
            item.OnItemSold += OnItemSold;
        }
    }

    protected void OnDisable()
    {
        PauseMenu.OnPause -= OnPause;

        foreach (ShopItem item in items)
        {
            item.OnItemSold -= OnItemSold;
        }
    }

    private void OnPause()
    {
        if (opened)
        {
            opened = false;
            panel.SetActive(false);
        }
    }

    protected void Start()
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

    protected abstract void OnItemSold(ShopItem item);

    public bool IsOpened()
    {
        return opened;
    }


    public void ToggleAccess(bool toggle)
    {
        canAccessShop = toggle;
    }
}