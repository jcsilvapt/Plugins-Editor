using UnityEngine;

namespace jcsilva.InspectionSystem {

    [CreateAssetMenu(fileName = "New Inspectable Data", menuName = "Inspection System/Inspectable Data")]
    public class InspectableData : ScriptableObject {

        [Header("Identity")]
        [SerializeField] private string objectName;
        [SerializeField] [TextArea(2, 4)] private string description;

        [Header("Clue")]
        [SerializeField] private bool hasClue;
        [SerializeField] [TextArea(2, 6)] private string clueText;

        public string ObjectName => objectName;
        public string Description => description;
        public bool HasClue => hasClue;
        public string ClueText => clueText;
    }
}
