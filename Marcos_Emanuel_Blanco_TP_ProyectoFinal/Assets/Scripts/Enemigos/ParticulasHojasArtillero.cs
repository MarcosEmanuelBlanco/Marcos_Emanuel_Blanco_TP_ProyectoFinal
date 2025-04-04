using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticulasHojasArtillero : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private float factorAleatorio;
    private ParticleSystem.RotationOverLifetimeModule rotacion;
    //private MaterialPropertyBlock propertyBlock;
    //[SerializeField] private Sprite[] seleccionSprites;

    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        rotacion = _particleSystem.rotationOverLifetime;
        //StartCoroutine(nameof(AlternarTexturaParticulas));
    }

    //private IEnumerator AlternarTexturaParticulas()
    //{
    //    yield return new WaitForSeconds(1.0f);
    //    int cambioParticula = Random.Range(0, 4);
    //    gameObject.GetComponent<ParticleSystem>().GetComponent<Material>().mainTexture = seleccionSprites[cambioParticula].texture;
    //    StartCoroutine(nameof(AlternarTexturaParticulas));
    //}

    void GenerarRotacionAleatoria()
    {
        factorAleatorio = Random.Range(-70.0f, 90.0f);
        rotacion.z = factorAleatorio;
    }

    // Update is called once per frame
    void Update()
    {
        GenerarRotacionAleatoria();
        //
    }
}
