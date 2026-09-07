using UnityEngine;

public class Bee : Enemy
{
    public bool bee = true;

    public override void Serang()
    {
        Debug.Log("Bee menyengat");
    }
}
