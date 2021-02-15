using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

//attach this script to your camera object
public class CreateStereoCubemaps : MonoBehaviour {
    public GameObject FrameRateSlider;
    public GameObject LengthSlider;
    public GameObject CamObject;
    public RenderTexture cubemapLeft;
    public RenderTexture cubemapRight;
    public RenderTexture equirect;
    public RenderTexture vidCubeLeft;
    public RenderTexture vidCubeRight;
    public RenderTexture vidEquirect;
    public bool renderStereo = true; //8k 4k
    public bool preview = true;
    public float stereoSeparation = 0.064f;
    public GameObject Menu;
    private bool recording = false;
    private int length = 0;
    private int frameRate = 0;
    private int counter = 0;
    private List<byte[]> vidBuffer = new List<byte[]>();
    private float _timer;
    private float _hudRefreshRate = 1f;

    void Start() {
        System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/vidImages/");
    }

    void LateUpdate() {

        if (!recording) {
            return;
        }

        if (counter == length * frameRate) {
            saveImages();
        }
        
        if (Time.unscaledTime > _timer) {
            counter++;
            _timer = Time.unscaledTime + (_hudRefreshRate / frameRate);
            if (recording) {
                saveFrame();
            } else {
                UpdateTexture();
            }
        }
    }
    public void ToggleRecord() {
        recording = !recording;
        UpdateValues();
    }

    public void UpdateValues() {
        length = int.Parse(LengthSlider.GetComponent<TextMeshPro>().text);
        frameRate = int.Parse(FrameRateSlider.GetComponent<TextMeshPro>().text);
        Debug.Log($"{length} {frameRate}");
    }

    public void TakePhoto() {
        Camera cam = CamObject.GetComponent<Camera>();

        if (cam == null) {
            cam = GetComponentInParent<Camera>();
        }

        if (cam == null) {
            Debug.Log("stereo 360 capture node has no camera or parent camera");
        }

        if (renderStereo) {
            cam.stereoSeparation = stereoSeparation;
            cam.RenderToCubemap(cubemapLeft, 63, Camera.MonoOrStereoscopicEye.Left);
            cam.RenderToCubemap(cubemapRight, 63, Camera.MonoOrStereoscopicEye.Right);
        } else {
            cam.RenderToCubemap(cubemapLeft, 63, Camera.MonoOrStereoscopicEye.Mono);
        }

        //optional: convert cubemaps to equirect

        if (equirect == null) {
            return;
        }

        if (renderStereo) {
            cubemapLeft.ConvertToEquirect(equirect, Camera.MonoOrStereoscopicEye.Left);
            cubemapRight.ConvertToEquirect(equirect, Camera.MonoOrStereoscopicEye.Right);
        } else {
            cubemapLeft.ConvertToEquirect(equirect, Camera.MonoOrStereoscopicEye.Mono);
        }

        // Creates buffer
        Texture2D tempTexture = new Texture2D(equirect.width, equirect.height);

        // Copies EquirectTexture into the tempTexture
        RenderTexture currentActiveRT = RenderTexture.active;
        RenderTexture.active = equirect;
        tempTexture.ReadPixels(new Rect(0, 0, equirect.width, equirect.height), 0, 0);

        // Exports to a PNG
        var bytes = tempTexture.EncodeToPNG();
        var bytes2 = tempTexture.EncodeToJPG();

        System.IO.File.WriteAllBytes(Application.persistentDataPath + "/test.jpg", bytes2);

        // Restores the active render texture
        RenderTexture.active = currentActiveRT;
    }

    public void UpdateTexture() {
        Camera cam = CamObject.GetComponent<Camera>();

        if (cam == null) {
            cam = GetComponentInParent<Camera>();
        }

        if (cam == null) {
            Debug.Log("stereo 360 capture node has no camera or parent camera");
        }

        if (renderStereo) {
            cam.stereoSeparation = stereoSeparation;
            cam.RenderToCubemap(vidCubeLeft, 63, Camera.MonoOrStereoscopicEye.Left);
            cam.RenderToCubemap(vidCubeRight, 63, Camera.MonoOrStereoscopicEye.Right);
        } else {
            cam.RenderToCubemap(cubemapLeft, 63, Camera.MonoOrStereoscopicEye.Mono);
        }

        //optional: convert cubemaps to equirect

        if (vidEquirect == null) {
            return;
        }

        if (renderStereo) {
            vidCubeLeft.ConvertToEquirect(vidEquirect, Camera.MonoOrStereoscopicEye.Left);
            vidCubeRight.ConvertToEquirect(vidEquirect, Camera.MonoOrStereoscopicEye.Right);
        } else {
            cubemapLeft.ConvertToEquirect(vidEquirect, Camera.MonoOrStereoscopicEye.Mono);
        }
    }

    public void StopRecording(){
        recording = false;
    }

    private void saveImages() {
        recording = false;
        counter = 0;
        for (int i = 0; i < vidBuffer.Count; i++)
        {
            System.IO.File.WriteAllBytes(Application.persistentDataPath + "/vidImages/frame" + i + ".jpg", vidBuffer[i]);
        }
        vidBuffer.Clear();

    }

    private void saveFrame() {
        UpdateTexture();
        // Creates buffer
        Texture2D tempTexture = new Texture2D(vidEquirect.width, vidEquirect.height);

        // Copies EquirectTexture into the tempTexture
        RenderTexture currentActiveRT = RenderTexture.active;
        RenderTexture.active = vidEquirect;
        tempTexture.ReadPixels(new Rect(0, 0, vidEquirect.width, vidEquirect.height), 0, 0);

        // Exports to a PNG
        var bytes = tempTexture.EncodeToPNG();
        var bytes2 = tempTexture.EncodeToJPG();
        vidBuffer.Add(bytes2);
        //System.IO.File.WriteAllBytes(Application.persistentDataPath + "/test.jpg", bytes2);

        // Restores the active render texture
        RenderTexture.active = currentActiveRT;
    }

}