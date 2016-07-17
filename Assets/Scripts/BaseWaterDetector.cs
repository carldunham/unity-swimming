using UnityEngine;
using System;
using System.Collections;
using System.Linq;


public abstract class BaseWaterDetector : MonoBehaviour
{
    public const int GROUND_LAYER_MASK = (1<<8);

    public Collider water;

    protected float waterY;
    protected Collider ourCollider;
    protected bool wasUnderwater = false;


    public bool underwater {
        get { 
            return this.water.bounds.IntersectRay(new Ray(this.transform.position, Vector3.up));
        }
    }

    public float percentSubmerged {
        get {
            // return 0.9f;
            return Mathf.Clamp01((this.waterY - this.ourCollider.bounds.min.y) / this.ourCollider.bounds.extents.y);
        }
    }

    public float depth {

        get {

            if (!this.underwater) {
                throw new InvalidOperationException();
            }
            Vector3 surface = new Vector3(this.ourCollider.bounds.center.x, this.waterY, this.ourCollider.bounds.center.z);

            RaycastHit[] hits = Physics.RaycastAll(surface, Vector3.down, Mathf.Infinity, GROUND_LAYER_MASK);

            if (hits.Length == 0) {
                return Mathf.Infinity;
            }

            float groundY = hits.Min(element => element.point.y);

            return waterY - groundY;
        }
    }

    public virtual void Start() {
        this.waterY = this.water.bounds.max.y;
        this.ourCollider = this.GetComponent<Collider>();
    }

    public virtual void Update() {
        bool isUnderwater = this.underwater;

        if (this.wasUnderwater != isUnderwater) {
        
            if (isUnderwater) {
                OnEnterWater();
            }
            else {
                OnLeaveWater();
            }
            this.wasUnderwater = isUnderwater;
        }
        // Debug.DrawRay(this.transform.position, Vector3.up * 100f, this.underwater ? Color.blue : Color.red);
    }

    protected abstract void OnEnterWater();
    protected abstract void OnLeaveWater();
}
