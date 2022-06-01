using UnityEngine;
using UnityEditor;

namespace Finark.LootTable
{
    [CustomEditor(typeof(LootTable))]
    [CanEditMultipleObjects]
    public class LootTableEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LootTable lootTable = (LootTable)target;

            if (GUILayout.Button("Generate A Tier"))
            {
                lootTable.GetLootTableTier();
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}