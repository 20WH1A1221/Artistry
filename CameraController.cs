using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
public class CameraController : MonoBehaviour
{
    public RawImage imageDisplay; // Reference to a UI RawImage to display the captured photo

    public WebCamTexture camTexture; // Reference to the camera texture

    // Start is called before the first frame update
    void Start()
    {
        // Start the camera
        if(!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
    }
    public void OnClickStartCamera()
    {
        StartCamera();
    }
    // Start the camera
    void StartCamera()
    {
        // Find the device camera
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > 0)
        {
            // Use the first device found
            camTexture = new WebCamTexture(devices[0].name);
            if (camTexture.isPlaying)
                return;
            // Start the camera
            camTexture.Play();
        }
        else
        {
            Debug.LogError("No camera device found.");
        }
    }

    // Update is called once per frame

    // Capture a photo from the camera
    public void CapturePhoto()
    {
        // Check if the camera is running
        if (camTexture.isPlaying)
        {
            // Create a texture to store the captured photo
            Texture2D photoTexture = new Texture2D(camTexture.width, camTexture.height);
            // Read the pixels from the camera texture
            photoTexture.SetPixels(camTexture.GetPixels());
            // Apply the changes to the texture
            photoTexture.Apply();

            // Display the captured photo on a UI RawImage
            imageDisplay.texture = photoTexture;
        }
        else
        {
            Debug.LogWarning("Camera is not running. Unable to capture photo.");
        }
    }
}
