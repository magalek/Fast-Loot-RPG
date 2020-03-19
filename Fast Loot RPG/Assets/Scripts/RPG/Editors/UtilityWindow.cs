using System;
using RPG.Controllers;
using UnityEditor;
using UnityEngine;

namespace RPG.Editors {
    public class UtilityWindow : EditorWindow {

        [MenuItem("Window/UtilityWindow")]
        public static void ShowWindow() {
            GetWindow<UtilityWindow>();
        }

        private void OnGUI() {
            if (GUILayout.Button("Generate Level")) {
                //LevelController.GenerateLevel();
                Debug.Log("Lolz");
            }
        }
    }
}