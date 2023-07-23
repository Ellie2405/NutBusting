using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public static FloorManager Instance;
    int selectedFloor = 0;
    [SerializeField] List<RingFloor> rings = new List<RingFloor>();
    [SerializeField] TurretAbstract turret;
    [SerializeField] LayerMask floorLayerMask;
    [SerializeField] LayerMask turretSlotLayerMask;

    [SerializeField] RingFloor floorToMove;
    TurretSlot turretSlotSelected;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        //Ring controls
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    selectedFloor--;
        //    if (selectedFloor < 0) selectedFloor = 0;
        //}
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    selectedFloor++;
        //    if (selectedFloor > rings.Count - 1) selectedFloor = rings.Count - 1;
        //}
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    rings[selectedFloor].Rotate(rotationDegrees);
        //}
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    rings[selectedFloor].Rotate(-rotationDegrees);
        //}

        //Turret Building

        if (Input.GetMouseButtonDown(1))
        {
            SelectSlot();
            OrderTurret(turret);
        }
        if (Input.GetMouseButtonDown(0))
        {
            SelectRing();
        }
        if (Input.GetMouseButton(0))
        {
            SelectRotation();
        }

        //if (Input.GetMouseButtonUp(0))
        //{
        //    SelectRotation();
        //}
    }

    void OrderTurret(TurretAbstract turretToBuild)
    {
        //check if the player has enough currency
        if (Inventory.Instance.CanAfford(turretToBuild.GetPrice()))
        {
            //build the turret if conditions are met
            if (rings[selectedFloor].BuildTurret(turretToBuild))
            {
                //pay currency
                Inventory.Instance.Pay(turretToBuild.GetPrice());
                //Debug.Log($"Turret built succesfully, {turretToBuild}, costed {turretToBuild.GetPrice()}");

            }
        }
        else Debug.Log("Not enough currency");

    }

    //select slot
    void SelectSlot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 100, turretSlotLayerMask))
        {
            turretSlotSelected = raycastHit.transform.GetComponent<TurretSlot>().SelectSlot();
        }

    }

    //store start
    void SelectRing()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 100, floorLayerMask))
        {
            //RingFloor rf = raycastHit.transform.GetComponent<RingFloor>();
            //rf.StoreA(raycastHit.point);
            floorToMove = raycastHit.transform.GetComponent<RingFloor>();
            floorToMove.StoreA(raycastHit.point);
        }

    }

    //store end
    void SelectRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 100, floorLayerMask))
        {
            //if (raycastHit.transform.GetComponent<RingFloor>() == floorToMove)
            floorToMove.StoreB(raycastHit.point);
        }

    }
}
