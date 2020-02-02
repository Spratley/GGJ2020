﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class AICharacter : MonoBehaviour, IShootable
{
    public float health;
    [Range(1, 100)]
    public float maxHealth;

    public bool startDead;

    public bool isDead
    {
        get
        {
            return dead;
        }

        set
        {
            dead = value;
            if (dead)
                Die();
            else
                Revive();
        }
    }
    private bool dead;

    public float getUpSpeed;

    private Rigidbody[] bodyBodies;
    private Vector3[] bodyInitialPos;

    private void Start()
    {
        health = maxHealth;

        bodyBodies = gameObject.GetComponentsInChildren<Rigidbody>();
        SetKinematic(true);

        if (startDead)
            TakeDamage(maxHealth);
    }

    private void Update()
    {
        //TakeDamage(Time.deltaTime);
        //
        //if (isDead && Input.GetKeyDown(KeyCode.E))
        //    isDead = !isDead;
    }

    public float GetHealth()
    {
        return health;
    }

    public void TakeDamage(float amount)
    {
        health = Mathf.Max(0, health - amount);

        if (health <= 0)
            isDead = true;
    }

    public void HealDamage(float amount)
    {
        health = Mathf.Min(maxHealth, health + amount);

        if (health > 0 && isDead)
            isDead = false;
    }

    protected virtual void Revive()
    {
        SetKinematic(true);
        ResetBodies();

        health = maxHealth;
    }

    protected virtual void Die()
    {
        SetKinematic(false);
    }

    void SetKinematic(bool isKinematic)
    {
        if (bodyBodies == null)
            return;

        foreach (var body in bodyBodies)
        {
            body.isKinematic = isKinematic;
        }
    }

    void ResetBodies()
    {
        if (bodyBodies == null)
            return;

        StartCoroutine(GetUp(getUpSpeed));
    }

    IEnumerator GetUp(float duration)
    {
        float percent = 0;

        var posOffset = bodyBodies[0].transform.position - transform.position;
        posOffset.y = 0;

        transform.position += posOffset;
        transform.GetChild(0).GetChild(0).position -= posOffset;

        Quaternion[] rotations = new Quaternion[bodyBodies.Length];
        Vector3 startPos = bodyBodies[0].transform.localPosition;

        for (int i = 0; i < bodyBodies.Length; i++)
        {
            rotations[i] = bodyBodies[i].transform.rotation;
        }

        while (percent < 1)
        {
            percent += Time.deltaTime / duration;

            if (percent > 1)
            {
                percent = 1;
            }

            bodyBodies[0].transform.localPosition = Vector3.Lerp(startPos, Vector3.zero, percent);

            for (int i = 0; i < bodyBodies.Length; ++i)
            {
                bodyBodies[i].transform.rotation = Quaternion.Slerp(rotations[i], Quaternion.identity, percent);
            }

            yield return null;
        }

        bodyBodies[0].transform.localPosition = Vector3.zero;

        foreach (var body in bodyBodies)
        {
            body.transform.rotation = Quaternion.identity;
        }
        
    }

}
