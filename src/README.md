# Collaborative Cinematic VR Production Tool
Production tool for VR artists and project managers, with focus on collaborative work for cinema, made in Unity.

## Directory Structure:

/CCVRPT   - Unity project 

/Packages - required Unity packages

## Instructions for Developers:

1. Download and install Unity with Android build support.

2. Download this repository and make sure the 'External' folder inside the Unity project directory isn't empty.

3. Open the 'Assets' folder inside the Unity project directory and delete the 'MixedRealityToolkit.Generated' folder and the 'MRTK' shortcut. Also delete the corresponding .meta files.

4. Open the 'External' folder inside the Unity project directory and run the .bat (for Windows users) or .sh (for Linux users) file with admin priviledges (only tested for Windows).

5. Open the project inside Unity, wait for updates to be completed (if needed) and accept the notification that pops up once the project is open.

6. Some errors should appear on the command line. Download and import the Oculus Integration Unity package in order to solve them.

7. Upgrade plugins and restart the project (if needed).

8. Turn on your Oculus Quest and connect it to the computer running the Unity project using the link cable.

9. Select File > Build Settings > Android, on the 'Run Device' select the Oculus Quest and then click on 'Switch Platform'.

10. If "Missing Project ID" error pops up, go to Window > General > Services, then click create a new link.

11. If errors related to "MixedRealityStandard.shader" occur, lower your quality settings. If that does not work, check inside the shader file if all fixed<X> and half<X> are replaced by float<X>.

12. When executing with "Build and Run", on Oculus Link, you can open a console and type "adb logcat -s Unity" for debugging.

## Authors:

Filipe Pires

Isaac dos Anjos
