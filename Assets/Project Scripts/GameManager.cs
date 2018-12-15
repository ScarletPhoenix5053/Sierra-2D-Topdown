using UnityEngine;
using System.Collections;
using Sierra.Unity2D.TopDown;

public class GameManager : MonoBehaviour
{
    public AttackData data;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GetComponent<Health>().RemoveHp(data);
        }
    }
}
