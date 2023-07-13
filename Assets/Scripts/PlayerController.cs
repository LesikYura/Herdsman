using System.Collections.Generic;
using Data;
using UnityEngine;
using View;

public class PlayerController : Movable
{
    /// <summary>
    /// 
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
                    Debug.Log($"Connect # {view.Index}");
                }
            }
        }
    }

}
