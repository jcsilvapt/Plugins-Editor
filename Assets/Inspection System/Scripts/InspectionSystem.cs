using UnityEngine;

namespace jcsilva.InspectionSystem {

    public class InspectionSystem : MonoBehaviour {

        // ── Inspector ──────────────────────────────────────────────
        [Header("References")]
        [SerializeField] private Camera playerCamera;
        [SerializeField] private InspectionUI ui;

        [Header("Detection")]
        [SerializeField] [Range(0.5f, 10f)] private float detectionRange = 3f;
        [SerializeField] private LayerMask inspectableLayer;

        [Header("Travel")]
        [SerializeField] [Range(1f, 20f)] private float travelSpeed = 8f;
        [SerializeField] private KeyCode inspectKey = KeyCode.E;

        [Header("Rotation")]
        [SerializeField] private float rotationSensitivity = 120f;

        // ── State ──────────────────────────────────────────────────
        private enum State { Idle, Traveling, Inspecting, Returning }
        private State currentState = State.Idle;

        private InspectableObject currentObject;
        private Vector3 targetPosition;
        private Quaternion targetRotation;

        // ── Unity lifecycle ────────────────────────────────────────
        private void Update() {
            switch (currentState) {
                case State.Idle:      TickIdle();      break;
                case State.Traveling: TickTraveling(); break;
                case State.Inspecting:                 break;
                case State.Returning: TickReturning(); break;
            }

            if (currentState == State.Inspecting && Input.GetKeyDown(inspectKey)) {
                Cancel();
            }
        }

        // ── State ticks ────────────────────────────────────────────
        private void TickIdle() {
            InspectableObject hit = Raycast();

            if (hit != null) {
                ui.ShowPrompt(hit.Data.ObjectName);

                if (Input.GetKeyDown(inspectKey)) {
                    BeginInspection(hit);
                }
            } else {
                ui.HidePrompt();
            }
        }

        private void TickTraveling() {
            currentObject.transform.position = Vector3.Lerp(
                currentObject.transform.position,
                targetPosition,
                Time.deltaTime * travelSpeed
            );

            // Switch to Inspecting once close enough
            if (Vector3.Distance(currentObject.transform.position, targetPosition) < 0.01f) {
                currentObject.transform.position = targetPosition;
                currentState = State.Inspecting;
                ui.ShowClue(currentObject.Data);
            }
        }

        private void TickReturning() {
            var (storedPos, storedRot) = currentObject.GetStoredPose();

            currentObject.transform.position = Vector3.Lerp(
                currentObject.transform.position,
                storedPos,
                Time.deltaTime * travelSpeed
            );

            currentObject.transform.rotation = Quaternion.Slerp(
                currentObject.transform.rotation,
                storedRot,
                Time.deltaTime * travelSpeed
            );

            if (Vector3.Distance(currentObject.transform.position, storedPos) < 0.01f) {
                currentObject.transform.position = storedPos;
                currentObject.transform.rotation = storedRot;
                currentObject = null;
                currentState = State.Idle;
            }
        }

        // ── Public API ─────────────────────────────────────────────

        /// <summary>
        /// Rotates the inspected object. Pass mouse delta (or any float pair)
        /// so both old and new input systems can drive this.
        /// </summary>
        /// <param name="horizontal">Horizontal axis value (mouse X delta).</param>
        /// <param name="vertical">Vertical axis value (mouse Y delta).</param>
        public void Rotate(float horizontal, float vertical) {
            if (currentState != State.Inspecting || currentObject == null) return;

            float rotX = vertical   * rotationSensitivity * Time.deltaTime;
            float rotY = horizontal * rotationSensitivity * Time.deltaTime;

            currentObject.transform.Rotate(playerCamera.transform.up,    -rotY, Space.World);
            currentObject.transform.Rotate(playerCamera.transform.right,   rotX, Space.World);
        }

        // ── Private helpers ────────────────────────────────────────
        private void BeginInspection(InspectableObject obj) {
            currentObject = obj;
            currentObject.StorePose();

            targetPosition = playerCamera.transform.position
                           + playerCamera.transform.forward * obj.InspectDistance;

            currentState = State.Traveling;
            ui.HidePrompt();
        }

        private void Cancel() {
            ui.HideClue();
            currentState = State.Returning;
        }

        private InspectableObject Raycast() {
            Ray ray = playerCamera.ScreenPointToRay(
                new Vector3(Screen.width / 2f, Screen.height / 2f, 0f)
            );

            if (Physics.Raycast(ray, out RaycastHit hit, detectionRange, inspectableLayer)) {
                return hit.collider.GetComponent<InspectableObject>();
            }

            return null;
        }
    }
}
