using UnityEngine;
using System.Collections;


public abstract class BaseCamera : MonoBehaviour
{
    public float timeToTransition = 2.0f;

    private bool inTransition = false;
    private float transitionTime;

    private const float TRANSITION_THRESHOLD = 0.6f;
    private const float SQR_TRANSITION_THRESHOLD = TRANSITION_THRESHOLD * TRANSITION_THRESHOLD;


    protected float CheckTransitionTime(Vector3 moveTo, bool changeCameras) {

        if (changeCameras) {
            this.inTransition = true;
            this.transitionTime = 0f;
        }

        if (this.inTransition) {
            this.transitionTime += Time.deltaTime;

            if ((moveTo - this.transform.position).sqrMagnitude < SQR_TRANSITION_THRESHOLD) {
                this.inTransition = false;
            }
            return this.transitionTime / this.timeToTransition;
        }
        return -1.0f;
    }

	protected Vector3 barrierStrikeCheck(Vector3 origin, Vector3 destination) {
		RaycastHit hit = new RaycastHit();

		if (Physics.Linecast(origin, destination, out hit)) {
			Debug.Log("camera strike detected");
//			return new Vector3(hit.point.x, destination.y, hit.point.z);
			return hit.point;
		}
		return destination;
	}

    protected void lookAt(Vector3 newPos, float transitionSpeed) {
        Vector3 pos = newPos - this.transform.position;
        Quaternion newRot = Quaternion.LookRotation(pos);
        this.transform.rotation = Quaternion.Lerp(transform.rotation, newRot, transitionSpeed);
    }
}
