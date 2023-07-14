using System.Collections.Generic;
using View;

namespace CoreSystem
{
    public class EnemyMaker
    {
        private ObjectPool _objectPool;
        private FieldController _fieldController;
        public EnemyMaker(ObjectPool objectPool, FieldController fieldController)
        {
            _objectPool = objectPool;
            _fieldController = fieldController;
        }
        public void AddEnemies(List<EnemyView> enemies, int count)
        {
            for (var i = 0; i < count; i++)
            {
                var enemy = _objectPool.GetObjectFromPool();
                var enemyView = enemy.GetComponent<EnemyView>();
                enemyView.SetData(_fieldController.GetSpawnPosition());
            
                enemies.Add(enemyView);
            }
        }
    }
}
