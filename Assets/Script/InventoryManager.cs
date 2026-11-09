using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject InventoryCanvas;

    private InputAction inventoryAction;
    private bool inventoryActive;

    void Start()
    {
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
}