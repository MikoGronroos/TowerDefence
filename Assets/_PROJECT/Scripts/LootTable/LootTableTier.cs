namespace Finark.LootTable
{
    [System.Serializable]
    public class LootTableTier
    {

        public LootTableTier(string name, int weight)
        {
            Name = name;
            Weight = weight;
        }

        public string Name;
        public int Weight;

        public LootTableTierReward LootTableTierReward;

    }
}