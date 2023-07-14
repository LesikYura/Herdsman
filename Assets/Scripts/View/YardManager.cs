using System;
using System.Collections.Generic;
using CoreSystem;
using UnityEngine;

namespace View
{
    public class YardManager : MonoBehaviour
    {
        /// <summary>
        /// FieldController helper
        /// Сhecks the win condition
        /// </summary>
        
        public Action<EnemyView> OnTriggerEnter;

        [SerializeField] private Transform _deliveredRoot;
        private List<EnemyView> _deliveredEnemies = new List<EnemyView>();
        private ObjectPool _objectPool;

        public void SetData(Action<EnemyView> onTriggerEnter, ObjectPool objectPool)
        {
            OnTriggerEnter = onTriggerEnter;
            _objectPool = objectPool;
        }
        
        public void CheckCompleteLevel(ObjectPool objectPool)
        {
            foreach (var view in _deliveredEnemies)
            {
                objectPool.ReturnObjectToPool(view.gameObject);
            }
        
            _deliveredEnemies.Clear();
        }
        
        private void OnTriggerEnter2D(Collider2D colider)
        {
            if (colider.gameObject.CompareTag("Enemy"))
            {
                var view = colider.gameObject.GetComponent<EnemyView>();
                if (view != null)
                {
                    _deliveredEnemies.Add(view);
                    view.SetAsDelivered(_deliveredRoot, () =>
                    {
                        _deliveredEnemies.Remove(view);
                        _objectPool.ReturnObjectToPool(view.gameObject);
                    });
                    
                    OnTriggerEnter?.Invoke(view);
                }
            }
        }
    }
}
