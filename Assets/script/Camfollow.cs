using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 5.0f;

    public GameObject player;

    public Vector2 center;
    public Vector2 size;
    float height;
    float width;


    void Start() 
    {
       height = Camera.main.orthographicSize;   
       width = height * Screen.width / Screen.height;
    }
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center,size);
    }

    private void LateUpdate()
    {
        Vector3 dir = player.transform.position - this.transform.position;
        Vector3 moveVector = new Vector3(dir.x * cameraSpeed * Time.deltaTime, dir.y * cameraSpeed * Time.deltaTime, 0.0f);
        this.transform.Translate(moveVector);

        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x , -lx + center.x , lx + center.x);
        
        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y , -ly + center.y , ly + center.y);

        transform.position = new Vector3(clampX, clampY , -10f);
  }
}