using System;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class YardManager : MonoBehaviour
    {
        public Action<EnemyView> OnTriggerEnter;

        [SerializeField] private Transform _deliveredRoot;
        private List<EnemyView> _deliveredEnemies = new List<EnemyView>();

        public void SetData(Action<EnemyView> onTriggerEnter)
        {
            OnTriggerEnter = onTriggerEnter;
        }
        
        private void OnTriggerEnter2D(Collider2D colider)
        {
            if (colider.gameObject.CompareTag("Enemy"))
            {
                var view = colider.gameObject.GetComponent<EnemyView>();
                if (view != null)
                {
                    _deliveredEnemies.Add(view);
                    // view.Move(_deliveredEnemies.Count <= 0 ? _deliveredRoot : _deliveredEnemies[^1].transform);
                    view.SetAsDelivered(_deliveredRoot);
                    OnTriggerEnter?.Invoke(view);
                    Debug.Log($"YardManager view #{view.Index}");
                }
            }
            
            Debug.Log($"OnTriggerEnter2D {colider.gameObject.name}");
        }
    }
}
