using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

[RequireComponent(typeof(Camera))]
public class SnapshotCamera : MonoBehaviour
{
    Camera snapCam;

    int resWidth = 1080;
    int resHeight = 1920;

    private void Awake()
    {
        snapCam = GetComponent<Camera>();
        if(snapCam.targetTexture == null)
        {
            snapCam.targetTexture = new RenderTexture(resWidth, resHeight, 24);
        }else
        {
            resWidth = snapCam.targetTexture.width;
            resHeight = snapCam.targetTexture.height;
        }
        snapCam.gameObject.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CallTakeSnapshot();
        }
    }

    public void CallTakeSnapshot()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
        snapCam.gameObject.SetActive(true);
    }

    private void LateUpdate() {
        if(snapCam.gameObject.activeInHierarchy)
        {
            Texture2D snapshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            snapCam.Render();
            RenderTexture.active = snapCam.targetTexture;
            snapshot.ReadPixels(new Rect(0,0, resWidth, resHeight),0,0);
            byte[] bytes = snapshot.EncodeToPNG();
            string filename = SnapshotName();
            System.IO.File.WriteAllBytes(filename, bytes);
            snapCam.gameObject.SetActive(false);
        }    
    }

    string SnapshotName()
    {
        return string.Format("(0)/Snapshots/snap_(1)x(2)_(3).png", Application.dataPath, resWidth, resHeight, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm--ss"));
    }
}
