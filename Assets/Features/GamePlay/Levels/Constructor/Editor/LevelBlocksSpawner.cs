using System;
using Features.GamePlay.Levels.Platforms;
using Internal;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Features.GamePlay.Levels.Constructor.Editor
{
    [CustomEditor(typeof(LevelConstructor))]
    public class LevelBlocksSpawner : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            var options = AssetsExtensions.Environment.GetAsset<LevelConstructorOptions>();
            var e = Event.current;

            if (e == null)
                return;

            if (!(e.type == EventType.MouseDown || e.type == EventType.Used || e.type == EventType.MouseDrag) ||
                e.button != 0)
                return;

            var levelBlockPoint = GetPoint();

            if (levelBlockPoint == null)
                return;

            var hitTransform = levelBlockPoint.gameObject.transform;
            var parent = hitTransform.parent.parent;

            var block = PrefabUtility.InstantiatePrefab(options.BlockPrefab, parent) as MonoBehaviour;
            block.transform.position = hitTransform.position;

            e.Use();

            LevelBlockPoint GetPoint()
            {
                var stage = PrefabStageUtility.GetCurrentPrefabStage().stageHandle;
                var points = stage.FindComponentsOfType<LevelBlockPoint>();
                var blocks = stage.FindComponentsOfType<Block>();

                Vector2 mousePosition = HandleUtility.GUIPointToWorldRay(e.mousePosition).origin;

                foreach (var checkBlock in blocks)
                {
                    var distance = Vector2.Distance(mousePosition, checkBlock.transform.position);

                    if (distance < options.OverlapDistance)
                        return null;
                }

                foreach (var point in points)
                {
                    var distance = Vector2.Distance(mousePosition, point.transform.position);

                    if (distance < options.OverlapDistance)
                        return point;
                }

                return null;
            }
        }
    }
}