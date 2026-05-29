using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jcsilva.SaveManager {
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Save System/Create Game Settings")]
    [System.Serializable]
    public class SO_GameSettings : SO_SaveManager {
        [Header("Graphic Settings")]
        public int resolution;
        public int displayMode;
        public int quality;

        [Header("Audio Settings")]
        [Range(0f, 100f)]
        public float masterVolume; 
        [Range(0f, 100f)]
        public float musicVolume; 
        [Range(0f, 100f)]
        public float sfxVolume;

        [Header("Input Settings")]
        public KeyCode walkFoward;
        public KeyCode walkBackwards;
        public KeyCode walkLeft;
        public KeyCode walkRight;
        public KeyCode crouch;
        public KeyCode jump;
        public KeyCode sprint;
        public KeyCode interaction;
        public KeyCode escape;
        public KeyCode primaryAttack;
        public KeyCode secondaryAttack;

        [Header("Mouse Settings")]
        public float mouseSensitivity;
        public bool invertMouse;
    }
}


