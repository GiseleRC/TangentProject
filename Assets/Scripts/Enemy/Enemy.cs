using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour, IPooleableObject
{
    //HACER------ pasar a flyweights
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _health = 100f;

    private Transform _target;
    private int _waypointsIndex = 0;

    [SerializeField] private TypeEnemy _typeEnemy;

    public enum TypeEnemy
    {
        Basic,
        Heavy
    }

    void Start()
    {
        _target = Waypoints.points[0];

        if (_typeEnemy == TypeEnemy.Basic)// cuando se apliue flyweight ya no va a ser necesario hacer esta distincion
        {
            _health = 100f;
        }
        else if (_typeEnemy == TypeEnemy.Heavy)
        {
            _health = 150f;
        }
    }

    void Update()
    {
        Vector3 _dir = _target.position - transform.position;
        transform.Translate(_dir.normalized * _speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, _target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if(_waypointsIndex >= Waypoints.points.Length -1)
        {
            Die();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }
        _waypointsIndex++;
        _target = Waypoints.points[_waypointsIndex];
    }

    public void Reset()
    {
        _target = Waypoints.points[0];
        _waypointsIndex = 0;
        transform.position = new Vector3(58.5f, 2.5f, 20.1599998f);
    }

    public static void TurnOn(Enemy enemy)
    {
        enemy.Reset();
        enemy.gameObject.SetActive(true);
    }

    public static void TurnOff(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    public void TakeDamage(int damageAmounth)
    {
        _health -= damageAmounth;

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (_typeEnemy == TypeEnemy.Basic)
        {
            EnemyBasicFactory.Instance.ReturnObjectToPool(this);
        }
        else if (_typeEnemy == TypeEnemy.Heavy)
        {
            EnemyHeavyFactory.Instance.ReturnObjectToPool(this);
        }
    }
}