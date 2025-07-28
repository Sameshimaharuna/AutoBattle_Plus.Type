using AutoBattle.Vc;
using Cysharp.Threading.Tasks;

namespace AutoBattle.Lc
{
    public class LcStartBattle : LogicCommand
    {
        protected override UniTask Execute()
        {
            // 味方生成
            var warrior = new Unit(Belonging.Ally, 0, "Warrior", 100, Type.Fire, 20);
            var mage = new Unit(Belonging.Ally, 1, "Mage", 50, Type.Water, 40);
            var rogue = new Unit(Belonging.Ally, 2, "Rogue", 80, Type.Grass, 30);
            
            BattleManager.AddUnit(warrior, true);
            BattleManager.AddUnit(mage, true);
            BattleManager.AddUnit(rogue, true);
            
            // 敵生成
            var goblin = new Unit(Belonging.Enemy, 0, "Goblin", 60, Type.Fire, 15);
            var orc = new Unit(Belonging.Enemy, 1, "Orc", 120, Type.Water, 25);
            var dragon = new Unit(Belonging.Enemy, 2, "Dragon", 200, Type.Grass, 50);
            
            BattleManager.AddUnit(goblin, false);
            BattleManager.AddUnit(orc, false);
            BattleManager.AddUnit(dragon, false);
            
            new VcOnStageUnit(BattleManager.AllyList, BattleManager.EnemyList)
                .AddToQueue();
            
            return UniTask.CompletedTask;
        }
    }
}