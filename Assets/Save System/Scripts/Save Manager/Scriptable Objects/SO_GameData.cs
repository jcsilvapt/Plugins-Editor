using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jcsilva.SaveManager {
    [CreateAssetMenu(fileName = "Game Data", menuName = "Save System/Create Game Data")]
    [System.Serializable]
    public class SO_GameData : SO_SaveManager {
        public Vector3 currentPosition;
        public Vector3 currentAngle;
    }
}