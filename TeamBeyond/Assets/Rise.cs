using UnityEngine;

public class Rise : Triggerable
{
    private Vector3 endPos;
    private Vector3 startPos;

    private bool triggered;
    private bool direction = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.endPos = transform.position;
        this.startPos = transform.position + new Vector3(0, -10000f, 0);
        transform.position = startPos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (triggered && !direction)
        {
            transform.position = Vector3.Lerp(transform.position, endPos, 0.1f);
        }
        else if (triggered && direction)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, 0.01f);
        }
    }

    public override void Trigger()
    {
        triggered = true;
        direction = !direction;
    }
}
