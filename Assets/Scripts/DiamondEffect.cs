using UnityEngine;

public class DiamondEffect : MonoBehaviour
{
    public GameObject[] diamondParticles = new GameObject[4];
    private float timer = 0;
    public void ParticleTimerInit() => timer = 0;
    public void ParticleDisable(int idx, GameObject[] particles) => particles[idx].SetActive(false);
    public void ParticleTimer(int idx)
    {
        if (diamondParticles[idx].activeSelf == true)
        {
            ParticleDisable(idx, diamondParticles);
            diamondParticles[idx].SetActive(true);
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            diamondParticles[idx].SetActive(true);
        }

        if (timer >= 2) ParticleDisable(idx, diamondParticles);
    }
}
