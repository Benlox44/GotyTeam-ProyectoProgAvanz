using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenshotsCapture : MonoBehaviour
{
    private int screenshotCount = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            string folderPath = Application.dataPath + "/Screenshots/";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string screenshotName = folderPath + "Screenshot" + screenshotCount + ".png";
            ScreenCapture.CaptureScreenshot(screenshotName);
            screenshotCount++;
            Debug.Log("Screenshot saved to: " + screenshotName);
        }
    }
}
