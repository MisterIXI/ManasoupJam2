using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaseTest : MonoBehaviour
{
    private const float X_START = 0;
    private const float X_END = 10;
    [SerializeField] private Transform _c1;
    [SerializeField] private Transform _c2;
    [SerializeField] private Transform _c3;
    float c1, c2, c3;
    // Update is called once per frame
    void FixedUpdate()
    {
        // input: -1 und 1 abwechselnd
        float input = 1 - 2 * Mathf.Round(Mathf.PingPong(Time.time + 1.5f, 1));
        // Debug.Log(Time.time + " " + Mathf.Round(input));

        _c1.Translate(Vector3.right * 5 * input * Time.fixedDeltaTime);

        // c2: beginnt mit 0, lerpt von sich selbst zu input mit hälfte von weg
        // c2 = Mathf.Lerp(c2, input, 0.5f);
        c2 = Mathf.MoveTowards(c2, input, 0.04f );
        Debug.Log("C2: " + c2);
        _c2.Translate(Vector3.right * 5 * c2 * Time.fixedDeltaTime);


        c3 = Mathf.Lerp(c3, input, Time.fixedDeltaTime * 0.5f);
        _c3.Translate(Vector3.right * 5 * c3 * Time.fixedDeltaTime);

    }
}
