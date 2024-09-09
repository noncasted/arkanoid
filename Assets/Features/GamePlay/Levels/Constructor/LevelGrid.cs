using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Features.GamePlay
{
    [DisallowMultipleComponent]
    public class LevelGrid : MonoBehaviour
    {
        [SerializeField] private LevelBlockPoint _blockPointPrefab;

        [SerializeField] private Transform _from;
        [SerializeField] private Transform _to;

        [SerializeField] [Min(0f)] private float _distanceBetween;

        [Button]
        private void Construct()
        {
#if UNITY_EDITOR
            Clear();

            var from = _from.position;
            var to = _to.position;

            var horizontalDistance = Mathf.Abs(from.x - to.x);
            var verticalDistance = Mathf.Abs(from.y - to.y);

            var horizontalCount = Mathf.CeilToInt(horizontalDistance / _distanceBetween);
            var verticalCount = Mathf.CeilToInt(verticalDistance / _distanceBetween);


            for (var y = 0; y < verticalCount; y++)
            {
                for (var x = 0; x < horizontalCount; x++)
                {
                    var position = from + Vector3.right * _distanceBetween * x + Vector3.up * _distanceBetween * y;
                    var blockPoint =
                        (PrefabUtility.InstantiatePrefab(_blockPointPrefab, transform) as LevelBlockPoint)!;
                    blockPoint.transform.position = position;
                    blockPoint.name = $"BlockPoint_{x}_{y}";
                }
            }

            EditorUtility.SetDirty(this);
#endif
        }

        private void Clear()
        {
            var points = GetComponentsInChildren<LevelBlockPoint>(true);

            foreach (var area in points)
                DestroyImmediate(area.gameObject);
        }
    }
}