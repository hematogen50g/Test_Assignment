using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class Waypoint : MonoBehaviour
{
    //[SerializeField]
    //private int order;
    [Inject]
    GameConfig gc;
    //public int Order { get { return order; } }
    public Vector3 Position { get { return t.position; } }
    private SpriteRenderer sr;
    private Transform t;
    private void OnValidate()
    {
        t = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
    }
    public void DisableSprite(bool b)
    {
        sr.enabled = b;
    }
    public void Start()
    {
        DisableSprite(gc.ShowWaypoints);
    }
}
