using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

    public bool shakeScreen;
    public bool pistolShake;
    public bool rocketLauncherShake;
    public bool explosionShake;

    public float duration1;
    public float strength1;
    public float duration2;
    public float speed;

    private Vector3 originalPosition;

    private Vector3 targetPosition;

    private GameObject explosion;


    void Start ()
    {
        originalPosition = transform.position;
        targetPosition = new Vector3(originalPosition.x, originalPosition.y, 100f);
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
    public IEnumerator CameraShake1()
    {
        //Vector3 originalPosition = transform.position;
        float elapsedTime = 0.0f;

        Debug.Log(originalPosition);
       

            while (elapsedTime < duration1)
            {
                //Debug.Log("ElapsedTime: " + elapsedTime);
                float x = Random.Range(-0.1f, 0.1f) * strength1;
                float y = Random.Range(-0.1f, 0.1f) * strength1;
                transform.localPosition = new Vector3(x, y, 0) + originalPosition;

                elapsedTime += Time.deltaTime;

                yield return null;
            }
        

        shakeScreen = false;
        transform.position = originalPosition;
        Debug.Log("END" + originalPosition);


    }

    public IEnumerator CameraShake2()
    {
        //Vector3 originalPosition = transform.position;
        Debug.Log("Original position: " + originalPosition);
        float elapsedTime = 0.0f;


        
            //Debug.Log("ElapsedTime: " + elapsedTime);

            //Vector3 targetPosition = new Vector3(0, 0, -1);
            float velocity = speed * Time.deltaTime;

        while (elapsedTime < duration1)
        {
            
            float move = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, move);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        elapsedTime = 0.0f;

        while (elapsedTime < duration1)
        {

            float move = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, move);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        //while (transform.position != targetPosition)
        //{
        //    float move = speed * Time.deltaTime;
        //    transform.position = Vector3.MoveTowards(transform.position, targetPosition, move);

        //    elapsedTime += Time.deltaTime;

        //    yield return null;

        //}


        shakeScreen = false;
        //transform.position = new Vector3 (0, 0, 0);
        Debug.Log("END" + originalPosition);


    }


    public IEnumerator PistolShake()
    {
        float duration = 0.1f;
        float strength = 0.2f;
        float speed = 3f;
        float elapsedTime = 0.0f;

        Debug.Log(originalPosition);


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

        
        shakeScreen = false;
        pistolShake = false;
        transform.position = originalPosition;
    }

    public IEnumerator RocketLauncherShake()
    {
        float duration = 0.2f;
        float strength = 0.2f;
        float speed = 3f;
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


        shakeScreen = false;
        rocketLauncherShake = false;
        transform.position = originalPosition;
        Debug.Log("END" + originalPosition);
    }

    public IEnumerator ExplosionShake()
    {        
        explosion = GameObject.Find("Expl");
        

        Vector3 dir = explosion.transform.position - transform.position;
        dir = new Vector3(Mathf.Abs(dir.x), Mathf.Abs(dir.y), Mathf.Abs(dir.z));

        float explosionStrenght = strength1 / (dir.x + dir.z);

        Debug.Log("explosionStrenght " + explosionStrenght);

        float elapsedTime = 0.0f;

        while (elapsedTime < duration1)
        {
            float x = Random.Range(-1f, 1f) * explosionStrenght;
            float y = Random.Range(-1f, 1f) * explosionStrenght;

            Vector3 newPos = new Vector3(x, y, 0) + originalPosition;

            transform.localPosition = newPos;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        shakeScreen = false;
        explosionShake = false;
        transform.position = originalPosition;
        Debug.Log("END" + originalPosition);
    }
}


