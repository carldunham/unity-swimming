using UnityEngine;
using System;
using System.Collections;

public class PlayerWaterDetector : BaseWaterDetector
{
    public float egressDepth = 1.0f;
    public float minimumSwimTimeBeforeEgress = 3.0f;
    public float buoyancy = (Physics.gravity * -2f).y;  // 9.81f * 1.1f = 10.791f;

    private Animator anim;
    private Rigidbody rigidBody;
    private int swimmingStateHash;

    private const string SWIMMING_STATE = "Base Layer.Swimming.swimming";


    private bool swimming {
        get {
            AnimatorStateInfo animState = this.anim.GetCurrentAnimatorStateInfo(0);
            return (animState.fullPathHash == this.swimmingStateHash) && (animState.length >= this.minimumSwimTimeBeforeEgress);
        }
    }
        
    public new void Start() {
        this.anim = this.GetComponentInParent<Animator>();
        this.rigidBody = this.GetComponentInParent<Rigidbody>();
        this.swimmingStateHash = Animator.StringToHash(SWIMMING_STATE);
        base.Start();
    }

    public void FixedUpdate() {
//        base.FixedUpdate();
        if (this.swimming) this.rigidBody.AddRelativeForce(this.buoyancy * this.percentSubmerged * Vector3.up, ForceMode.Acceleration);
    }

    public override void Update() {
        base.Update();

        if (this.swimming) {
            try {
//                Debug.Log("swimming and past minimum time, depth=" + this.depth.ToString());
//                Debug.DrawRay(new Vector3(this.ourCollider.bounds.center.x, this.waterY, this.ourCollider.bounds.center.z), Vector3.down * 100f, Color.red);

                if (this.depth < this.egressDepth) {
                    Debug.Log("shallow water, getting out, depth=" + this.depth.ToString());
                    anim.SetBool(PlayerController.UNDERWATER, false);
                }
            }
            catch {
//              Debug.Log("Unable to gauge depth, not underwater " + e.ToString());
            }
        }
    }

    protected override void OnEnterWater() {
        Debug.Log("Player entered the water");
        anim.SetBool(PlayerController.UNDERWATER, true);
    }

    protected override void OnLeaveWater() {
//        Debug.Log("Player left the water");
//        anim.SetBool(PlayerController.UNDERWATER, false);
    }
}
