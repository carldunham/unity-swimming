using UnityEngine;
using System.Collections;

public class SwitchCamera : StateMachineBehaviour
{
    private ISwitchableCamera _camera;

    public ISwitchableCamera camera { set { this._camera = value; } }


	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.LogFormat("Entering swimming state, _camera={0}", this._camera);
        if (this._camera != null) this._camera.setAltCamera();
	}

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (this._camera != null) this._camera.setMainCamera();
	}
}
