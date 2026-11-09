public interface GrabableItem
{
    // true = item berhasil masuk inventory, false = inventory penuh
    bool AddItem(ItemDetails itemDetails);
}