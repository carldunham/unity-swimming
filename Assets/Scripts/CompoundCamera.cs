using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class CompoundCamera : MonoBehaviour {

    public int cameraIndex = 0;

    private ICamera[] cameraScripts;

    private int lastCameraIndex;


    public void Start()
    {
        this.cameraScripts = this.GetComponents<ICamera>();
        Debug.Assert(this.cameraScripts != null);

        foreach (ICamera camera in this.cameraScripts) {
            MonoBehaviour script = camera as MonoBehaviour;

            if (script.enabled) {
                Debug.LogWarningFormat("Disabling dependent camera script '{0}'", camera);
                script.enabled = false;
            }
        }
        this.cameraIndex %= this.cameraScripts.Length;
        this.lastCameraIndex = this.cameraIndex;
    }

    void LateUpdate() {
        
        if (this.cameraScripts.Length > 0) {
            ICamera camera = cameraScripts[this.cameraIndex];
            int newIndex = this.getCameraIndex();

            bool changeCameras = newIndex != this.lastCameraIndex;

            if (changeCameras) {
                camera = cameraScripts[newIndex];
                this.lastCameraIndex = this.cameraIndex = newIndex;

                Debug.LogFormat("Moving to Camera{0} ({1})", newIndex, camera.CameraName);
            }
            camera.MoveCamera(changeCameras);
        }
    }

    private int getCameraIndex() {
        this.cameraIndex %= this.cameraScripts.Length;

        for (int newIndex=0; newIndex<this.cameraScripts.Length; newIndex++) {

            if (Input.GetAxisRaw(string.Format("Camera{0}", newIndex+1)) > 0) {
                return newIndex;
            }
        }
        return this.cameraIndex;
    }
}
