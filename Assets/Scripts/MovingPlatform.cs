using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform m_ThePath;
    [SerializeField] private Transform[] m_WayPoints;
    [SerializeField] private float m_MoveSpeed;

    private int m_CurWayPointIndex;

    private void Start()
    {
        m_ThePath.position = m_WayPoints[0].position;
    }

    private void Update()
    {
        m_ThePath.position = Vector3.MoveTowards(m_ThePath.position, m_WayPoints[m_CurWayPointIndex].position, m_MoveSpeed * Time.deltaTime);
        if(m_ThePath.position == m_WayPoints[m_CurWayPointIndex].position)
        {
            m_CurWayPointIndex++;
            if(m_CurWayPointIndex >= m_WayPoints.Length)
            {
                m_CurWayPointIndex = 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (m_WayPoints.Length <= 1)
        {
            return;
        }
        for(int i = 0; i < m_WayPoints.Length-1; i++)
        {
            Gizmos.DrawLine(m_WayPoints[i].position, m_WayPoints[i+1].position);
        }
        Gizmos.DrawLine(m_WayPoints[0].position, m_WayPoints[m_WayPoints.Length-1].position);
    }
}
