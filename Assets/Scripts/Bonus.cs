using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BonusType
{
    Shield,
    DoubleShot
}

public class Bonus : Spawnable
{
    public BonusType currentType;
    public GameObject bonusChild;

    public float time;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        this.gameObject.tag = "Bonus";
        this.gameObject.layer = 13;
        UIScript.Instance.bonus--;
        // this.transform.localScale = new Vector3(0.015f, 0.015f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyShip"))
        {
            if (this.currentType == BonusType.Shield && other.gameObject.GetComponentInChildren<Shield>())
            {
                other.gameObject.GetComponentInChildren<Shield>().time += time;
            }
            else if (this.currentType == BonusType.DoubleShot && other.gameObject.GetComponentInChildren<DoubleShot>())
            {
                other.gameObject.GetComponentInChildren<DoubleShot>().time += time;
            }
            else
            {
                GameObject obj = Instantiate(bonusChild, other.transform);
                obj.SetActive(true);
            }

            Destroy(this.gameObject);
        }
    }
}
