# Fiinder
An app that uses congnitive computing for finding common lost items. Completed for COSC89.11, by Group 13.

## Setup Instructions
First, you will need the following applications installed:
- Unity 2019.3
- Xcode (if building for iOS)
- Android SDK (if building for Android)

## iOS Build Instructions
1. Open the project in Unity
2. In the taskbar, go to File -> Build Settings, and click on Build. Name it ThingFinderApp or similar. This could take a minute or two.
3. Open ThingFinderApp/Unity-iPhone.xcodeproj (If you changed the name during build, this folder will have a different name)
4. In the file explorer on the left of the Xcode window, double click on Unity-iPhone. Then under the "Signing and Capabilities" tab, for the Team selection bar, select your development profile name.
5. Plug in your iPhone to your computer. Select your device in the upper right of the Xcode window if it still says "Generic iOS Device"
6. Click build. (the play button near the device selector in the upper right of the window). This will install the app on your device and open the app.

## Android Build Instructions
1. Open the project in Unity
2. In the taskbar, go to File -> Build Settings, and switch the platform to Android.
3. Plug in your Android device, then click 'Build & Run'.
