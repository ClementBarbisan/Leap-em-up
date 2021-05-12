using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public List<GameObject> sprites;
    [SerializeField] private float _speed;
    private List<List<GameObject>> _background;
    [SerializeField]
    private int _parallaxLayer = 3;
    [SerializeField]
    private int _nbBack = 8;
    // Start is called before the first frame update
    void Start()
    {
        _background = new List<List<GameObject>>();
        for (int i = 0; i < _parallaxLayer; i++)
        {
            _background.Add(new List<GameObject>()); 
            for (int j = 0; j < _nbBack * (i + 1); j++)
            {
                GameObject obj = Instantiate(sprites[Random.Range(0, sprites.Count - 1)]);
                obj.GetComponent<SpriteRenderer>().sortingOrder = -i-1;
                obj.GetComponent<SpriteRenderer>().flipX = Random.Range(0, 1) == 0;
                _background[i].Add(obj);
                obj.transform.localScale /= (i + 1);
                obj.transform.position = new Vector3(j * (_background[i][_background[i].Count - 1].transform.localScale.x * 1.5f), i *  _background[i][_background[i].Count - 1].transform.localScale.y, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _background.Count; i++)
        {
            int j = 0;
            while (j < _background[i].Count)
            {
                _background[i][j].transform.Translate(_speed / (i + 1), 0, 0);
                if (_background[i][j].transform.position.x <= -0.4f)
                {
                    GameObject sprite = _background[i][j];
                    _background[i].Remove(sprite);
                    Destroy(sprite);
                    GameObject obj = Instantiate(sprites[Random.Range(0, sprites.Count - 1)]);
                    obj.GetComponent<SpriteRenderer>().sortingOrder = -i-1;
                    obj.GetComponent<SpriteRenderer>().flipX = Random.Range(0, 1) == 0;
                    _background[i].Add(obj);
                    obj.transform.localScale /= (i + 1);
                    obj.transform.position = new Vector3(0.5f, i *  _background[i][_background[i].Count - 1].transform.localScale.y, 0);
                }

                j++;
            }

            // while (garbage.Count > 0)
            // {
            //     _background[i].Remove(garbage[garbage.Count - 1]);
            //     // Destroy(garbage[garbage.Count - 1]);
            // }
            // garbage.Clear();
        }
    }
}
