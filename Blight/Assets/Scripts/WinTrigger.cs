using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameObject winUI; // Assign a UI panel or text to display "You Win!"

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("You Win!");
            if (winUI != null)
                winUI.SetActive(true);

            // Optional: freeze time to simulate ending
            Time.timeScale = 0f;

            // Optional: quit if it's a standalone build
#if UNITY_STANDALONE || UNITY_WEBGL
            StartCoroutine(QuitAfterDelay(2f));
#endif
        }
    }

    private System.Collections.IEnumerator QuitAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Application.Quit();
    }
}
