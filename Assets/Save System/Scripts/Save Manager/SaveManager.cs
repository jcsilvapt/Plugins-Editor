using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace jcsilva.SaveManager {
    public class SaveManager : MonoBehaviour {

        // Singleton
        public static SaveManager ins;

        // Event Quick Save
        public static Action EventSave;

        [Header("Save Manager Settings")]
        [TextArea(1, 2)]
        [SerializeField] string savePath;
        [SerializeField] List<SO_SaveManager> data;
        [SerializeField] SO_SaveManager playerData;
        [SerializeField] SO_SaveManager gameSettings;
        [SerializeField] SO_SaveManager gameData;

        

        // Scriptable Objects Settings
        private SaveController saveController;

        private void OnEnable() {
            EventSave += HandleAutomaticSave;
        }

        private void OnDisable() {
            EventSave -= HandleAutomaticSave;
        }

        private void Awake() {
            if (ins == null) {
                ins = this;
                DontDestroyOnLoad(this);
                saveController = new SaveController();
            } else {
                Destroy(gameObject);
            }
        }

        // Start is called before the first frame update
        void Start() {
            this.savePath = Application.dataPath;
        }

        // Update is called once per frame
        void Update() {

        }

        public void HandleAutomaticSave() {
            saveController.HandleSave("autoSave", data);
        }


        public void Load() {
            saveController.HandleLoad();
        }

        public static string GetSavePath() {
            if (ins != null) {
                return Application.dataPath;
            }
            return null;
        }
    }
}