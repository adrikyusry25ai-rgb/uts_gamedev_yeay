using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour, GrabableItem
{
    [Header("Inventory")]
    [SerializeField] private GameObject InventoryCanvas;

    [Header("Panel Detail Item")]
    [SerializeField] private Image detailIcon;
    [SerializeField] private TMP_Text detailText;

    private InputAction inventoryAction;
    private bool inventoryActive;
    private ItemSlot[] slots;
    private ItemSlot selectedSlot;

    void Start()
    {
        // Kumpulkan semua slot di dalam InventoryCanvas (termasuk yang nonaktif)
        if (InventoryCanvas != null)
        {
            slots = InventoryCanvas.GetComponentsInChildren<ItemSlot>(true);
            foreach (ItemSlot slot in slots)
            {
                slot.Init(this);
            }
        }
        else
        {
            slots = new ItemSlot[0];
        }

        ClearDetail();

        if (InputSystem.actions == null)
        {
            Debug.LogError("InputSystem.actions kosong! Assign asset di Project Settings > Input System Package.");
            return;
        }

        inventoryAction = InputSystem.actions.FindAction("Inventory");

        if (inventoryAction == null)
            Debug.LogError("Action 'Inventory' tidak ditemukan di Input Actions asset.");

        if (InventoryCanvas != null)
            InventoryCanvas.SetActive(false);

        inventoryActive = false;
    }

    void Update()
    {
        if (inventoryAction == null || InventoryCanvas == null) return;

        if (inventoryAction.triggered)
        {
            inventoryActive = !inventoryActive;
            InventoryCanvas.SetActive(inventoryActive);
            Time.timeScale = inventoryActive ? 0f : 1f;
            Debug.Log(inventoryActive ? "Inventory opened" : "Inventory closed");
        }
    }

    // Dipanggil oleh Item saat player menyentuhnya
    public bool AddItem(ItemDetails item)
    {
        if (slots == null || slots.Length == 0)
        {
            Debug.LogWarning("Inventory tidak punya slot, '" + item.ItemName + "' tidak bisa disimpan.");
            return false;
        }

        // 1) Tumpuk ke slot yang sudah berisi item sama
        foreach (ItemSlot slot in slots)
        {
            if (slot.CanStackWith(item))
            {
                slot.Add(item, item.Quantity);
                if (slot == selectedSlot) ShowDetail(slot);
                Debug.Log("Item ditumpuk: " + item.ItemName);
                return true;
            }
        }

        // 2) Kalau tidak ada, pakai slot kosong pertama
        foreach (ItemSlot slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.Add(item, item.Quantity);
                Debug.Log("Item masuk inventory: " + item.ItemName);
                return true;
            }
        }

        Debug.LogWarning("Inventory penuh, '" + item.ItemName + "' tidak diambil.");
        return false;
    }

    // Dipanggil oleh ItemSlot saat diklik
    public void SelectSlot(ItemSlot slot)
    {
        if (selectedSlot != null) selectedSlot.SetSelected(false);

        selectedSlot = slot;
        selectedSlot.SetSelected(true);
        ShowDetail(slot);
    }

    void ShowDetail(ItemSlot slot)
    {
        if (slot.IsEmpty)
        {
            ClearDetail();
            return;
        }

        ItemDetails d = slot.Details;

        if (detailIcon != null)
        {
            detailIcon.sprite = d.ItemIcon;
            detailIcon.enabled = true;
        }

        if (detailText != null)
        {
            detailText.text = "<b>" + d.ItemName + "</b>\nJumlah: " + slot.Quantity + "\n\n" + d.Description;
        }
    }

    void ClearDetail()
    {
        if (detailIcon != null) detailIcon.enabled = false;
        if (detailText != null) detailText.text = "Pilih item untuk melihat detail.";
    }
}