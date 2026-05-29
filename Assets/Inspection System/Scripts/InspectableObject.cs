using UnityEngine;

namespace jcsilva.InspectionSystem {

    public class InspectableObject : MonoBehaviour {

        [Header("Data")]
        [SerializeField] private InspectableData data;

        [Header("Inspection Settings")]
        [SerializeField] [Range(0.2f, 3f)] private float inspectDistance = 1f;

        // Pose is captured at runtime so it works regardless of
        // where the object is placed or moved before play.
        private Vector3 originalPosition;
        private Quaternion originalRotation;
        private bool poseStored;

        public InspectableData Data => data;
        public float InspectDistance => inspectDistance;

        /// <summary>
        /// Stores the current world pose so it can be restored after inspection.
        /// Called by InspectionSystem before moving the object.
        /// </summary>
        public void StorePose() {
            originalPosition = transform.position;
            originalRotation = transform.rotation;
            poseStored = true;
        }

        /// <summary>
        /// Returns the stored world pose, or the current pose as a fallback.
        /// </summary>
        public (Vector3 position, Quaternion rotation) GetStoredPose() {
            if (!poseStored) {
                return (transform.position, transform.rotation);
            }
            return (originalPosition, originalRotation);
        }
    }
}
