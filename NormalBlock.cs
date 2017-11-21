// ノーマルブロッククラス

using UnityEngine;

public class NormalBlock : BaseBlock
{

    void Start()
    {
    }

    void Update()
    {
        Move();
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, TO_DEATH_TIME);
    }
}
