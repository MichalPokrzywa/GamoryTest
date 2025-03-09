using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public int enemyHP = 100;
    public int damage = 20;
    public float speed = 3f;
    private Transform player;
    [SerializeField] 
    private EnemyVisualser visualser;
    void Start()
    {
        player = GameManager.Instance.GetPlayerTransform();
        enemyHP = Random.Range(50, 200);
        damage = Random.Range(10, 20);
        speed = Random.Range(3f, 6f);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    public void ModifyEnemy(int modifier)
    {
        enemyHP *= modifier;
        damage *= modifier;
    }
    public void Damage(int damage)
    {
        enemyHP -= damage;
        if (enemyHP <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            visualser.ShowDamage();
        }
    }
    public void CritDamage(int damage)
    {
        enemyHP -= damage;
        if (enemyHP <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            visualser.ShowCritDamage();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamagable damagable = collision.transform.GetComponent<IDamagable>();
            damagable?.Damage(damage);
            Destroy(gameObject);
        }
    }
}