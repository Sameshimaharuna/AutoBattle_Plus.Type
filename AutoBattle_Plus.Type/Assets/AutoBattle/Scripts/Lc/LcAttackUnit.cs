using System.Collections.Generic;
using AutoBattle.Vc;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AutoBattle.Lc
{
    public class LcAttackUnit : LogicCommand
    {
        /** 攻撃主 */
        private readonly Unit _owner;
        /** 標的リスト */
        private readonly List<Unit> _targets;
        
        public LcAttackUnit(Unit owner, List<Unit> targets)
        {
            _owner = owner;
            _targets = targets;
        }
        
        protected override async UniTask Execute()
        {
            // 死亡済み
            if (_owner.IsDead()) return;
            
            // ターゲットがいない
            var aliveTargets = _targets.FindAll(target => !target.IsDead());
            if (aliveTargets.Count == 0) return;
            
            // ランダムにターゲットを選択
            var target = aliveTargets[Random.Range(0, aliveTargets.Count)];
            
            // ターゲットにダメージを与える
            var battleDamage = new BattleDamage(_owner, _owner.Atk);
            var nextHp = target.ApplyDamage(battleDamage);
            
            // 攻撃アニメーション発行
            new VcAttackUnit(_owner, target, nextHp).AddToQueue();
            
            await UniTask.CompletedTask;
        }
    }
}