using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    [SerializeField]
    private float Delay;

    void Update()
    {
        if (Delay > 0) Delay -= Time.deltaTime;
        else Destroy(gameObject);
    }
}
