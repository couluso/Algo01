using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class MazeCell : MonoBehaviour
{
    [SerializeField]
    public GameObject _leftWall;

    [SerializeField]
    public GameObject _rightWall;

    [SerializeField]
    public GameObject _frontWall;

    [SerializeField]
    public GameObject _backWall;

    [SerializeField]
    private GameObject _unvisitedBlock;
   
    public GameObject _light;

    public bool IsVisited { get; private set; }

    public void Visit()
    {
        IsVisited = true;
        _unvisitedBlock.SetActive(false);
    }

    public void ClearLeftWall()
    {
        _leftWall.SetActive(false);
    }

    public void ClearRightWall()
    {
        _rightWall.SetActive(false);
    }

    public void ClearFrontWall()
    {
        _frontWall.SetActive(false);
    }

    public void ClearBackWall()
    {
        _backWall.SetActive(false);
    }
}
