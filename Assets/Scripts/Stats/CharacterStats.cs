using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth { get; private set;}
    public Stat damage;
    public Stat defense;

    void Awake(){
        currentHealth = maxHealth;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.T)){
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage){

        damage -= defense.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + "took" + damage + "damage.");

        if(currentHealth <= 0){

            Die();
        }
    }

    public virtual void Die(){
        //Override 
        Debug.Log(transform.name + "died.");
    }
}
