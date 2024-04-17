using UnityEngine;

// this script identifies places (with a help of ray) where the player clicks with mouse button.
// If ray meets the BuildPoint, the script informs us that we can build at that place
public class Raycast : MonoBehaviour
{
    // we need an access to the camera that makes a ray
    [SerializeField] Camera rayCamera;

    //we need an access to the TowerBuilder to give him a command that we can or cannot build on the place chosen.
    [SerializeField] TowerBuilder towerBuilder;


    private void Update()
    {
        CheckToBuild();
    }

    void CheckToBuild()
    {
        if (!Input.GetMouseButtonDown(0)) return; // check wheter the mouse button was NOT clicked

        var ray = rayCamera.ScreenPointToRay(Input.mousePosition); // transform mousePosition into a ray

        RaycastHit hit; // save info about objects that cought the ray

        // our raycast comes from "ray". If the ray hits some object, the "out" command transfers an object info into "hit"
        if (!Physics.Raycast(ray, out hit)) return;

        var hitCollider = hit.collider; //try to get a collider from the object our ray hit into

        if (!hitCollider.CompareTag("BuildPoint")) return; //when the ray hit some collider, we need to know the object's tag
        if (!hitCollider.TryGetComponent<BuildPoint>(out var buildPoint)) return; //try to get a BuildPoint script to know if we can build a tower or not 
        if (!buildPoint.CanBuild) return; //to check whether it's allowed to build according to the BuildPoint info

        /*now we should check whether the tower was build.
        If the building was successful, we put "false". That means we cannot build in this point anymore*/
        if (towerBuilder.Construct(buildPoint))
        {
            buildPoint.BuildingPermission(false);
        }
    }
}
