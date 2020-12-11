using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Camera cam;

    public ButtonManager buttonManager;

    public LayerMask groundLayer;

    public UnityEngine.AI.NavMeshAgent playerAgent;

    public RectTransform Energybar;

    public GameObject particleMan;

    private float totalEnergyWidth;

    private float maxEnergy = 10;

    private float healthBarSize;

    private float energy;

    private bool ded = false;

    private void Start()
    {
        energy = 0;
        healthBarSize = Energybar.localScale.x;
    }
    void Awake()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ded)
        {
            if (Input.GetMouseButtonDown(1))
            {
                playerAgent.SetDestination(GetPointUnderCursor());
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Boost();
            }
            else
            {
                UnBoost();
            }

            float healthPercentage = Mathf.Clamp01(energy / maxEnergy); // check value is between 0 and 1

            Energybar.localScale = new Vector3(healthPercentage * healthBarSize, Energybar.localScale.y, Energybar.localScale.z);
        }
    }

    private Vector3 GetPointUnderCursor()
    {
        Vector2 screenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(screenPosition);

        RaycastHit hitPosition;

        Physics.Raycast(mouseWorldPosition, cam.transform.forward, out hitPosition, 100, groundLayer);

        return hitPosition.point;
    }

    private void Eat()
    {
        Debug.Log("Om nom nom");
        AddEnergy(3);
    }

    private void AddEnergy(int energy)
    {
        this.energy += energy;
    }

    private void Boost()
    {
        if (energy > 0)
        {
            this.playerAgent.speed = 10;
            this.playerAgent.acceleration = 10;
            particleMan.GetComponent<ParticleSystem>().enableEmission = true;
            energy -= Time.deltaTime;
        }
        else
        {
            Freeze();
        }
    }

    private void Freeze()
    {
        this.playerAgent.speed = 0;
        this.playerAgent.acceleration = 0;
        this.playerAgent.isStopped = true;
        this.playerAgent.velocity = Vector3.zero;
    }

    private void UnBoost()
    {
        this.playerAgent.speed = 2;
        this.playerAgent.acceleration = 2;
        this.playerAgent.isStopped = false;
        particleMan.GetComponent<ParticleSystem>().enableEmission = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        switch (other.gameObject.layer)
        {
            case 10:
                Destroy(other.gameObject);
                Eat();
                break;
            case 11:
                Freeze();
                ded = true;
                Debug.Log("You Died");
                buttonManager.ShowRestartLevelButtoN();
                break;
            case 12:
                if (energy > 8)
                {
                    Debug.Log("You Win!");
                    Debug.Log($"{(int)(energy - 7)} stars");
                }
                buttonManager.ShowNextLevelButton();
                break;
            default:
                break;
        }
    }
}
