using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace CoreSystem
{
    public class EnemyAI
    {
        /// <summary>
        /// Path search for enemy
        /// Move random items to random position
        /// </summary>
    
        private List<EnemyView> _enemyViews = new List<EnemyView>();
        private Bounds _bounds;
        private Coroutine _moveRandomItems;
        private GamePlayConfig _config;

        public void SetData(List<EnemyView> enemyViews, Bounds bounds, GamePlayConfig config)
        {
            _enemyViews = enemyViews;
            _bounds = bounds;
            _config = config;

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
                
                yield return new WaitForSeconds(Random.Range(_config.minDelay, _config.maxDelay));
            }
        }

        private void MoveToRandomPosition(EnemyView view)
        {
            var maxStep = _config.maxStep;
            var localPosition = view.transform.localPosition;
            var randomX = Random.Range(localPosition.x - maxStep, localPosition.x + maxStep);
            var randomY = Random.Range(localPosition.y - maxStep, localPosition.y + maxStep);
        
            var clampedX = Mathf.Clamp(randomX, _bounds.min.x, _bounds.max.x);
            var clampedY = Mathf.Clamp(randomY, _bounds.min.y, _bounds.max.y);
        
            var randomPosition = new Vector3(clampedX, clampedY, localPosition.z);
            view.SetMoveData(randomPosition);
        }
    }
}
