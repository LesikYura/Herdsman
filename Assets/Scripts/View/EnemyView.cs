using UnityEngine;

namespace View
{
    public class EnemyView : MonoBehaviour
    {
        /// <summary>
        /// Enemy view
        /// Follow the target 
        /// </summary>

        public EnemyState State { get; private set; }

        [SerializeField] private int _index;

        private CircleCollider2D _collider;
        private Transform _target;
        private readonly float _moveSpeed = 2f;

        public int Index => _index;
        
        public void SetData(int index, Vector2 startPosition)
        {
            _index = index;
            transform.localPosition = startPosition;
            State = EnemyState.Ready;
        }

        public void Move(Transform targetPosition)
        {
            _target = targetPosition;
            State = EnemyState.Move;
        }

        public void SetAsDelivered(Transform targetPosition)
        {
            _target = targetPosition;
            State = EnemyState.Delivered;
        }
        
        private void Update()
        {
            if (_target != null)
            {
                var offset = transform.position - _target.position;
                var distance = offset.magnitude;
                if (distance > 1)
                {
                    transform.position = Vector3.Lerp(transform.position, _target.position, _moveSpeed * Time.deltaTime);
                }
            }
        }
    }

    public enum EnemyState
    {
        Ready = 0,
        Move,
        Delivered
    }
}
