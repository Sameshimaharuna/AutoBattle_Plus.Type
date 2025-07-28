using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace AutoBattle.Vc
{
    public class VcOnStageUnit : ViewCommand
    {
        private readonly List<Unit> _allyUnits;
        private readonly List<Unit> _enemyUnits;
        
        public VcOnStageUnit(List<Unit> allyUnits, List<Unit> enemyUnits)
        {
            _allyUnits = allyUnits;
            _enemyUnits = enemyUnits;
        }
        
        protected override async UniTask Execute()
        {
            // 味方ユニットをステージに配置
            foreach (var unit in _allyUnits)
            {
                BattleLayer.AddUnitView(unit, true);
            }
            
            // 敵ユニットをステージに配置
            foreach (var unit in _enemyUnits)
            {
                BattleLayer.AddUnitView(unit, false);
            }
            
            // 2秒待機
            await UniTask.Delay(2000);
        }
    }
}