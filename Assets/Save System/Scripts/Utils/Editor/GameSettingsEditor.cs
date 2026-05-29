using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



namespace jcsilva.SaveManager {
    [CustomEditor(typeof(SO_GameSettings))]
    public class GameSettingsEditor : Editor {
    
        public enum DeviceSettings {
            PC,
            Mobile
        }

        public DeviceSettings deviceSettings;

        public override void OnInspectorGUI() {

            EditorGUILayout.PropertyField(serializedObject.FindProperty("isBinary"));

            EditorGUILayout.Space();

            deviceSettings = (DeviceSettings)EditorGUILayout.EnumPopup("Device", deviceSettings);

            EditorGUILayout.Space();

            switch(deviceSettings) {
                case DeviceSettings.PC:
                    DisplayGraphicSettingsPC();
                    DisplayAudioSettings();
                    DisplayInputSettingsPC();
                    DisplayMouseSettings();
                    break;
                case DeviceSettings.Mobile:
                    DisplayGraphicSettingsMobile();
                    DisplayAudioSettings();
                    break;

            }

            serializedObject.ApplyModifiedProperties();
        }

        void DisplayGraphicSettingsPC() {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("resolution"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("displayMode"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("quality"));
        }        
        void DisplayGraphicSettingsMobile() {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("quality"));
        }

        void DisplayAudioSettings() {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("masterVolume"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("musicVolume"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("sfxVolume"));
        }

        void DisplayInputSettingsPC() {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("walkFoward"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("walkBackwards"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("walkLeft"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("walkRight"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("crouch"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("jump"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprint"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("interaction"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("escape"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("primaryAttack"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("secondaryAttack"));
            
        }

        void DisplayMouseSettings() {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("mouseSensitivity"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("invertMouse"));
        }

    }
}