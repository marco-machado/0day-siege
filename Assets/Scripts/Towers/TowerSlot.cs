namespace ZeroDaySiege.Towers
{
    public class TowerSlot
    {
        public int Index { get; }
        public bool IsOccupied => Tower != null;
        public Tower Tower { get; private set; }
        public bool IsMiddleSlot => Index == MiddleSlotIndex;

        public const int MiddleSlotIndex = 2;
        public const int TotalSlots = 5;

        public TowerSlot(int index)
        {
            Index = index;
            Tower = null;
        }

        public void SetTower(Tower tower)
        {
            Tower = tower;
        }

        public void Clear()
        {
            Tower = null;
        }
    }
}
