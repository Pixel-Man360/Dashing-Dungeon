using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Prefabs and Dependecies:")]
    [SerializeField] private GameObject _entryRoom;
    [SerializeField] private GameObject _exitRoom;
    [SerializeField] private List<GameObject> _otherRooms;

    [Header("Room Variables:")]

    [SerializeField] private int _totalRowsInGrid = 20;
    [SerializeField] private int _totalColumnsInGrid = 20;
    [Range(10,20)][SerializeField] private int _totalRooms;

    private int _gridRowSize;
    private int _gridColumnSize;
    private List<GameObject> _roomsList = new List<GameObject>();
    private List<GameObject> _roomsWithEmptyAdjacents = new List<GameObject>();
    private bool[,] _gridForRooms;

    void Awake()
    {
        _gridRowSize = _totalRowsInGrid * 12;
        _gridColumnSize = _totalColumnsInGrid * 26;
        _gridForRooms = new bool[_gridColumnSize + 1, _gridRowSize + 1];
        _totalRooms = Mathf.Clamp(_totalRooms, 0, (_totalRowsInGrid * _totalColumnsInGrid));
        
    }

    void Start()
    {
        GenerateRooms();
        GenerateSideWalls();
    }


    void GenerateRooms()
    {
      Transform entryRoom = AddEntryRoom();

      CameraFollowRooms.instance.SetCameraFollowTarget(entryRoom);

       for(int i = 1; i < _totalRooms; i++)
       {
           bool newRoomPlaced = false;

           while(!newRoomPlaced)
           {
                GameObject previousRoomPos = _roomsWithEmptyAdjacents[Random.Range(0, _roomsWithEmptyAdjacents.Count)];
                List<Vector2> adjacentCells = GetAdjacentGridCells(previousRoomPos.transform.position);

                if(adjacentCells.Count != 0)
                {
                    if(i != _totalRooms - 1)
                    {
                        AddNewRoom(_otherRooms[Random.Range(0, _otherRooms.Count)], adjacentCells[Random.Range(0, adjacentCells.Count)], $"Room {i}");
                    }

                    else 
                    {
                        AddNewRoom(_exitRoom, adjacentCells[Random.Range(0, adjacentCells.Count)], "Exit Room");
                    }

                    newRoomPlaced = true;
                }

                else
                {
                    _roomsWithEmptyAdjacents.Remove(previousRoomPos);
                }
            }
        }
    }

    void GenerateSideWalls()
    {
        foreach (GameObject room in _roomsList)
        {
            Debug.Log(room.name);
            Vector2 left = new Vector2(room.transform.position.x - 26, room.transform.position.y);
            Vector2 right = new Vector2(room.transform.position.x + 26, room.transform.position.y);
            Vector2 top = new Vector2(room.transform.position.x, room.transform.position.y + 12);
            Vector2 bottom = new Vector2(room.transform.position.x, room.transform.position.y - 12);

            if(left.x >= 0)
                if(_gridForRooms[(int)left.x, (int)left.y] == false)
                    room.transform.GetChild(1).gameObject.SetActive(true);
            
            if(right.x <= _gridColumnSize - 26)
                if(_gridForRooms[(int)right.x, (int)right.y] == false)
                    room.transform.GetChild(2).gameObject.SetActive(true);

            if(top.y <= _gridRowSize - 12)
                if(_gridForRooms[(int)top.x, (int)top.y] == false)
                    room.transform.GetChild(3).gameObject.SetActive(true);
            
            if(bottom.y >= 0 )
                if(_gridForRooms[(int)bottom.x, (int)bottom.y] == false)
                    room.transform.GetChild(4).gameObject.SetActive(true);

        }

        Debug.Log(_roomsList[0]);
    }

    Transform AddEntryRoom()
    {
        float xPos = Random.Range(0, _gridColumnSize - 26);
        float yPos = Random.Range(0, _gridRowSize - 12);

        xPos = xPos - (xPos % 26);
        yPos = yPos - (yPos % 12);

        return AddNewRoom(_entryRoom, new Vector2(xPos, yPos), "Entry Room");
    }



    Transform AddNewRoom(GameObject prefab, Vector2 position, string tag)
    {
        GameObject newRoom = Instantiate(prefab, position, Quaternion.identity);
        newRoom.tag = tag;
        _roomsList.Add(newRoom);
        _roomsWithEmptyAdjacents.Add(newRoom);
        _gridForRooms[(int)newRoom.transform.position.x, (int)newRoom.transform.position.y] = true;
        return newRoom.transform;
    }

    List<Vector2> GetAdjacentGridCells(Vector2 previousRoomPos)
    {
        List<Vector2> cells = new List<Vector2>();

        Vector2 top = new Vector2(previousRoomPos.x, previousRoomPos.y + 12);
        Vector2 bottom = new Vector2(previousRoomPos.x, previousRoomPos.y - 12);
        Vector2 left = new Vector2(previousRoomPos.x - 26, previousRoomPos.y);
        Vector2 right = new Vector2(previousRoomPos.x + 26, previousRoomPos.y);

        if(top.y <= _gridRowSize - 12)
            if(_gridForRooms[(int)top.x, (int)top.y] == false)
                cells.Add(top);
        
        if(bottom.y >= 0 )
            if(_gridForRooms[(int)bottom.x, (int)bottom.y] == false)
                cells.Add(bottom);

        if(left.x >= 0)
            if(_gridForRooms[(int)left.x, (int)left.y] == false)
                cells.Add(left);
        
        if(right.x <= _gridColumnSize - 26)
            if(_gridForRooms[(int)right.x, (int)right.y] == false)
                cells.Add(right);

        return cells;
    }

    

    


    

    
    
}
