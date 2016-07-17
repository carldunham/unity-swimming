using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public const string DIRECTION = "direction";
    public const string SPEED = "speed";
    public const string UNDERWATER = "underwater";

    public FirstPersonCamera switchableCamera;
    public float speedTurnThreshold = 0.1f;
    public float landTurnSpeed = 2.0f;
    public float waterTurnSpeed = 0.3f;

    private Animator anim;
    // private PlayerWaterDetector waterDetector;
    private float lastDirection = 0.0f;

    private const float EPSILON = 0.001f;


    void Start() {
        this.anim = this.GetComponent<Animator>();
//        this.waterDetector = this.GetComponentInChildren<PlayerWaterDetector>();
        SwitchCamera[] switchers = this.anim.GetBehaviours<SwitchCamera> ();

        if (switchers != null) {

            foreach (SwitchCamera switcher in switchers) {
                switcher.camera = switchableCamera;
            }
        }
    }

    void FixedUpdate() {
        if (IsUnderwater()) {
            WaterUpdate();
        }
        else {
            LandUpdate();
        }
    }

    private bool IsUnderwater() {
        return this.anim.GetBool(UNDERWATER);
    }

    private void LandUpdate() {
        float targetDirection = Input.GetAxisRaw("Horizontal");
        float rawSpeed = Input.GetAxisRaw("Vertical");
        float speed = Input.GetAxis("Vertical");
        float direction;

//        if (this.anim.GetBool(UNDERWATER)) {
//            this.anim.SetBool(UNDERWATER, false);
//        }

        if (Mathf.Abs(rawSpeed) < this.speedTurnThreshold) {
            targetDirection = 0.0f;
        }

        if (targetDirection != this.lastDirection) {
            // direction = Mathf.SmoothStep(this.lastDirection, rawDirection, Time.deltaTime * directionDeltaSpeed);

            float sign = (targetDirection < this.lastDirection) ? -1f : 1f;
            float delta = Time.deltaTime * landTurnSpeed;
            direction = Mathf.Clamp(this.lastDirection + (sign * delta), -1f, 1f);

            if ((targetDirection == 0f) && (Mathf.Abs(direction) < EPSILON)) direction = 0f;
        }
        else {
            direction = this.lastDirection;
        }
        this.lastDirection = direction;

        this.anim.SetFloat(DIRECTION, direction);
        this.anim.SetFloat(SPEED, speed);
    }

    private void WaterUpdate() {
        float direction = Input.GetAxis("Horizontal");

        Vector3 rot = Vector3.right * direction;

        if (rot != Vector3.zero) {
            Quaternion newRotation = this.transform.rotation * Quaternion.LookRotation(rot);

            if (this.transform.rotation != newRotation) {
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, newRotation, Time.deltaTime * this.waterTurnSpeed);
            }
        }
    }
}
