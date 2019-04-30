using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] Transform m_Transform;

    void Start()
    {
        m_Transform = transform;

        StartCoroutine(TransitionAnimation());
    }

    public void MoveToNextScene()
    {
        MoveToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MoveToScene(int index)
    {
        StartCoroutine(ChangeScene(index));
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    IEnumerator ChangeScene(int index)
    {
        float t = 0;
        Vector3 startPos = m_Transform.position;
        Vector3 targetPos = new Vector3(0, 0, 0);
        while (t < 1)
        {
            m_Transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
        }

        SceneManager.LoadScene(index);
        print("Loading Scene: " + index);
    }

    IEnumerator TransitionAnimation()
    {
        m_Transform.position = new Vector3(0, 0, 0);

        float t = 0;
        Vector3 startPos = m_Transform.position;
        Vector3 targetPos = new Vector3(42, 0, 0);
        while (t < 1)
        {
            m_Transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
        }

        m_Transform.position = new Vector3(-42, 0, 0);
    }
}
