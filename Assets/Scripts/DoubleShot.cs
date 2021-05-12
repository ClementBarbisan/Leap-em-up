using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleShot : PreBonus
{
    private float _timeElapsed = 0;
    public float speedFire = 0.2f;
    public GameObject ammoPrefab;
    public List<TypeAmmo> weapons;

    private Ship currentShip;
    // Start is called before the first frame update
    void Start()
    {
        currentShip = GetComponentInParent<Ship>();
        if (currentShip == null)
        {
            Debug.LogError("No ship for this bonus");
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeElapsed >= speedFire)
        {
            _timeElapsed = 0;
            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].Shot(ammoPrefab, transform.position);
            }
        }

        _timeElapsed += Time.deltaTime;
        time -= Time.deltaTime;
        if (time < 0)
            Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bonus") && other.gameObject.GetComponent<Bonus>().currentType == BonusType.DoubleShot)
        {
            time += other.gameObject.GetComponent<Bonus>().time;
            Destroy(other.gameObject);
        }
    }
}