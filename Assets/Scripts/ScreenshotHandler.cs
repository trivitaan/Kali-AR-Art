using UnityEngine;
using System;

public class ScreenshotHandler : MonoBehaviour {
    public void SaveScreenshotToGallery(byte[] imageData, string filename) {
        AndroidJavaClass jc = new AndroidJavaClass("com.kali.arapp.NativeGallery");
        AndroidJavaObject currentActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");

        jc.CallStatic("saveImageToGallery", currentActivity, imageData, filename);
    }

    void CaptureScreenshotOnClick() {
    int width = Screen.width;
    int height = Screen.height;
    Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
    screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
    screenshot.Apply();

    byte[] bytes = screenshot.EncodeToPNG();
    string fileName = "Screenshot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";

    SaveScreenshotToGallery(bytes, fileName);
}

}
