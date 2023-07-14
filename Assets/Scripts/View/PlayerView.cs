using System.Collections.Generic;
using Data;
using UnityEngine;

namespace View
{
    public class PlayerView : Movable
    {
        /// <summary>
        /// Move player
        /// Collect enemy
        /// </summary>
    
        private List<EnemyView> _collectedEnemies = new List<EnemyView>();
        private int _maxCollectEnemyCount;

        public void SetData(int maxCollectEnemyCount)
        {
            _maxCollectEnemyCount = maxCollectEnemyCount;
            transform.localPosition = Vector3.zero;
        }

        public void EnemyDelivered(EnemyView view)
        {
            if (_collectedEnemies.Contains(view))
            {
                _collectedEnemies.Remove(view);
            }
        }

        private void OnTriggerEnter2D(Collider2D colider)
        {
            if (_collectedEnemies.Count < _maxCollectEnemyCount && colider.gameObject.CompareTag("Enemy"))
            {
                var view = colider.gameObject.GetComponent<EnemyView>();
                if (view != null && view.State != EnemyState.Delivered && view.State != EnemyState.MoveToSafeArea)
                {
                    if (!_collectedEnemies.Contains(view))
                    {
                        view.SetMoveData(_collectedEnemies.Count <= 0 ? transform : _collectedEnemies[^1].transform);
                        _collectedEnemies.Add(view);
                    }
                }
            }
        }

        protected override void Move()
        {
            if (IsBaseMoving)
            {
                var offset = transform.position - TargetPosition;
                icon.transform.localScale = new Vector3(offset.x < 0 ? 1 : -1, 1, 1);
            
                transform.position = Vector3.Lerp(transform.position, TargetPosition, MoveSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, TargetPosition) < 0.1f)
                {
                    IsBaseMoving = false;
                }
            }
        }
    }
}
