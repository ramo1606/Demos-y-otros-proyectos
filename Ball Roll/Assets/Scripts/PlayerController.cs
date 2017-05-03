using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public Text counterText;
    public Text winText;

    private Rigidbody rb;
    private int counter;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent < Rigidbody >();
        counter = 0;
        SetCountText();
        winText.text = "";
    }

    // FixedUpdate updates the phisics
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            counter += 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        counterText.text = "Count: " + counter.ToString();

        if(counter >= 12)
        {
            winText.text = "You Win";
        }
    }
}
