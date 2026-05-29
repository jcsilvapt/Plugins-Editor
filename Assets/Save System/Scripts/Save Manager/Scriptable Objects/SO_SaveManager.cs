using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jcsilva.SaveManager {
    public abstract class SO_SaveManager : ScriptableObject {
        [Tooltip("When Checked, this object will be converted to binary.")]
        public bool isBinary;

        public void HandleSave() {
            // TBD
        }
    }
}
