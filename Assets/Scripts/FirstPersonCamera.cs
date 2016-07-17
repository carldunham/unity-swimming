using UnityEngine;
using System.Collections;


public class FirstPersonCamera : BaseCamera, ISwitchableCamera
{
    public Transform marker;
    public Transform altMarker;
    public bool useAlt = false;
    public float rotationSpeed = 2.0f;

    public void switchCamera() {
        this.useAlt = !this.useAlt;
    }

    public void setMainCamera() {
        this.useAlt = false;
    }

    public void setAltCamera() {
        this.useAlt = true;
    }


    public string CameraName { get { return "First Person Camera"; } }


    void Awake() {
    }

    void Start() {
        MoveCamera(true);
    }

    void LateUpdate() {
        MoveCamera(false);
    }

    public void MoveCamera(bool changeCameras) {
        Transform marker = (this.useAlt ? this.altMarker : this.marker);
        Vector3 moveTo = marker.position;

        float transitionSpeed = this.CheckTransitionTime(moveTo, changeCameras);

        if (transitionSpeed > 0.0f) {
            this.transform.position = Vector3.Lerp(this.transform.position, moveTo, transitionSpeed);

            Debug.LogFormat("In Transition, magnitude={0}", (moveTo - this.transform.position).magnitude);
        }
        else {
            this.transform.position = moveTo;
        }

        Quaternion newRotation = this.transform.rotation * marker.rotation;

        if (this.transform.rotation != newRotation) {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, marker.rotation, Time.deltaTime * this.rotationSpeed);
        }
    }
}
