using System.Collections.Generic;
using System.Linq;
using AutoBattle.Vc;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AutoBattle.Lc
{
    public class LcAttackUnit : LogicCommand
    {
        /** 攻撃主 */
        private Unit _owner;
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

            // 追加：ターゲットから属性を取得
            var T_type = target.Type;

            // ターゲットにダメージを与える
            var battledamage = new BattleDamage(_owner, _owner.Atk);

            // 追加：ターゲットの属性相性
            if (_owner.Type == Type.Fire)
            {
                if (T_type == Type.Grass)
                {
                    battledamage = new BattleDamage(_owner, _owner.Atk * 2);
                }

                else if (T_type == Type.Water)
                {
                    battledamage = new BattleDamage(_owner, _owner.Atk / 2);
                }

                else if (T_type == Type.Fire)
                {
                    battledamage = new BattleDamage(_owner, _owner.Atk);
                }
            }

            else if (_owner.Type == Type.Water)
            {
                if (T_type == Type.Fire)
                {
                    battledamage = new BattleDamage(_owner, _owner.Atk * 2);
                }

                else if (T_type == Type.Grass)
                {
                    battledamage = new BattleDamage(_owner, _owner.Atk / 2);
                }

                else if (T_type == Type.Water)
                {
                    battledamage = new BattleDamage(_owner, _owner.Atk);
                }
            }

            else if (_owner.Type == Type.Grass)
            {
                if (T_type == Type.Water)
                {
                    battledamage = new BattleDamage(_owner, _owner.Atk * 2);
                }

                else if (T_type == Type.Fire)
                {
                    battledamage = new BattleDamage(_owner, _owner.Atk / 2);
                }

                else if (T_type == Type.Grass)
                {
                    battledamage = new BattleDamage(_owner, _owner.Atk);
                }
            }

            var nextHp = target.ApplyDamage(battledamage);
            
            // 攻撃アニメーション発行
            new VcAttackUnit(_owner, target, nextHp).AddToQueue();
            
            await UniTask.CompletedTask;
        }
    }
}