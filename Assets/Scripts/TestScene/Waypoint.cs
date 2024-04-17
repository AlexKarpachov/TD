using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Ballista ballistaPrefab;

    [SerializeField] bool isPlaceable = true;
    public bool IsPlaceable { get { return isPlaceable; } }

    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = ballistaPrefab.CreatePrefab(ballistaPrefab, transform.position);
            isPlaceable = !isPlaced;
        }
    }
}
