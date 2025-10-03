using UnityEngine;

[CreateAssetMenu(fileName = "newEnemySO", menuName = "ScriptableObjects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public int maxHealth;
    public float speed_walk;
    public float speed_run;
    public int damage;
    
}
