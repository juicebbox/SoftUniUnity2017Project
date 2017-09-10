using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private float xMax;
    [SerializeField]
    private float yMax;
    [SerializeField]
    private float xMin;
    [SerializeField]
    private float yMin;

    [SerializeField]
    private float yOffset;

    public Transform target;

    private bool isLocked;

    private GameMaster gameMaster;

    public bool IsLocked
    {
        get
        {
            return isLocked;
        }
    }

    void Start ()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
	}

    private void Update()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    void LateUpdate()
    {
        if (isLocked)
        {

        }
        else
        {
            transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y + yOffset, yMin, yMax), transform.position.z);
        }
    }
    public void LockCameraInRoom()
    {

    }

    public void UnlockCamera()
    {

    }
}
