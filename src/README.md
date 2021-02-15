# Collaborative Cinematic VR Production Tool
Production tool for VR artists and project managers, with focus on collaborative work for cinema, made in Unity.

## Directory Structure:

/CCVRPT   - Unity project 

/Packages - required Unity packages

## Instructions for Developers:

Installation:

1. Download and install Unity with Android build support.

2. Download this repository and make sure the 'External' folder inside the Unity project directory isn't empty.

3. Open the 'Assets' folder inside the Unity project directory and delete the 'MixedRealityToolkit.Generated' folder and the 'MRTK' shortcut. Also delete the corresponding .meta files.

4. Open the 'External' folder inside the Unity project directory and run the .bat (for Windows users) or .sh (for Linux users) file with admin priviledges (only tested for Windows).

5. Open the project inside Unity, wait for updates to be completed (if needed) and accept the notification that pops up once the project is open.

6. Some errors should appear on the command line. Download and import the Oculus Integration Unity package in order to solve them.

7. Upgrade plugins and restart the project (if needed).

8. Turn on your Oculus Quest and connect it to the computer running the Unity project using the link cable.

9. Select File > Build Settings > Android, on the 'Run Device' select the Oculus Quest and then click on 'Switch Platform'.

Errors:

1. If "Missing Project ID" error pops up, go to Window > General > Services, then click create a new link.

2. If errors related to "MixedRealityStandard.shader" occur, lower your quality settings. If that does not work, check inside the shader file if all fixed<X> and half<X> are replaced by float<X>.

Debugging:

1. Connect your Quest using Oculus Link.

2. For debugging, you need to run the project using the "Build and Run" option.

3. Open a console and type "adb logcat -s Unity".

Adding New Items to Inventory:

1. Open Unity and load the project on the Playground scene.

2. On the Hierarchy, scroll down to SceneContent > Interaction and open the HandMenu_Playground prefab in edit mode. Then, scroll down to MenuContent > InventoryMenu and open the ScrollingObjects prefab in edit mode. 

3. On the Hierarchy, scroll down to ScrollingObjectCollection > Container > GridObjectCollection, create an instance of the PressableItem prefab and name it.

4. On the Hierarchy, scroll down to PressableItemInstance (or the name you gave to it) > ButtonContent > CompressableButtonVisuals > FrontPlate > Item and insert the inventory item you wish to add.

5. Make sure the added item is confined within the UI button. To do this, click on the FrontPlate and check if the highlighted border is larger than the item added.

6. You have created the UI button, but the item is not spawnable yet. To do this, enable ObjectInstance temporarily, select InteractableObject and create a child instance containing the object you wish to spawn.

7. Make sure the added object's scale is less than 1.0. Then, select InteractableObject and, in the Inspector tab, click on Box Collider > Edit Collider - this will allow you to edit the collider and wrap it around your object for propper manipulation during gameplay (make sure its scale is 1). After that you can (and should) disable ObjectInstance.

8. Save changes to the ScrollingObjects prefab and go back to HandMenu_Playground prefab's edit mode.

9. Select your newly created item and, in the Inspector tab, take a look at the SpawnItem and RemoveItem scripts. Drag the MenuContent object (from Hierarchy) to the Hand Menu field (on both scripts), and drag the item's ObjectInstance (that should be disabled) to the Spawn Position field. This will allow the system to manage the creation/deletion of object instances during gameplay.

10. Select the InteractableObject and, in the Inspector tab, go to the Object Manipulator script and add an element to the lists On Hover Entered and On Hover Exited. For each list item, drag the MenuContent (from the hierarchy) to it and select the corresponding function for enabling the remove button (either HandMenu_Playground.EnableRemoveButton() or HandMenu_Playground.DisableRemoveButton()). Finally, drag the Remove button (child of the current InteractableObject) to the function argument. 

11. The latest implementation also requires you to assign a unique tag to the newly created inventory item. To do this, on the Hierarchy, scroll down to MenuContent > InventoryMenu > ScrollingObjects > ScrollingObjectCollection > Container > GridObjectCollection > (name of your new item) > ButtonContent > CompressableButtonVisuals > FrontPlate, then assign a new tag equal to both the Item and ObjectInstance GameObjects.

12. You can now save changes to the HandMenu_Playground prefab as well and it should work just fine.

## Authors:

Filipe Pires

Isaac dos Anjos
