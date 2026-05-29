using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace jcsilva.SaveManager {
    public class SaveController {


        public void HandleSave(string saveName, List<SO_SaveManager> lstData) {
            Debug.Log(Application.persistentDataPath);
            foreach (SO_SaveManager s in lstData) {
                if (s.isBinary) {
                    HandlePrivateSave(saveName, s);
                } else {
                    HandlePublicSave(saveName, s);
                }
            }
        }

        public bool HandlePublicSave(string saveName, SO_SaveManager data) {
            try {
                // Convert incoming data into JSON
                string dataJson = JsonUtility.ToJson(data);

                // Save Data
                string savePath = Application.persistentDataPath + '/' + saveName + ".json";

                if (File.Exists(savePath)) File.Delete(savePath);

                File.WriteAllText(savePath, dataJson);

                return true;
            } catch (Exception ex) {
                Debug.LogError("An error has occurred: " + ex.Message);
                return false;
            }
        }

        public bool HandlePrivateSave(string saveName, SO_SaveManager dataToBeSaved) {
            try {
                string savePath = Application.persistentDataPath + '/' + saveName + ".data";
                BinaryFormatter bf = new BinaryFormatter();


                if (File.Exists(savePath)) {
                    File.Delete(savePath);
                }

                FileStream fs = File.Open(savePath, FileMode.OpenOrCreate);
                bf.Serialize(fs, dataToBeSaved);

                fs.Close();
                return true;
        } catch (Exception ex) {
                Debug.LogError("An error has occurred: " + ex.Message);
                return false;
        }
    }

        public SO_SaveManager HandleLoad() {
            return null;
        }


        public void GetSavedData() {
            string saveDataPath = Application.persistentDataPath;

            if (File.Exists(saveDataPath)) {
                foreach (string file in Directory.GetFiles(saveDataPath)) {
                    Debug.Log(file);
                }
            }

            //List<string> lstFiles = Directory.GetFiles(saveDataPath).ToList<string>();
        }

        /*public bool CheckSaveData() {
            string savePath = SaveManager.GetSavePath();
        }*/
    }
}