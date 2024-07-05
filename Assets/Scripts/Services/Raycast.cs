using UnityEngine;

// this script identifies places (with a help of ray) where the player clicks with mouse button.
// If ray meets the BuildPoint, the script informs us that we can build at that place
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
        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // our ray comes from "ray". If the ray hits some object, the "out" command transfers an object info into "hit"
        if (!Physics.Raycast(ray, out hit)) return;

        var hitCollider = hit.collider; //try to get a collider from the object our ray hit into

        if (!hitCollider.CompareTag("BuildPoint")) return;
        if (!hitCollider.TryGetComponent<TowerBuildPoint>(out towerBuildPointScript)) return;
        if (!towerBuildPointScript.CanBuild) return;
    }

    public TowerBuildPoint GetTowerBuildPointScript()
    {
        return towerBuildPointScript;
    }
}
