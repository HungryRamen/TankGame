using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellDown : MonoBehaviour
{
    private GameObject TankObj;
    public GameObject explotion;
    private GameObject UI;

    void Awake()
    {
        TankObj = GameObject.Find("Tank");
        Quaternion vRot = transform.rotation;
        UI = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (TankObj != null)
        {
            this.transform.Translate(0.0f, UI.GetComponent<GameMain>().GetBulletSpeed * Time.deltaTime, 0.0f);
            Vector3 vDir = this.transform.position - TankObj.transform.position;
            if (vDir.magnitude <= 1.5f)
            {
                EffectCreate();
                Destroy(gameObject);
                GameObject.Find("Canvas").GetComponent<GameMain>().HpMinus();
            }
            if (this.transform.position.y <= 0.0f)
            {
                EffectCreate();
                Destroy(gameObject);
            }
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
