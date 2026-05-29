using TMPro;
using UnityEngine;

namespace jcsilva.InspectionSystem {

    public class InspectionUI : MonoBehaviour {

        [Header("Prompt")]
        [SerializeField] private GameObject promptPanel;
        [SerializeField] private TMP_Text promptText;

        [Header("Clue")]
        [SerializeField] private GameObject cluePanel;
        [SerializeField] private TMP_Text clueNameText;
        [SerializeField] private TMP_Text clueDescriptionText;
        [SerializeField] private TMP_Text clueText;

        private void Awake() {
            HidePrompt();
            HideClue();
        }

        // ── Prompt ─────────────────────────────────────────────────

        public void ShowPrompt(string objectName) {
            promptText.text = $"[E] Inspect {objectName}";
            promptPanel.SetActive(true);
        }

        public void HidePrompt() {
            promptPanel.SetActive(false);
        }

        // ── Clue ───────────────────────────────────────────────────

        public void ShowClue(InspectableData data) {
            clueNameText.text        = data.ObjectName;
            clueDescriptionText.text = data.Description;

            bool hasClue = data.HasClue;
            clueText.gameObject.SetActive(hasClue);
            if (hasClue) {
                clueText.text = data.ClueText;
            }

            cluePanel.SetActive(true);
        }

        public void HideClue() {
            cluePanel.SetActive(false);
        }
    }
}
