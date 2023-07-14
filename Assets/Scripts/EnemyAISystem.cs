using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using View;

public class EnemyAISystem
{
    /// <summary>
    /// Path search for enemy
    /// Move random items to random position
    /// </summary>
    
    private List<EnemyView> _enemyViews = new List<EnemyView>();
    private Bounds _bounds;

    private readonly float _minDelay = 1f;
    private readonly float _maxDelay = 3f;
    private readonly float _maxStep = 100f;
    private Coroutine _moveRandomItems;

    public void SetData(List<EnemyView> enemyViews, Bounds bounds)
    {
        _enemyViews = enemyViews;
        _bounds = bounds;

        MoveRandomItemsHelper();
    }

    private void MoveRandomItemsHelper()
    {
        if (_moveRandomItems != null)
        {
            Global.Instance.StopCoroutine(_moveRandomItems);
            _moveRandomItems = null;
        }

        _moveRandomItems = Global.Instance.StartCoroutine(MoveRandomItems());
    }

    private IEnumerator MoveRandomItems()
    {
        while (true)
        {
            var itemCount = Random.Range(1, _enemyViews.Count + 1);
            for (var i = 0; i < itemCount; i++)
            {
                var view = _enemyViews[Random.Range(0, _enemyViews.Count)];
                MoveToRandomPosition(view);
            }
            
            var delay = Random.Range(_minDelay, _maxDelay);
            yield return new WaitForSeconds(delay);
        }
    }

    private void MoveToRandomPosition(EnemyView view)
    {
        var localPosition = view.transform.localPosition;
        var randomX = Random.Range(localPosition.x - _maxStep, localPosition.x + _maxStep);
        var randomY = Random.Range(localPosition.y - _maxStep, localPosition.y + _maxStep);
        
        var clampedX = Mathf.Clamp(randomX, _bounds.min.x, _bounds.max.x);
        var clampedY = Mathf.Clamp(randomY, _bounds.min.y, _bounds.max.y);
        
        var randomPosition = new Vector3(clampedX, clampedY, localPosition.z);
        view.SetMoveData(randomPosition);
    }
}
