using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemDetails itemDetails;

    private GrabableItem collector;

    void Start()
    {
        collector = FindAnyObjectByType<InventoryManager>();

        if (collector == null)
        {
            Debug.LogError("InventoryManager tidak ditemukan di scene.", this);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || itemDetails == null || collector == null) return;

        // Item hanya hilang dari dunia kalau benar-benar masuk inventory
        if (collector.AddItem(itemDetails))
        {
            Destroy(gameObject);
        }
    }
}