using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxScroll : MonoBehaviour
{
     Material material;
    [SerializeField] float scrollSpeed = 0.1f;
    Camera cam;
    Vector2 lasPos;
    Vector2 delta;
    private void Awake()
    {
         material = GetComponent<MeshRenderer>().material;

        cam = Camera.main;
        lasPos = cam.transform.position;
    }
    Vector2 offset;
    void Update()
    {
        delta = (Vector2)cam.transform.position - lasPos;
        lasPos = (Vector2)cam.transform.position;
        offset.x += delta.x * scrollSpeed * Time.deltaTime;
        material.SetTextureOffset("_MainTex", offset);
    }
}
