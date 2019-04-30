using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Settings")]
    public bool lastLevel;

    [Header("Debug")]
    [SerializeField] int m_ShieldEnemyCount;
    [SerializeField] int m_SpikeEnemyCount;
    [SerializeField] int m_ArmorEnemyCount;
    [SerializeField] bool m_IsChangingLevel;
    [SerializeField] LevelTransition m_LevelTransition;

    private void Start()
    {
        m_LevelTransition = GameObject.Find("LevelTransition").GetComponent<LevelTransition>();
    }

    void Update()
    {
        m_ShieldEnemyCount = GameObject.FindGameObjectsWithTag("ShieldEnemy").Length;
        m_SpikeEnemyCount = GameObject.FindGameObjectsWithTag("SpikeEnemy").Length;
        m_ArmorEnemyCount = GameObject.FindGameObjectsWithTag("ArmorEnemy").Length;
        if (m_ShieldEnemyCount + m_SpikeEnemyCount + m_ArmorEnemyCount == 0)
        {
            if (lastLevel)
            {
                EndGame();
            }
            else
            {
                Win();
            }
        }
    }

    public void Win()
    {
        if (!m_IsChangingLevel)
        {
            m_LevelTransition.MoveToNextScene();
            m_IsChangingLevel = true;
        }
    }

    public void Lose()
    {
        // Add This
    }

    public void EndGame()
    {
        // Add This
    }
}
