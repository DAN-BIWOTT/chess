﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

	private const float TILE_SIZE = 1.0f;
	private const float TILE_OFFSET_Y = 0.6f;
    private const float TILE_OFFSET_X = 0.15f;

	private int selectionX = -1;
	private int selectionY = -1;

	public List<GameObject> chessmanPrefabs;
    private List<GameObject> activeChessman;

    private void Start()
    {
        SpawnAllChessMans();
    }

	private void Update(){
		UpdateSelection ();
		DrawChessBoard ();
	}

	private void UpdateSelection(){
	
		if (!Camera.main)
			return;

		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 25.0f, LayerMask.GetMask ("ChessPlane"))) {

			selectionX = (int)hit.point.x;
			selectionY = (int)hit.point.z;
		} else {
			selectionX = -1;
			selectionY = -1;

		}
	
	}

    private void SpawnChessman(int index, Vector3 position)
    {
        GameObject go = Instantiate(chessmanPrefabs[index],position,Quaternion.identity) as GameObject;
        go.transform.SetParent(transform);
        activeChessman.Add(go);
    }

    private void SpawnAllChessMans()
    {
        activeChessman = new List<GameObject>();

        //Spawn the white team!

        //king
        SpawnChessman(0, GetTileCenter(3, 0));
        //Queen
        SpawnChessman(1, GetTileCenter(1, 0));
        //rooks
        SpawnChessman(2, GetTileCenter(2, 0));
        SpawnChessman(2, GetTileCenter(-5, 0));
        //bishops
        SpawnChessman(3, GetTileCenter(2, 0));
        SpawnChessman(3, GetTileCenter(5, 0));
        //knights
        SpawnChessman(4, GetTileCenter(1, 0));
        SpawnChessman(4, GetTileCenter(6, 0));
        //pawn
        for (int i = 0;i < 8; i++)
        {
            SpawnChessman(5, GetTileCenter(i, 0));
        }

    }

    private Vector3 GetTileCenter(int x,int y)   
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET_X;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET_Y;
       
        return origin;
    }

	private void DrawChessBoard(){
		Vector3 widthLine = Vector3.right * 8;
		Vector3 heightLine = Vector3.forward * 8;

		for (int i = 0; i <= 8; i++) {
			Vector3 start = Vector3.forward * i;
			Debug.DrawLine (start,start + widthLine);
			for (int j = 0; j <= 8; j++) {
				start = Vector3.right * j;
				Debug.DrawLine (start, start + heightLine);
			}
		}

		//draw the selection
		if(selectionX >= 0 && selectionY >= 0){
			Debug.DrawLine (
				Vector3.forward * selectionY + Vector3.right * selectionX,
				Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1)
			);
			Debug.DrawLine (
				Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
				Vector3.forward * selectionY + Vector3.right * (selectionX + 1)
			);
		}
	}
}
