using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip m_coinClip;
    [SerializeField, Min(0)] float m_rotateSpeed;
    [SerializeField, Min(0)] float m_distanceToPick;
    Transform m_playerTransform;
    Vector3 m_coinPosition;
    // Start is called before the first frame update
    void Start()
    {
        m_playerTransform = PlayerManager.Instance.transform;
        m_coinPosition = transform.position;
        m_coinPosition.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(m_rotateSpeed * Time.deltaTime * Vector3.up, Space.World);

        Vector3 playerPosition = m_playerTransform.position;
        playerPosition.y = m_coinPosition.y;

        float distance = Vector3.Distance(m_coinPosition, playerPosition);
        if (distance <= m_distanceToPick)
        {
            Pick();
        }
    }

    void Pick()
    {
        AudioSource.PlayClipAtPoint(m_coinClip, transform.position);
        PlayerManager.Instance.AddCoin();
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, m_distanceToPick);
    }
}
