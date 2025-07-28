namespace AutoBattle
{
    public class BattleDamage
    {
        public readonly Unit Owner;
        public readonly int Value;
        
        public BattleDamage(Unit owner, int value)
        {
            Owner = owner;
            Value = value;
        }
    }
}