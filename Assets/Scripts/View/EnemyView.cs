using Data;
using UnityEngine;

namespace View
{
    public class EnemyView : Movable
    {
        /// <summary>
        /// Enemy view
        /// Follow the target 
        /// </summary>

        public EnemyState State { get; private set; }

        [SerializeField] private int _index;

        private CircleCollider2D _collider;

        private readonly float _distance = 0.5f;

        public int Index => _index;
        
        public void SetData(int index, Vector2 startPosition)
        {
            _index = index;
            transform.localPosition = startPosition;
            State = EnemyState.Ready;
        }

        public override void SetMoveData(Transform targetTransform)
        {
            base.SetMoveData(targetTransform);
            State = EnemyState.Move;
        }

        public void SetAsDelivered(Transform targetTransform)
        {
            TargetTransform = targetTransform;
            State = EnemyState.Delivered;
        }

        protected override void Move()
        {
            if (State != EnemyState.Ready)
            {
                var offset = transform.position - TargetTransform.position;
                var distance = offset.magnitude;
                if (distance > _distance)
                {
                    transform.position = Vector3.Lerp(transform.position, TargetTransform.position, MoveSpeed * Time.deltaTime);
                }
            }
        }
    }
}
