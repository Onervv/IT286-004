using System.Collections;
using UnityEngine;

public class ParrySystem : MonoBehaviour
{
    public float parryWindow = 0.3f;
    public KeyCode parryKey = KeyCode.Mouse1;
    private bool isParrying = false;

    void Update()
    {
        if (Input.GetKeyDown(parryKey))
        {
            StartCoroutine(ParryWindow());
        }
    }

    IEnumerator ParryWindow()
    {
        isParrying = true;
        yield return new WaitForSeconds(parryWindow);
        isParrying = false;
    }

    public bool IsParrying()
    {
        return isParrying;
    }
}