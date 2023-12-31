using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float _timeBetweenWaves = 5f;

    private int _waveIndex = 0;
    private float _secondsToWaitWave = 0.5f;
    private string _waveText = "Wave: ";

    public TMP_Text waveCountdownText;

    #region ReworkedSpawnerVariables

    [SerializeField] private int _enemiesInitialNumber;
    private int _roundIndex = 1;
    private int _remainingEnemiesInWave;
    private bool _spawningEnemies = false;

    #endregion

    private void Awake()
    {
        if (waveCountdownText == null)
        {
            waveCountdownText = GameObject.Find("WaveCountdownTimer").GetComponent<TMP_Text>();
        }
    }

    void Update()
    {
        StartWave();
    }

    private void StartWave()
    {
        if (!_spawningEnemies)
        {
            if (_waveIndex >= 3)
            {
                _roundIndex++;

                if(_roundIndex > 3)
                {
                    JsonSaveSystem.Instance._level1Win = true;
                    _spawningEnemies = true;
                    UIWin.Instance.WinScreen();
                    return;
                }

                _waveIndex = 0;
            }
            StartCoroutine(SpawnWaveTest());
        }
    }

    IEnumerator SpawnWaveTest()
    {
        _spawningEnemies = true;
        _waveIndex++;
        _remainingEnemiesInWave = _enemiesInitialNumber;
        waveCountdownText.text = (_waveText + _waveIndex.ToString());

        while (_remainingEnemiesInWave > 0) // Al n�mero inicial de enemigos los divido en grupos aleatorios que no sean mayores a la mitad del n�mero inicial, ni a la cantidad de enemigos restantes
        {
            int enemiesInThisGroupOfTheWave = Mathf.Clamp(Random.Range(1, _enemiesInitialNumber / 2), 1, _remainingEnemiesInWave);
            _remainingEnemiesInWave -= enemiesInThisGroupOfTheWave;

            StartCoroutine(SpawnGroup(enemiesInThisGroupOfTheWave));
            yield return new WaitForSeconds(_secondsToWaitWave * 4); // Tiempo entre grupo y grupo
        }
        yield return new WaitForSeconds(_timeBetweenWaves);
        _spawningEnemies = false;
    }

    IEnumerator SpawnGroup(int enemiesAmount) // Spawnea al grupo completo antes de seguir con la oleada
    {
        for (int i = 0; i < enemiesAmount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(_secondsToWaitWave);
        }
    }

    private void SpawnEnemy()
    {
        if (Random.Range(0, 101) <= 100 - _waveIndex * 10 - (_roundIndex - 1) * 25)
        {
            EnemyBasicFactory.Instance.GetObjectFromPool();
        }
        else
        {
            EnemyHeavyFactory.Instance.GetObjectFromPool();
        }
    }   

    public void NextWaveEarly()
    {
        //_countdown = 0;
    }
}