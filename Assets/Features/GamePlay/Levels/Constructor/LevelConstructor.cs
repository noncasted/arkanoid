using Features.GamePlay.Levels.Platforms;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.GamePlay
{
    [DisallowMultipleComponent]
    public class LevelConstructor : MonoBehaviour
    {
        [SerializeField] private LevelConstructorOptions _options;

        [Button("Construct")]
        [ContextMenu("Construct")]
        private void Construct()
        {
            Clear();
            var blockPoints = GetComponentsInChildren<LevelBlockPoint>(true);

            foreach (var blockPoint in blockPoints)
            {
#if UNITY_EDITOR
                var block = UnityEditor.PrefabUtility.InstantiatePrefab(_options.BlockPrefab, transform) as Block;
                block.transform.position = blockPoint.transform.position;
#endif
            }
        }

        [Button("Clear")]
        [ContextMenu("Clear")]
        private void Clear()
        {
            var blocks = GetComponentsInChildren<Block>(true);

            foreach (var block in blocks)
                DestroyImmediate(block.gameObject);
        }
    }
}