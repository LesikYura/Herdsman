using UnityEngine;
using UnityEngine.UI;

public class Movable : MonoBehaviour
{
    [SerializeField] protected Image icon;
    protected Transform TargetTransform;
    
    private Vector3 _targetPosition;
    private bool _isMoving = false;
    
    protected readonly float MoveSpeed = 2f;
    public virtual void SetMoveData(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
        _isMoving = true;
    }
    
    public virtual void SetMoveData(Transform targetTransform)
    {
        TargetTransform = targetTransform;
    }

    protected virtual void Move()
    {
        if (_isMoving)
        {
            var offset = transform.position - _targetPosition;
            icon.transform.localScale = new Vector3(offset.x < 0 ? 1 : -1, 1, 1);
            
            transform.position = Vector3.Lerp(transform.position, _targetPosition, MoveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
            {
                _isMoving = false;
            }
        }
    }
    
    private void Update()
    {
        Move();
    }
}
