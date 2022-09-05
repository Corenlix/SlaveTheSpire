using Infrastructure.StaticData.Enemies;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class EnemyEditor : OdinMenuEditorWindow
    {
        private readonly SimpleTreeMenuEditor<EnemyStaticData> _simpleEditor;
      
        [MenuItem("StaticData/Enemy Editor")]
        private static void ShowWindow()
        {
            var window = GetWindow<EnemyEditor>();
            window.titleContent = new GUIContent("Enemy Editor");
            window.Show();
        }

        public EnemyEditor() => 
            _simpleEditor = new SimpleTreeMenuEditor<EnemyStaticData>("Resources/Static Data/Enemies", this);

        protected override OdinMenuTree BuildMenuTree() => _simpleEditor.BuildMenuTree(header: "Enemies");
    
        protected override void OnBeginDrawEditors() => 
            _simpleEditor.OnBeginDrawEditors(MenuTree);
    }
}