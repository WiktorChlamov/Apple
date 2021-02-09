using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameObjects;

public class RandomKnifesAndApple : MonoBehaviour
{   
    private CircleCollider2D wheelCollider;
    private Transform[] selectedPoints;
    void Start()
    {
       wheelCollider = gameObjects.Wheel.GetComponent<CircleCollider2D>();
       int numStartKnifes = UnityEngine.Random.Range(1, 4);
       if(SpawnApple(gameObjects.ApplePB.GetComponent<FruitPB>().Fruit.Chance))
        {
            selectedPoints = new Transform[numStartKnifes+1];
            selectedPoints[0] = gameObjects.PointsOfRandom[UnityEngine.Random.Range(0, gameObjects.PointsOfRandom.Length)];
            InstantiateAndRotate(selectedPoints[0], gameObjects.ApplePB,out Transform apple);
            gameObjects.Apple = apple.gameObject;
        }
        else
        {
            selectedPoints = new Transform[numStartKnifes];
        }
        CreateKnifes(numStartKnifes);

        Debug.Log(gameObjects.KnifesInWood.Count);
    }
    private void CreateKnifes(int numStartKnifes)
    {
        for (int i = selectedPoints.Length- numStartKnifes; i< selectedPoints.Length; i++)
        {
            Transform point = gameObjects.PointsOfRandom[UnityEngine.Random.Range(0, gameObjects.PointsOfRandom.Length)];
            while (Array.Exists(selectedPoints, poi => point == poi))
            {
                point = gameObjects.PointsOfRandom[UnityEngine.Random.Range(0, gameObjects.PointsOfRandom.Length)];
            }
            selectedPoints[i] = point;
        }
        for (int i =selectedPoints.Length-numStartKnifes; i< selectedPoints.Length; i++)
        {
            InstantiateAndRotate(selectedPoints[i], gameObjects.KnifePB, out Transform knife);
            knife.GetComponent<PolygonCollider2D>().enabled = true;
            gameObjects.KnifesInWood.Add(knife);
        }
    }
    private void InstantiateAndRotate(Transform rotateToThisObject, GameObject gameObjectToRotate, out Transform knife)
    {
        GameObject objectToRotate = Instantiate(gameObjectToRotate, wheelCollider.ClosestPoint(rotateToThisObject.position), Quaternion.identity);
        knife = objectToRotate.transform;
        Debug.Log(objectToRotate);
        Vector2 direction = wheelCollider.ClosestPoint(rotateToThisObject.position) - (Vector2)wheelCollider.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90;
        objectToRotate.GetComponent<Rigidbody2D>().rotation = angle; 
    }
    private bool SpawnApple(int chance) =>chance > UnityEngine.Random.Range(0, 100);
    
}
