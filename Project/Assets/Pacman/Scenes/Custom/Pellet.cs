using UnityEngine;

public class Pellet : MonoBehaviour
{
    //bobbing speed
    private const float speed = 2f;
    //bobbing amplitude
    private const float amplitude = 0.2f;

    public bool respawn;

    public void OnEaten()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed);
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(pos.x, newY, pos.z) * amplitude;
    }
}
