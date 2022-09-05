using Infrastructure.StaticData.Cards;
using Infrastructure.StaticData.Enemies;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class CardEditor : OdinMenuEditorWindow
    {
        private readonly SimpleTreeMenuEditor<CardStaticData> _simpleEditor;
      
        [MenuItem("StaticData/Card Editor")]
        private static void ShowWindow()
        {
            var window = GetWindow<CardEditor>();
            window.titleContent = new GUIContent("Card Editor");
            window.Show();
        }

        public CardEditor() => 
            _simpleEditor = new SimpleTreeMenuEditor<CardStaticData>("Resources/Static Data/Cards", this);

        protected override OdinMenuTree BuildMenuTree() => _simpleEditor.BuildMenuTree(header: "Cards");
    
        protected override void OnBeginDrawEditors() => 
            _simpleEditor.OnBeginDrawEditors(MenuTree);
    }
}