using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool requesetDestroy { get; set; } = false;

    private Matrix4x4 matrix = Matrix4x4.identity;

    private float speed_;
}
