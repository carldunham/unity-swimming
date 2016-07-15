using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speedTurnThreshold = 0.1f;
    public float directionDeltaSpeed = 2.0f;

    private Animator anim;
    private float lastDirection = 0.0f;

    private const float EPSILON = 0.001f;


    void Start() {
        anim = this.GetComponent<Animator>();
    }

    void FixedUpdate() {
        float targetDirection = Input.GetAxisRaw("Horizontal");
        float rawSpeed = Input.GetAxisRaw("Vertical");
        float speed = Input.GetAxis("Vertical");
        float direction;

        if (Mathf.Abs(rawSpeed) < this.speedTurnThreshold) {
            targetDirection = 0.0f;
        }

        if (targetDirection != this.lastDirection) {
            // direction = Mathf.SmoothStep(this.lastDirection, rawDirection, Time.deltaTime * directionDeltaSpeed);

            float sign = (targetDirection < this.lastDirection) ? -1f : 1f;
            float delta = Time.deltaTime * directionDeltaSpeed;
            direction = Mathf.Clamp(this.lastDirection + (sign * delta), -1f, 1f);

            if ((targetDirection == 0f) && (Mathf.Abs(direction) < EPSILON)) direction = 0f;
        }
        else {
            direction = this.lastDirection;
        }
        this.lastDirection = direction;

        anim.SetFloat("direction", direction);
        anim.SetFloat("speed", speed);
    }
}
