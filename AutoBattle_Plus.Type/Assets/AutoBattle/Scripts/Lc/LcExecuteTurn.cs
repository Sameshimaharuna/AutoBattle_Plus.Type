using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AutoBattle.Lc
{
    public class LcExecuteTurn : LogicCommand
    {
        protected override UniTask Execute()
        {
            // 味方の攻撃 -> 敵の攻撃 を繰り返す
            
            var maxIndex = Mathf.Max(BattleManager.AllyList.Count, BattleManager.EnemyList.Count) - 1;
            
            for (var i = 0; i <= maxIndex; i++)
            {
                // 味方の攻撃
                if (i < BattleManager.AllyList.Count)
                {
                    new LcAttackUnit(BattleManager.AllyList[i], BattleManager.EnemyList).AddToQueue();
                }
                
                // 敵の攻撃
                if (i < BattleManager.EnemyList.Count)
                {
                    new LcAttackUnit(BattleManager.EnemyList[i], BattleManager.AllyList).AddToQueue();
                }
                
            }
            
            // ViewCommandの処理を待機
            new LcWaitView().AddToQueue();
            
            // 終了判定
            if (BattleManager.EnemyList.TrueForAll(unit => unit.IsDead()))
            {
                Debug.Log("味方の勝利！");
                return UniTask.CompletedTask;
            }
            if (BattleManager.AllyList.TrueForAll(unit => unit.IsDead()))
            {
                Debug.Log("敵の勝利！");
                return UniTask.CompletedTask;
            }
            
            // 次のターンへ
            BattleManager.ExecuteTurn();
            
            return UniTask.CompletedTask;
        }
    }
}