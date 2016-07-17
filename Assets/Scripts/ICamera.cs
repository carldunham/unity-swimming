using System;


public interface ICamera
{
    string CameraName { get; }
    void MoveCamera(bool changeCameras);
}

