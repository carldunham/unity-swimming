using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

    public Transform transformToFollow;
    public float smooth = 1;

    private Vector3 offset;

    void Start() {
        offset = this.transform.position - transformToFollow.position;
        MoveCamera();
    }
    
    void LateUpdate() {
        MoveCamera();
    }

    private void MoveCamera() {
        Vector3 moveTo = transformToFollow.position + offset;
        this.transform.position = Vector3.Lerp(this.transform.position, moveTo, Time.deltaTime * smooth);
    }
}
