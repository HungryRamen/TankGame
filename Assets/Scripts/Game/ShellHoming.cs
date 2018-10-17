using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellHoming : MonoBehaviour
{
    private GameObject TankObj;
    public GameObject explotion;
    private float fTime;
    private float digree;
    void Awake()
    {
        TankObj = GameObject.Find("Tank");
        fTime = 1.0f;

        float rad = Mathf.Atan2(TankObj.transform.position.z - this.transform.position.z, TankObj.transform.position.y - this.transform.position.y);
        digree = rad * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(digree, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (TankObj != null)
        {
            fTime += Time.deltaTime;
            if (fTime >= 0.05f)
            {
                EffectCreate();
                fTime = 0.0f;
            }
            transform.eulerAngles = new Vector3(digree, 0, 0);
            this.transform.Translate(0.0f, 15.0f * Time.deltaTime, 0.0f);
            transform.eulerAngles = new Vector3(digree - 90, 0, 0);
            Vector3 vDir = this.transform.position - TankObj.transform.position;
            if (vDir.magnitude <= 1.5f)
            {
                EffectCreate();
                Destroy(gameObject);
                GameObject.Find("Canvas").GetComponent<GameMain>().HpMinus();
            }
            if (this.transform.position.y <= -10.0f)
                Destroy(gameObject);
        }
        else
        {
            EffectCreate();
            Destroy(gameObject);
        }

    }

    void EffectCreate()
    {
        GameObject go = Instantiate(explotion, transform.position, this.transform.rotation) as GameObject;
        ParticleSystem ExplosionParticles = go.GetComponent<ParticleSystem>();
        ExplosionParticles.Play();
        ParticleSystem.Destroy(go, ExplosionParticles.main.simulationSpeed);
    }
}
