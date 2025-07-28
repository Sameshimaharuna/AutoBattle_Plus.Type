using System;
using UnityEngine;

namespace AutoBattle
{
    /** 所属 */
    public enum Belonging
    {
        Ally,
        Enemy
    }

    /** 追加：所属 */
    public enum Type
    {
        Fire,
        Water,
        Grass
    }
    
    public class Unit
    {
        /** 所属 */
        public readonly Belonging Belonging;
        /** インデックス */
        public readonly int Index;
        /** 名前 */
        public readonly string Name;
        /** 最大HP */
        public int MaxHp { get; private set; }
        /** 追加：属性 */
        public readonly Type Type;
        /** 現在HP */
        public int Hp { get; private set; }
        /** ATK */
        public int Atk { get; private set; }

        public Unit(Belonging belonging, int index, string name, int maxHp, Type type, int atk)
        {
            Belonging = belonging;
            Index = index;
            Name = name;
            MaxHp = maxHp;
            // 追加：属性
            Type = type;
            Hp = MaxHp;
            Atk = atk;
        }

        /** ダメージ適用 */
        public int ApplyDamage(BattleDamage damage)
        {
            Debug.Log($"{damage.Owner.Name} から {Name} に {damage.Value}のダメージ");
            Hp = Mathf.Max(0, Hp - damage.Value);
            return Hp;
        }

        /** 死亡しているか */
        public bool IsDead()
        {
            return Hp < 1;
        }
    }
}