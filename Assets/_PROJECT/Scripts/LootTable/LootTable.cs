using UnityEngine;

namespace Finark.LootTable
{
    [CreateAssetMenu(menuName = "Loot Table", fileName = "New Loot Table")]
    public class LootTable : ScriptableObject
    {

        [SerializeField]
        private LootTableTier[] lootTableItems = { new LootTableTier("Uncommon", 60),
        new LootTableTier("Common", 30),
        new LootTableTier("Rare", 5),
        new LootTableTier("Epic", 3),
        new LootTableTier("Legendary", 2)
    };

        [Header("Debug")]

        [SerializeField] private bool useForcedWeight;
        [SerializeField] private int forcedWeight;

        [Header("Generated")]

        [SerializeField] private int generatedWeight;
        [SerializeField] private string generatedTier;

        public LootTableTierReward GetLootTableTier()
        {

            int totalWeights = 0;
            int randomNumber = 0;

            foreach (var item in lootTableItems)
            {
                totalWeights += item.Weight;
            }

            if (useForcedWeight)
            {
                randomNumber = forcedWeight;
            }
            else
            {
                randomNumber = Random.Range(0, totalWeights);
            }
            generatedWeight = randomNumber;

            foreach (var tier in lootTableItems)
            {
                if (randomNumber <= tier.Weight)
                {

                    generatedTier = tier.Name;

                    return tier.LootTableTierReward;

                }
                else
                {
                    randomNumber -= tier.Weight;
                }
            }
            return null;
        }
    }
}