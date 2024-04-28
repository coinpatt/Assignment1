using UnityEngine;

public class CheckWinCondition : MonoBehaviour
{
    public GameObject winObject;

    [Range(0.01f, 1f)]
    public float positionTolerance = 0.1f;

    [Range(1f, 180f)]
    public float rotationTolerance = 1f;

    [Range(0.01f, 1f)]
    public float scaleTolerance = 0.1f;

    void Update()
    {
        GameObject[] checkObjects = GameObject.FindGameObjectsWithTag("Check");
        GameObject[] playObjects = GameObject.FindGameObjectsWithTag("Play");

        if (checkObjects.Length > 0 && playObjects.Length > 0)
        {
            foreach (GameObject checkObject in checkObjects)
            {
                foreach (GameObject playObject in playObjects)
                {
                    if (IsSameTransform(checkObject.transform, playObject.transform))
                    {
                        checkObject.SetActive(false);
                        playObject.SetActive(false);
                        winObject.SetActive(true);
                        return;
                    }
                }
            }
        }
    }

    bool IsSameTransform(Transform transform1, Transform transform2)
    {
        // Check position (disregarding Z-axis)
        Vector3 position1 = new Vector3(transform1.position.x, transform1.position.y, 0f);
        Vector3 position2 = new Vector3(transform2.position.x, transform2.position.y, 0f);
        if (Vector3.Distance(position1, position2) > positionTolerance)
            return false;

        // Check rotation
        if (Quaternion.Angle(transform1.rotation, transform2.rotation) > rotationTolerance)
            return false;

        // Check scale
        if (Vector3.Distance(transform1.localScale, transform2.localScale) > scaleTolerance)
            return false;

        return true;
    }
}
