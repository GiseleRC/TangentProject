using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] _points;

    private void Awake()
    {
        _points = new Transform[transform.childCount];

        for (int i = 0; i < _points.Length; i++)
        {
            _points[i] = transform.GetChild(i);
        }
    }
}
