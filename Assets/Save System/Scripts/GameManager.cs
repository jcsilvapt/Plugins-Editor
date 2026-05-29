using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jcsilva.SaveManager {
    public class GameManager : MonoBehaviour {

        // Singleton
        public static GameManager instance;

        // Inspector Settings
        [SerializeField] SO_GameSettings gameSettings;
        [SerializeField] SO_PlayerData playerData;

        private void Awake() {
            if (instance == null) {
                instance = this;
                DontDestroyOnLoad(this);
            } else {
                Destroy(gameObject);
            }
        }

        // Start is called before the first frame update
        void Start() {
            SaveManager.EventSave?.Invoke();
        }

        // Update is called once per frame
        void Update() {

        }
    }
}


