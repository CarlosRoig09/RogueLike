using UnityEngine;
public class EnemyWave 
{
    private readonly float _waveNumber;
    public float WaveNumber
    {
        get => _waveNumber;
    }
    private float _kamikazeeNumber;
    public float KamikazeeNumber
    {
        get => _kamikazeeNumber;
        set => _kamikazeeNumber = value;
    }
    private float _turretNumber;
    public float TurretNumber
    {
        get => _turretNumber;
        set => _turretNumber = value;
    }
    private bool _waveDefeated;
    public bool WaveDefeated
    {
        get => _waveDefeated;
        set => _waveDefeated = value;
    }
    public EnemyWave(float waveNumber, float kamikazeeNumber, float turretNumber)
    {
        _waveNumber = waveNumber;
        _kamikazeeNumber = kamikazeeNumber;
        _turretNumber = turretNumber;
        _waveDefeated = false;
    }
}
