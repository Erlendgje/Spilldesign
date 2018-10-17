using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public bool up;
    private float movementSpeed = 10;
    private float camDestinationY;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().velocity.y > 0)
            {
                camDestinationY = Camera.main.transform.position.y + Camera.main.orthographicSize * 2;
            }
            else if (GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().velocity.y < 0)
            {
                camDestinationY = Camera.main.transform.position.y - Camera.main.orthographicSize * 2;
            }
            StartCoroutine("panCamera");
        }
    }

    private IEnumerator panCamera()
    {
        while(camDestinationY != Camera.main.transform.position.y)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.GetComponent<Transform>().position, new Vector3(Camera.main.transform.position.x, camDestinationY, Camera.main.transform.position.z), movementSpeed * Time.deltaTime);
            yield return 0;
        }
    }
}
