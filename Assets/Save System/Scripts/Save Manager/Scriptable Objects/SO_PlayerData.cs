using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jcsilva.SaveManager {
    [CreateAssetMenu(fileName = "Player Data", menuName = "Save System/Create Player Data")]
    [System.Serializable]
    public class SO_PlayerData : SO_SaveManager {

        public string name;

    }
}
