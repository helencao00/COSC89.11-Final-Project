# Fiinder README
## Group 13 COSC89.11-Final-Project
To build the app to an iPhone, you will need:
- Unity version 2019.3.11f1
- Xcode
- Apple development profile
- An iPhone

1. Open Assets/Fiinder.unity in Unity
2. In the taskbar, go to File -> Build Settings, and click on Build. Name it ThingFinderApp or similar. This could take a minute or two.
3. Open ThingFinderApp/Unity-iPhone.xcodeproj (If you changed the name during build, this folder will have a different name)
4. In the file explorer on the left of the Xcode window, double click on Unity-iPhone. 
	- Under the "General" tab, set the target iOS version to your iPhone's iOS version
	- Under the "Signing and Capabilities" tab, set a unique bundle identifier (a unique name).
	- Under the "Signing and Capabilities" tab, for the Team selection bar, select your development profile name

5. Plug in your iPhone to your computer. Select your device in the upper right of the Xcode window if it still says "Generic iOS Device"
6. Click build. (the play button near the device selector in the upper right of the window). This will install the app on your device.
7. Launch the app. Watson will be called every few seconds, and alert you if it thinks it sees keys, wallets, or glasses. 


