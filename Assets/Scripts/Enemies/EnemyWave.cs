using UnityEngine;
public class EnemyWave 
{
    private readonly float _waveNumber;
    public int WaveNumber
    {
        get => _waveNumber;
    }
    private float[] _kamikazeeNumber;
    public float[] KamikazeeNumber
    {
        get => _kamikazeeNumber;
        set => _kamikazeeNumber = value;
    }
    private float[] _turretNumber;
    public float[] TurretNumber
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
    public EnemyWave(int waveNumber, float[] kamikazeeNumber, float[] turretNumber)
    {
        _waveNumber = waveNumber;
        _kamikazeeNumber = kamikazeeNumber;
        _turretNumber = turretNumber;
        _waveDefeated = false;
    }

    public float[] CallWave()
    {
        float[] waves = new float[0];
        float extraKamikazee;
        float extraTurret;
        for (int i = 0; i < WaveNumber; i++)
        {
            extraKamikazee = 0;
            extraTurret = 0;
            System.Array.Resize(ref waves, waves.Length + 2);
            if (Random.Range(1, 100) <= 20)
            {
                extraKamikazee = Random.Range(-1, 3);
                extraTurret = Random.Range(-1, 3);
            }
            waves[waves.Length - 2] = _kamikazeeNumber[WaveNumber] + extraKamikazee;
            waves[waves.Length - 1] = _turretNumber[WaveNumber] + extraTurret;
        }
        return waves;
    }
}
