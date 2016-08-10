using UnityEngine;
using System.Collections;

public class AutoDestroyParticleSystem : MonoBehaviour {

    ParticleSystem m_particleSystem;

	void Start () {
        m_particleSystem = GetComponent<ParticleSystem>();
    }
	

	void Update () {
        if (!m_particleSystem.IsAlive())
            Destroy(gameObject);
	}
}
