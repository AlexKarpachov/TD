using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField][Range(0, 5)] float enemySpeed;

    Enemy enemy;

     void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void FindPath()
    {
        path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in parent.transform)
        {
            path.Add(child.GetComponent<Waypoint>());
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }
    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = waypoint.transform.position;
            float lerpCoef = 0f;

            transform.LookAt(endPos);

            while (lerpCoef < 1f)
            {
                lerpCoef += Time.deltaTime * enemySpeed;
                transform.position = Vector3.Lerp(startPos, endPos, lerpCoef);
                yield return new WaitForEndOfFrame();
            }
        }

        enemy.PenaltyGold();
        gameObject.SetActive(false);
    }
}
