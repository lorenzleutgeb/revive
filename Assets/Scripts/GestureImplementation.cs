using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Input;
using HoloToolkit.Unity;
using HoloToolkit.Sharing;

public class GestureImplementation : MonoBehaviour
{

    private GestureRecognizer gestureRecognizer;
    private bool placing = false;

    // Use this for initialization
    void Start()
    {
        if (!CustomMessages.Instance.MessageHandlers.ContainsKey(CustomMessages.TestMessageID.TowerpositionChanged))
        {
            CustomMessages.Instance.MessageHandlers.Add(CustomMessages.TestMessageID.TowerpositionChanged, this.OnTowerPositionChanged);
        }
        else
        {
            CustomMessages.Instance.MessageHandlers[CustomMessages.TestMessageID.TowerpositionChanged] = this.OnTowerPositionChanged;
        }

        gestureRecognizer = new GestureRecognizer();
        gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap | GestureSettings.DoubleTap);
        gestureRecognizer.TappedEvent += GestureRecognizer_TappedEvent;

        gestureRecognizer.StartCapturingGestures();

    }

    private void OnTowerPositionChanged(NetworkInMessage msg)
    {
        // We read the user ID but we don't use it here.
        msg.ReadInt64();

        GameObject.Find("wrapper").transform.localPosition = CustomMessages.Instance.ReadVector3(msg);
        GameObject.Find("wrapper").transform.localRotation = CustomMessages.Instance.ReadQuaternion(msg);


    }

    private void GestureRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        if (tapCount == 1 && placing)
        {
            placing = false;
            SpatialMappingManager.Instance.DrawVisualMeshes = false;
        }
        else if (tapCount == 2)
        {
            placing = true;
            SpatialMappingManager.Instance.DrawVisualMeshes = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // If the user is in placing mode,
        // update the placement to match the user's gaze.
        if (placing)
        {
            // Do a raycast into the world that will only hit the Spatial Mapping mesh.
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
                30.0f, SpatialMappingManager.Instance.LayerMask))
            {
                // Move this object to where the raycast
                // hit the Spatial Mapping mesh.
                // Here is where you might consider adding intelligence
                // to how the object is placed.  For example, consider
                // placing based on the bottom of the object's
                // collider so it sits properly on surfaces.
                GameObject.Find("wrapper").transform.position = hitInfo.point;

                // Rotate this object to face the user.
                Quaternion toQuat = Camera.main.transform.localRotation;
                toQuat.x = 0;
                toQuat.z = 0;
                GameObject.Find("wrapper").transform.rotation = toQuat;

                CustomMessages.Instance.SendTowerPosition(GameObject.Find("wrapper").transform.localPosition, GameObject.Find("wrapper").transform.localRotation);
            }
        }
    }
}
