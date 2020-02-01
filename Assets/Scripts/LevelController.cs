using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : Singleton<LevelController>
{
    [SerializeField] GameObject heroHealthBar;
    [SerializeField] GameObject enemyHealthBar;
    [SerializeField] float heroHealth = 0;
    [SerializeField] float enemyHealth = 0;
    [SerializeField] float heroHealthInterval = 100;
    [SerializeField] float enemyHealthInterval = 100;
    [SerializeField] string badEndSceneName = "BadEnd";
    [SerializeField] string finalFightSceneName = "FinalFight";
    int heartCount;

    public void DealDamageToHero()
    {
        heroHealthBar.GetComponent<HealthControl>().TakeDamage(heroHealthInterval);
		heroHealth -= heroHealthInterval;
		if (heroHealth <= 0)
        {
            LoadBadEndScene();
        }
    }
    public void AddHealthToHero()
    {
        heroHealthBar.GetComponent<HealthControl>().AddHealth(heroHealthInterval);
		heroHealth += heroHealthInterval;
	}
    public void DealDamageToEnemy()
    {
        enemyHealthBar.GetComponent<HealthControl>().TakeDamage(heroHealthInterval);
		enemyHealth += enemyHealthInterval;
    }
    public void AddHealthToEnemy()
    {
        enemyHealthBar.GetComponent<HealthControl>().AddHealth(heroHealthInterval);
		enemyHealth += enemyHealthInterval;
    }

    public void LoadBadEndScene()
    {
		AudioCenter.Instance.Play("ZhujueDie");
        SceneManager.LoadScene(badEndSceneName);
    }
    public void LoadFinalFightScene()
    {
        SceneManager.LoadScene(finalFightSceneName);
		Destroy(FindObjectOfType<HealthControl>());
    }
}
