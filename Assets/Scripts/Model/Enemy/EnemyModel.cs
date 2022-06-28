using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyModel : CharacterModel
{

	public GameObject FloatingTextPrefab;
    protected SpawnManager spawnManager = null;

    protected GameManager gameManager;

    public bool dead = false;

    public float health;
    public float maxHealth;

    public GameObject healthBarUI;
    public Slider healthSlider;
    bool healthBarActive = false;


    public virtual void Start() {
      gameManager = GameManager.instance;

      SpawnManager[] obj = FindObjectsOfType<SpawnManager>();
      if (obj.Length != 0) {
          spawnManager = obj[0];
      }

      health = 2f * gameManager.getDifficulty();
      maxHealth = health;
      healthSlider.value = 1;
      if (healthBarUI != null)
        healthBarUI.SetActive(false);
    }

    override public void TakeDamage(float damage) {
		if(FloatingTextPrefab!= null){
			ShowFloatingText(damage);
		}

        health -= damage;
        healthSlider.value = health/maxHealth;

        if (healthBarUI != null && !healthBarActive) {
          healthBarActive = true;
          healthBarUI.SetActive(true);
        }

        if (health <= 0) {
            if (healthBarUI != null) healthBarUI.SetActive(false);
            Die();
        }
    }

	void ShowFloatingText(float damage){
		var text = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
		text.GetComponent<TextMesh>().text = damage.ToString();
	}

    public virtual void Die() {
      if (spawnManager != null){
          spawnManager.enemyDied(this.gameObject);
      }

      
      if (gameManager != null) {
        gameManager.addEnemyKilled();
      }

      Destroy(gameObject);
    }
}
