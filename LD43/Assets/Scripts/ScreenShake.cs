using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public bool pistolShake;
    public bool rocketLauncherShake;
    public bool explosionShake;

    [SerializeField] private ScreenShakes Pistol;
    [SerializeField] private ScreenShakes RocketLauncher;
    [SerializeField] private ScreenShakes Explosion;
    

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private GameObject explosion;


    void Start()
    {
        originalPosition = transform.localPosition;
    }


    void Update()
    {
        if (pistolShake)
            StartCoroutine(PistolShake());

        if (rocketLauncherShake)
            StartCoroutine(RocketLauncherShake());

        if (explosionShake)
            StartCoroutine(ExplosionShake());
    }

    public IEnumerator PistolShake()
    {
        float duration = Pistol.GetDuration();
        float strength = Pistol.GetStrenght();
        float speed = Pistol.GetSpeed();
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {

            float x = Random.Range(-0.1f, 0.1f) * strength;
            float y = Random.Range(0f, 0.5f) * strength;

            targetPosition = new Vector3(x, y, -0.1f) + originalPosition;
            float move = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, move);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        
        pistolShake = false;
        transform.localPosition = originalPosition;
    }

    public IEnumerator RocketLauncherShake()
    {
        float duration = RocketLauncher.GetDuration();
        float strength = RocketLauncher.GetStrenght();
        float speed = RocketLauncher.GetSpeed();
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            float x = Random.Range(-0.05f, 0.05f) * strength;
            float y = Random.Range(0.3f, 0.3f) * strength;

            targetPosition = new Vector3(x, y, -0.3f) + originalPosition;
            float move = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, move);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {

            float move = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, move);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
               
        rocketLauncherShake = false;
        transform.position = originalPosition;
    }

    public IEnumerator ExplosionShake()
    {
        float duration = Explosion.GetDuration();
        float strength = Explosion.GetStrenght();
        float speed = Explosion.GetSpeed();

        explosion = GameObject.Find("Particle_GrenadeExplosion(Clone)"); //find explosion 


        Vector3 dir = explosion.transform.position - transform.position;
        dir = new Vector3(Mathf.Abs(dir.x), Mathf.Abs(dir.y), Mathf.Abs(dir.z));

        float explosionStrenght = strength / (dir.x + dir.z);

        //Debug.Log("explosionStrenght " + explosionStrenght);

        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * explosionStrenght;
            float y = Random.Range(-1f, 1f) * explosionStrenght;

            Vector3 newPos = new Vector3(x, y, 0) + originalPosition;

            transform.localPosition = newPos;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        explosionShake = false;
        transform.position = originalPosition;
    }



    [System.Serializable]
    public class ScreenShakes
    {
        [SerializeField] private float duration;
        [SerializeField] private float strenght;
        [SerializeField] private float speed;

        public float GetDuration()
        {
            return duration;
        }
        public float GetStrenght()
        {
            return strenght;
        }
        public float GetSpeed()
        {
            return speed;
        }
    }
}