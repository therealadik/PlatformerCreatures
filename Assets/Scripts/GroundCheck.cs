using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GroundCheck : MonoBehaviour
{
    public bool isGround;

    [SerializeField] private LayerMask _groundLayerMask;

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        isGround = _collider.IsTouchingLayers(_groundLayerMask);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isGround = _collider.IsTouchingLayers(_groundLayerMask);
    }
}
