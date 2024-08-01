using UnityEngine;

/* this script uses raycasting to detect when the user clicks on a build point in the scene. 
 * It then checks if the build point is valid for building and stores a reference to the corresponding TowerBuildPoint script. 
 * The GetTowerBuildPointScript() method provides access to this script for other parts of the game logic.
 * */
public class Raycast : MonoBehaviour
{
    [SerializeField] TowerBuilder towerBuilder;

    Camera mainCamera;
    TowerBuildPoint towerBuildPointScript;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        CheckToBuild();
    }

    void CheckToBuild()
    {
        // This line checks if the left mouse button has been clicked. If not, the method returns immediately.
        if (!Input.GetMouseButtonDown(0)) return;
        
        // This variable stores the result of the raycast.
        RaycastHit hit;
        if (!GetRaycastHit(out hit)) return;

        // This line retrieves the collider that was hit by the raycast.
        var hitCollider = hit.collider;
        // This line checks if the hit collider has a tag "BuildPoint".
        if (!hitCollider.CompareTag("BuildPoint")) return;
        // This line attempts to retrieve the TowerBuildPoint script from the hit collider.
        if (!hitCollider.TryGetComponent<TowerBuildPoint>(out towerBuildPointScript)) return;
        // This line checks if the build point is valid for building by checking the CanBuild property of the TowerBuildPoint script.
        if (!towerBuildPointScript.CanBuild) return;
    }

    bool GetRaycastHit(out RaycastHit hit)
    {
        // This line creates a ray from the camera through the mouse position on the screen.
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit);
    }

    // This method returns the TowerBuildPoint script that was retrieved in the CheckToBuild() method
    public TowerBuildPoint GetTowerBuildPointScript()
    {
        return towerBuildPointScript;
    }
}
