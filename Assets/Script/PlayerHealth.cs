using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHp = 100;
    [SerializeField] private Slider healthBar;
    private int hp;

    void Start()
    {
        hp = maxHp;
        UpdateBar();
    }

    public void KenaDamage(int jumlah)
    {
        hp = Mathf.Max(0, hp - jumlah);
        UpdateBar();
        Debug.Log("Player kena damage: " + jumlah + ", HP: " + hp);
        if (hp <= 0) Mati();
    }

    void UpdateBar()
    {
        if (healthBar == null) return;
        healthBar.maxValue = maxHp;
        healthBar.value = hp;
    }

    void Mati()
    {
        Debug.Log("Player mati!");
        Time.timeScale = 0f; 
    }
}