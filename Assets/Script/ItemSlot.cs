using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int maxStackSize = 99;
    [SerializeField] private Color selectedColor = new Color(1f, 0.92f, 0.45f, 1f);

    private Image background;
    private Image iconImage;
    private TMP_Text quantityText;
    private Color normalColor;
    private InventoryManager owner;
    private bool initialized;

    public ItemDetails Details { get; private set; }
    public int Quantity { get; private set; }
    public bool IsEmpty => Details == null;

    // Dipanggil oleh InventoryManager (bukan Awake), supaya slot tetap siap
    // walaupun InventoryCanvas sedang nonaktif.
    public void Init(InventoryManager manager)
    {
        if (initialized) return;
        initialized = true;
        owner = manager;

        background = GetComponent<Image>();
        if (background != null) normalColor = background.color;

        iconImage = CreateIcon();
        quantityText = CreateQuantityText();
        Refresh();
    }

    public bool CanStackWith(ItemDetails item)
    {
        return Details == item && Quantity < maxStackSize;
    }

    public void Add(ItemDetails item, int amount)
    {
        Details = item;
        Quantity = Mathf.Min(Quantity + amount, maxStackSize);
        Refresh();
    }

    public void SetSelected(bool selected)
    {
        if (background != null)
        {
            background.color = selected ? selectedColor : normalColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && owner != null)
        {
            owner.SelectSlot(this);
        }
    }

    void Refresh()
    {
        if (IsEmpty)
        {
            iconImage.enabled = false;
            quantityText.text = "";
            return;
        }

        iconImage.sprite = Details.ItemIcon;
        iconImage.enabled = true;
        quantityText.text = Quantity > 1 ? Quantity.ToString() : "";
    }

    Image CreateIcon()
    {
        GameObject go = new GameObject("Icon", typeof(RectTransform), typeof(Image));
        go.transform.SetParent(transform, false);

        RectTransform rt = go.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = new Vector2(8f, 8f);
        rt.offsetMax = new Vector2(-8f, -8f);

        Image img = go.GetComponent<Image>();
        img.preserveAspect = true;
        img.raycastTarget = false; // supaya klik tetap diterima oleh slot
        return img;
    }

    TMP_Text CreateQuantityText()
    {
        GameObject go = new GameObject("Qty", typeof(RectTransform));
        go.transform.SetParent(transform, false);

        RectTransform rt = go.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = new Vector2(6f, 4f);
        rt.offsetMax = new Vector2(-6f, -4f);

        TextMeshProUGUI text = go.AddComponent<TextMeshProUGUI>();
        text.fontSize = 28f;
        text.alignment = TextAlignmentOptions.BottomRight;
        text.color = Color.white;
        text.raycastTarget = false;
        return text;
    }
}