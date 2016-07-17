using System;


public interface ISwitchableCamera: ICamera
{
    void switchCamera();
    void setMainCamera();
    void setAltCamera();
}

