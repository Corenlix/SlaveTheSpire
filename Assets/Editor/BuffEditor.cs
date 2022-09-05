using Infrastructure.StaticData.Buffs;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class BuffEditor : OdinMenuEditorWindow
    {
        private readonly SimpleTreeMenuEditor<BuffStaticData> _simpleEditor;
      
        [MenuItem("StaticData/Buff Editor")]
        private static void ShowWindow()
        {
            var window = GetWindow<BuffEditor>();
            window.titleContent = new GUIContent("Buff Editor");
            window.Show();
        }

        public BuffEditor() => 
            _simpleEditor = new SimpleTreeMenuEditor<BuffStaticData>("Resources/Static Data/Buffs", this);

        protected override OdinMenuTree BuildMenuTree() => _simpleEditor.BuildMenuTree(header: "Buffs");
    
        protected override void OnBeginDrawEditors() => 
            _simpleEditor.OnBeginDrawEditors(MenuTree);
    }
}