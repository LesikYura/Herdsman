using UnityEngine;
using UnityEngine.UI;

public abstract class Movable : MonoBehaviour
{
    /// <summary>
    /// Class responsible for movement
    /// </summary>
    
    [SerializeField] protected Image icon;
    protected Transform TargetTransform;
    protected Vector3 TargetPosition;
    protected bool IsBaseMoving = false;
    
    protected readonly float MoveSpeed = 2f;
    
    private void Update()
    {
        Move();
    }
    public virtual void SetMoveData(Vector3 targetPosition)
    {
        TargetPosition = targetPosition;
        IsBaseMoving = true;
    }
    
    public virtual void SetMoveData(Transform targetTransform)
    {
        TargetTransform = targetTransform;
        IsBaseMoving = false;
    }

    protected abstract void Move();
}
