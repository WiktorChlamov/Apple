using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Settings;

public class Knife : MonoBehaviour
{
    public bool Fly { get; set; } = false;
    public bool Rotate { get; set; } = false;
    void FixedUpdate()
    {
        if (Fly)
        {
           transform.position += new Vector3(0,settings.KnifeSpeed);
        }
        if (Rotate)
        {
            transform.Rotate(0, 0, 10);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Knife _))
        {   if (gameObject.GetComponent<PolygonCollider2D>().isTrigger)
            {
                Fly = false; Rotate = true;
                Vector2 vector2 = gameObject.transform.position - collision.transform.position;
                Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
                rb.AddForce(vector2 * 150);
                rb.gravityScale = 1;
                UiKnivesAndScoreEvents.uiKnivesAndScore.OnKnifeHit.Invoke();
            }
            return;
        }
        else if (collision.gameObject.TryGetComponent(out FruitPB _))
        {
            UiKnivesAndScoreEvents.uiKnivesAndScore.appleHitEvent.Invoke();
            collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            GameObjects.gameObjects.Apple = null;
            Destroy(collision.gameObject, 5f);
            return;
           
        }
        else
        {
            if (Fly)
            {
                UiKnivesAndScoreEvents.uiKnivesAndScore.onWheelHitEvent.Invoke();
            }
            Fly = false;
            gameObject.GetComponent<PolygonCollider2D>().isTrigger = false;
            transform.SetParent(collision.transform);
            
        }
    }
}
