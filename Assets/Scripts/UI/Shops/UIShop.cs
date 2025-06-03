using UnityEngine;

public abstract class UIShop : MonoBehaviour
{
    [SerializeField] GameObject panel;

    bool opened = false;

    protected ShopItem[] shopItems;

    private void Awake()
    {
        shopItems = GetComponentsInChildren<ShopItem>(true);
        Initialize();
    }

    protected abstract void Initialize();
    protected abstract void OnItemSold(ShopItem item);

    private void OnEnable()
    {
        PauseMenu.OnPause += OnPause;

        foreach (ShopItem item in shopItems)
        {
            item.OnItemSold += OnItemSold;
        }
    }

    private void OnDisable()
    {
        PauseMenu.OnPause -= OnPause;

        foreach (ShopItem item in shopItems)
        {
            item.OnItemSold -= OnItemSold;
        }
    }

    public virtual void Open()
    {
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

    private void OnPause()
    {
        if (opened)
        {
            opened = false;
            panel.SetActive(false);
        }
    }

    public bool IsOpen()
    {
        return opened;
    }

}