using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingCharacter : MonoBehaviour
{
    [SerializeField] float RotationSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float rotX = Input.GetAxis("Mouse X") * RotationSpeed * Mathf.Deg2Rad;
        if (rotX != 0f)
        {
            transform.Rotate(Vector3.up, rotX);
        }
        else
        {
            if (Vector3.zero != transform.rotation.eulerAngles)
            {
                if (transform.localScale.z < 0f)
                {
                    float rotVal = transform.rotation.eulerAngles.z;
                    rotVal += Time.deltaTime;
                    if (rotVal > 0f)
                    {
                        rotVal = 0f;
                    }
                    // PlayerObject.transform.rotation = Quaternion. Quaternion.EulerRotation(0f, 0f, rotVal);
                }
                else
                {
                    float rotVal = transform.localScale.z;
                    rotVal -= Time.deltaTime;
                    if (rotVal < 0f)
                    {
                        rotVal = 0f;
                    }
                    // PlayerObject.transform.localScale = new Vector3(0f, 0f, rotVal);
                }
            }
        }
    }
}
