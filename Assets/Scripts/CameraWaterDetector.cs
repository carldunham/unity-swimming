using UnityEngine;
using System;
using System.Collections;

public class CameraWaterDetector : BaseWaterDetector
{
    public Color waterColor = new Color(0.05f, 0.36f, 0.58f, 1f);
    public float waterClarity = 0.95f;

    private bool savedFog;
    private Color savedFogColor;
    private float savedFogDensity;

    protected override void OnEnterWater() {
        // turn on fog
        this.savedFog = RenderSettings.fog;
        this.savedFogColor = RenderSettings.fogColor;
        this.savedFogDensity = RenderSettings.fogDensity;

        RenderSettings.fogColor = this.waterColor;
        RenderSettings.fogDensity = 1.0f - this.waterClarity;
        RenderSettings.fog = true;

        // cam.clearFlags = CameraClearFlags.SolidColor; 
        // cam.backgroundColor = uFogColor; 
    }

    protected override void OnLeaveWater() {
        // turn off fog
        RenderSettings.fogColor = this.savedFogColor;
        RenderSettings.fogDensity = this.savedFogDensity;
        RenderSettings.fog = this.savedFog;

        // cam.clearFlags = CameraClearFlags.Skybox; 
    }
}
