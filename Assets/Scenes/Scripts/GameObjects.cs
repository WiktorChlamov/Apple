using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjects : MonoBehaviour
{
    public static GameObjects gameObjects;
    [SerializeField] private Transform wheel,knifeSpawnPoint,expPoint, particle;
    [SerializeField] private GameObject knifePB, applePB, appleObj;
    [SerializeField] private Transform[] points, woodPieces;
    [SerializeField] private AudioClip knifeThrow,wheelHitting,appleHitting,knifeHitting;
    [SerializeField] private GameObject music;
    [SerializeField] private AudioClip[] musics;
    [SerializeField] private AudioSource sound;


    public Transform Wheel { get => wheel; set => wheel = value; }
    public Transform[] PointsOfRandom { get => points; set => points = value; }
    public GameObject KnifePB { get => knifePB; set => knifePB = value; }
    public GameObject ApplePB { get => applePB; set => applePB = value; }
    public Transform KnifeSpawnPoint { get => knifeSpawnPoint; set => knifeSpawnPoint = value; }
    public Transform[] WoodPieces { get => woodPieces; set => woodPieces = value; }
    public Transform ExpPoint { get => expPoint; set => expPoint = value; }
    public List<Transform> KnifesInWood { get; set; } = new List<Transform>();
    public GameObject Apple { get => appleObj; set => appleObj = value; }
    public Transform Particle { get => particle; set => particle = value; }
    public GameObject Music { get => music; set => music = value; }
    public AudioClip[] Musics { get => musics; set => musics = value; }
    public AudioClip KnifeThrow { get => knifeThrow; set => knifeThrow = value; }
    public AudioClip WheelHitting { get => wheelHitting; set => wheelHitting = value; }
    public AudioClip AppleHitting { get => appleHitting; set => appleHitting = value; }
    public AudioClip KnifeHitting{ get => knifeHitting; set => knifeHitting = value; }
    public AudioSource Sound { get => sound; set => sound = value; }

    private void Awake()
    {
        gameObjects = this;
    }
}
