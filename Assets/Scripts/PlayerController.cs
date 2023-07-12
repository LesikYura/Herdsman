using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using View;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    
    private Vector3 _targetPosition;
    private bool _isMoving = false;
    private List<EnemyView> _collectedEnemies = new List<EnemyView>();
    
    private readonly float _moveSpeed = 2f;
    private readonly int _maxCollectEnemyCount = 5;

    public void SetData()
    {
        transform.localPosition = Vector3.zero;
    }

    public void Move(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
        _isMoving = true;
    }

    public void EnemyDelivered(EnemyView view)
    {
        if (_collectedEnemies.Contains(view))
        {
            _collectedEnemies.Remove(view);
            UpdateCollectedEnemies();

            Debug.Log($"YardManager view #{view.Index}");
        }
    }

    private void UpdateCollectedEnemies()
    {
        for (int i = _collectedEnemies.Count - 1; i >= 0; i--)
        {
            var enemy = _collectedEnemies[i];
            if (enemy != null)
            {
                enemy.Move(i == 0 ? transform : _collectedEnemies[i].transform);
            }
        }
    }

    private void Update()
    {
        if (_isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
            {
                _isMoving = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D colider)
    {
        if (_collectedEnemies.Count < _maxCollectEnemyCount && colider.gameObject.CompareTag("Enemy"))
        {
            var view = colider.gameObject.GetComponent<EnemyView>();
            if (view != null && view.State != EnemyState.Delivered)
            {
                if (!_collectedEnemies.Contains(view))
                {
                    view.Move(_collectedEnemies.Count <= 0 ? transform : _collectedEnemies[^1].transform);
                    _collectedEnemies.Add(view);
                }
            }
        }
    }
}
