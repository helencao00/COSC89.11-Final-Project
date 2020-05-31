using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using IBM.Cloud.SDK;
using IBM.Cloud.SDK.Authentication;
using IBM.Cloud.SDK.Authentication.Iam;
using IBM.Watson.VisualRecognition.V3;
using IBM.Watson.VisualRecognition.V3.Model;

public class ThingFinder : MonoBehaviour {

    [SerializeField]
    ARCameraManager cameraManager;

    Authenticator authenticator;
    VisualRecognitionService recognizer;
    Texture2D frameBuffer;
    const string ApiKey = @"od5t381iEMYecc8CPPLBE5zoVEry2GZbYW-xuovotVf1";
    const string VersionDate = @"2018-03-19";
    const string ClassifierID = @"DefaultCustomModel_122886958";

    IEnumerator Start () {
        // Authenticate
        authenticator = new IamAuthenticator(ApiKey);
        yield return new WaitUntil(authenticator.CanAuthenticate);
        // Create recognizer
        recognizer = new VisualRecognitionService(VersionDate, authenticator);
        // Register for camera frames
        cameraManager.frameReceived += OnPreviewFrame;
    }

    void OnDisable () => cameraManager.frameReceived -= OnPreviewFrame;

    void OnPreviewFrame (ARCameraFrameEventArgs args) {
        // Skip
        if (Time.frameCount % 30 != 0)
            return;
        // Get frame
        if (!cameraManager.TryGetLatestImage(out var image))
            return;
        // Convert to RGB(A)
        image.ConvertAsync(
            new XRCameraImageConversionParams {
                inputRect = new RectInt(0, 0, image.width, image.height),
                outputDimensions = new Vector2Int(image.width / 2, image.height / 2),
                outputFormat = TextureFormat.RGBA32, // CHECK // RGB24 or RGBA32?
                transformation = CameraImageTransformation.None
            },
            (status, config, data) => {
                // Check
                if (status != AsyncCameraImageConversionStatus.Ready)
                    return;
                // Copy data into texture
                if (frameBuffer)
                    if (frameBuffer.width != config.outputDimensions.x || frameBuffer.height != config.outputDimensions.y) {
                        Texture2D.Destroy(frameBuffer);
                        frameBuffer = null;
                    }
                frameBuffer = frameBuffer ?? new Texture2D(config.outputDimensions.x, config.outputDimensions.y, config.outputFormat, false);
                frameBuffer.LoadRawTextureData(data);
                // Send to Watson
                var jpegData = ImageConversion.EncodeToJPG(frameBuffer);
                using (var stream = new MemoryStream(jpegData))
                    recognizer.Classify(
                        OnClassificationResult,
                        stream,
                        imagesFileContentType: @"image/jpeg",
                        threshold: 0.5f,
                        classifierIds: new List<string> { ClassifierID }
                    );
            }
        );
        // Release frame
        image.Dispose();
    }

    void OnClassificationResult (DetailedResponse<ClassifiedImages> response, IBMError error) {
        var labels = response.Result.Images[0].Classifiers[0].Classes.Select(c => (c._Class, c.Score)).ToArray();
        var labelsStr = string.Join(", ", labels);
        Debug.Log($"Result: {labelsStr}");
    }
}
