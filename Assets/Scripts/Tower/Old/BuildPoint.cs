using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    [SerializeField] GameObject storeMenu;
    NewTowerBuilder newTowerBuilder;


    bool isPlaceable = true;
    public bool IsPlaceable { get { return isPlaceable; } }

    private Transform buildPointTransform;
    public Transform BuildPointTransform { get { return buildPointTransform; } }

    private void Start()
    {
        buildPointTransform = transform;
        newTowerBuilder = NewTowerBuilder.instance;
    }

    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            Debug.Log("The click was made in " + transform.position);
            storeMenu.SetActive(true);
            Time.timeScale = 0f;
            newTowerBuilder.SetBuildPoint(this);
        }
    }

    public void HideBuildPoint()
    {
        gameObject.SetActive(false);
    }
}
