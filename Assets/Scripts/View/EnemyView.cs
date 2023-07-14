using System;
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

        private Action _onComplete;
        private Vector3 _previousPosition;
        private readonly float _distance = 0.5f;

        public void SetData(Vector2 startPosition)
        {
            transform.localPosition = startPosition;
            State = EnemyState.Ready;
        }

        public override void SetMoveData(Transform targetTransform)
        {
            base.SetMoveData(targetTransform);
            State = EnemyState.Move;
        }

        public void SetAsDelivered(Transform targetTransform, Action onComplete)
        {
            TargetTransform = targetTransform;
            State = EnemyState.MoveToSafeArea;
            _onComplete = onComplete;
        }

        protected override void Move()
        {
            switch (State)
            {
                case EnemyState.Ready when TargetTransform == null:
                    BaseMove();
                    break;
                
                case EnemyState.Move:
                case EnemyState.MoveToSafeArea:
                {
                    Follow();
                    break;
                }
                
                case EnemyState.Delivered:
                    break;
                default:
                    break;
            }
        }

        private void Follow()
        {
            var offset = transform.position - TargetTransform.position;
            var distance = offset.magnitude;
            if (distance > _distance)
            {
                transform.position = Vector3.Lerp(
                    transform.position,
                    TargetTransform.position,
                    MoveSpeed * Time.deltaTime);
                
                icon.transform.localScale = new Vector3(offset.x < 0 ? -1 : 1, 1, 1);
            }
            else
            {
                if (State == EnemyState.MoveToSafeArea)
                {
                    _onComplete?.Invoke();
                    State = EnemyState.Delivered;
                }
            }
        }

        private void BaseMove()
        {
            if (IsBaseMoving)
            {
                var offset = transform.localPosition - TargetPosition;
                icon.transform.localScale = new Vector3(offset.x < 0 ? -1 : 1, 1, 1);
                
                transform.localPosition = Vector3.Lerp(transform.localPosition, TargetPosition, MoveSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.localPosition, TargetPosition) < 0.1f)
                {
                    IsBaseMoving = false;
                }
            }
        }
    }
}
