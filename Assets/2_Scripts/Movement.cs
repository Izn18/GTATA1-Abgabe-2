using UnityEngine;

public class Movement : MonoBehaviour
{
    private float speed = 10f;
    public CharacterController controller;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // LimitPosition(); 
    }

    ///<summary>
    /// This method makes sure that the Player stays in a given area
    ///</summary>
    public void LimitPosition()
    {
        float currX = transform.position.x;
        float currY = transform.position.y;
        float currZ = transform.position.z;

        // Limit the position on the y-axis
        if (currY > 10)
        {
            transform.position = new Vector3(currX, 10, currZ);
        }

        // Limit the position on the z-axis
        if (currZ > 23)
        {
            transform.position = new Vector3(currX, currY, 23);
        }

        if (currZ < -17)
        {
            transform.position = new Vector3(currX, currY, -17);
        }

        // Limit the position on the x-axis
        if (currX > 23)
        {
            transform.position = new Vector3(23, currY, currZ);
        }

        if (currX < -17)
        {
            transform.position = new Vector3(-17, currY, -currZ);
        }
    }
}
