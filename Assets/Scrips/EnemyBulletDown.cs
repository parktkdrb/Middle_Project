using UnityEngine;
public class EnemyBulletDown : MonoBehaviour
{
    public float Speed1 = 10.0f;

    void Start()
    {

    }


    void Update()
    {
        float BulletSpeed1 = Speed1 * Time.deltaTime;
        transform.Translate(Vector3.up * BulletSpeed1);
        if (transform.position.y < 23)
        {
            InitPosition();
        }
        if (transform.position.y < -9.5f && transform.position.y > -5.5f)
        {
            if (Input.GetKey("d"))
            {
                InitPosition();
                Speed1 = Speed1 + 1;
            }
        }
    }
    void InitPosition()
    {
        int range = (int)Random.Range(0, 1);
        transform.position = new Vector3(range, -23, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            InitPosition();
        }
    }
    public void Right()
    {
        if (transform.position.y < 9.5f && transform.position.y > -5.5f)
        {
            InitPosition();
            Speed1 = Speed1 + 1;
        }


    }
}
