using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.CompareTag("Destroyable")) {
            // Destroy(collision.gameObject);
            var rigidbody = gameObject.GetComponent<Rigidbody>();

            if (rigidbody != null) {
                rigidbody.AddForce(0f, 10f, 0f, ForceMode.Impulse);
            }
        }
    }
}
