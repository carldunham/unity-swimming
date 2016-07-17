using UnityEngine;
using System.Collections;

public class TargetingCamera : BaseCamera, ICamera
{
    public Transform transformToFollow;
    public Vector3 offset = new Vector3(-3.0f, 0.3f, 0f);
    public float smooth = 2.0f;


    public string CameraName { get { return "Targeting Camera"; } }


    void Awake() {
    }

    void Start() {
        MoveCamera(true);
    }

    void LateUpdate() {
        MoveCamera(false);
    }

    public void MoveCamera(bool changeCameras) {
        // TODO: not this
        Vector3 moveTo = this.transformToFollow.position + this.transformToFollow.forward * this.offset.x + this.transformToFollow.up * this.offset.y + this.transformToFollow.right * this.offset.z;
        // Vector3 moveTo = this.transformToFollow.position + this.transformToFollow.position * this.offset;

        float transitionSpeed = this.CheckTransitionTime(moveTo, changeCameras);

        if (transitionSpeed <= 0.0f) {
            transitionSpeed = Time.deltaTime * smooth;
        }
        moveTo = this.barrierStrikeCheck(this.transformToFollow.position, moveTo);
        this.transform.position = Vector3.Lerp(this.transform.position, moveTo, transitionSpeed);
        this.lookAt(this.transformToFollow.position, transitionSpeed);
    }
}
