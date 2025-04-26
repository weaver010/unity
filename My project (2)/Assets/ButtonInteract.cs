using UnityEngine;

public class ButtonInteract : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 initialPosition;
    private Vector3 endPosition;
    private float t, s;

    private bool hasTouched;
    private bool leftHasTouched;
    private bool rightHasTouched;

    public bool on = true; // Light is on by default

    public float timeLower;
    public float timeUpper;
    public float distance;
    public float distanceTrigger;

    public GameObject switchButton;
    public bool switchTrigger;

    public Light sceneLight; // Light reference

    public void OnTriggerEnter(Collider col)
    {
        // Detect hand touches
        if (col.name.Contains("hand")) hasTouched = true;
        if (col.name.Contains("hands:b_l")) leftHasTouched = true;
        else if (col.name.Contains("hands:b_r")) rightHasTouched = true;

        // Detect button hit with Switch object
        if (col.name.Contains("Switch"))
        {
            switchTrigger = true;
            on = !on;
            if (sceneLight != null)
            {
                sceneLight.enabled = on;
            }
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.name.Contains("hands:b_l")) leftHasTouched = false;
        else if (col.name.Contains("hands:b_r")) rightHasTouched = false;
        if (!(leftHasTouched && rightHasTouched)) hasTouched = false;

        if (col.name.Contains("Switch"))
        {
            switchTrigger = false;
        }
    }

    void Start()
    {
        hasTouched = leftHasTouched = rightHasTouched = switchTrigger = false;

        // Light starts ON by default
        if (sceneLight != null)
        {
            sceneLight.enabled = true;
        }

        initialPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        endPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - distance, transform.localPosition.z);
        startPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
    }

    void Update()
    {
        // If hand touching, move button down
        if (hasTouched)
        {
            s = 0;
            t += Time.deltaTime / timeLower;
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
        }
        // If not touching, move button back up
        else
        {
            t = 0;
            s += Time.deltaTime / timeUpper;
            startPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
            transform.localPosition = Vector3.Lerp(startPosition, initialPosition, s);
        }
    }
}
