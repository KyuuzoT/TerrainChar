using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    [SerializeField][Range(0.01f,8.0f)] private float rotationSpeed = 0.4f;
    private float rotation = 0.0f;
    // Update is called once per frame
    void Update()
    {
        rotation += Time.deltaTime * rotationSpeed;
        if(rotation >= 360.0f)
        {
            rotation = 0.0f;
        }
        RenderSettings.skybox.SetFloat("_Rotation", rotation);
    }
}
