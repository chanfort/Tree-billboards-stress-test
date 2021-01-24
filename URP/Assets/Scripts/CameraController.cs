using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController active;
    [HideInInspector] public Terrain terrain;

    void Awake()
    {
        active = this;
    }

    void Start()
    {

    }

    void Update()
    {
        Move();
        Rotate();
        Zoom();
    }

    void Move()
    {
        Vector3 movingDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movingDirection += transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movingDirection += Vector3.Cross(transform.forward, Vector3.up).normalized * transform.forward.magnitude;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movingDirection += -transform.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movingDirection += -Vector3.Cross(transform.forward, Vector3.up).normalized * transform.forward.magnitude;
        }

        float h = terrain.SampleHeight(transform.position);

        if (h < 337f)
        {
            h = 337f;
        }

        float y = transform.position.y - h;
        Vector3 newPosition = movingDirection * Time.deltaTime * y;
        transform.position += newPosition;
        float newHeight = terrain.SampleHeight(transform.position);

        if (newHeight < 337f)
        {
            newHeight = 337f;
        }

        transform.position = new Vector3(transform.position.x, newHeight + y, transform.position.z);
    }

    void Rotate()
    {
        if (Input.GetMouseButton(1))
        {
            float h = -5f * Input.GetAxis("Mouse X");
            float v = 5f * Input.GetAxis("Mouse Y");

            transform.Rotate(0, h, 0, Space.World);
            transform.Rotate(v, 0, 0);

            if ((transform.rotation.eulerAngles.z >= 160) && (transform.rotation.eulerAngles.z <= 200))
            {
                transform.Rotate(-v, 0, 0);
            }
        }
    }

    void Zoom()
    {
        float msw = Input.GetAxis("Mouse ScrollWheel");

        if (msw != 0)
        {
            msw /= Mathf.Abs(msw);
            transform.position += msw * transform.forward;

            float h = terrain.SampleHeight(transform.position);
            if (h < 337f)
            {
                h = 337f;
            }
            
            float y = transform.position.y - h;

            if (y < 2f)
            {
                transform.position = new Vector3(transform.position.x, h + 2f, transform.position.z);
            }
            else if (y > 100f)
            {
                transform.position = new Vector3(transform.position.x, h + 100f, transform.position.z);
            }
        }
    }
}
