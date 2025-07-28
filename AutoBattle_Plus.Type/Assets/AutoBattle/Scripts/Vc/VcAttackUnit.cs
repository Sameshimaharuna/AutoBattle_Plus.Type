using Cysharp.Threading.Tasks;

namespace AutoBattle.Vc
{
    public class VcAttackUnit : ViewCommand
    {
        private readonly Unit _attacker;
        private readonly Unit _defender;
        private readonly int _nextHp;
        
        public VcAttackUnit(Unit attacker, Unit defender, int nextHp)
        {
            _attacker = attacker;
            _defender = defender;
            _nextHp = nextHp;
        }
        
        protected override async UniTask Execute()
        {
            var attackerView = BattleLayer.GetUnitView(_attacker);
            var defenderView = BattleLayer.GetUnitView(_defender);
            
            // アニメーション
            var attackMotion = attackerView.AttackMotionAsync();
            var damageMotion = defenderView.DamageMotionAsync(_nextHp);
            
            // アニメーションを並行で走らせて完了するまで待機
            await UniTask.WhenAll(attackMotion, damageMotion);
        }
    }
}