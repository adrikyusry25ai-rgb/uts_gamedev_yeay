using UnityEngine;

// INHERITANCE + POLYMORPHISM
// Butterfly mewarisi Enemy, lalu meng-override Serang().
public class Butterfly : Enemy
{
    public bool butterfly = true;

    public override void Serang()
    {
        Debug.Log("Butterfly mencabik");
    }
}
