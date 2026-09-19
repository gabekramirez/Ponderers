using UnityEngine;
using System.Collections.Generic;

public class ResourceAssets : MonoBehaviour
{
    public List<Sprite> doorPieces;
    public List<Sprite> wallPieces;
    public List<Sprite> windowPieces;
    public List<Sprite> roofPieces;


    [Header("House Assets")]
    public static List<Sprite> globalDoorPieces;
    public static List<Sprite> globalWallPieces;
    public static List<Sprite> globalWindowPieces;
    public static List<Sprite> globalRoofPieces;


    void Awake(){
        //Initialize if null
        if(ResourceAssets.globalDoorPieces == null){
            ResourceAssets.globalDoorPieces = doorPieces;
            ResourceAssets.globalWallPieces = wallPieces;
            ResourceAssets.globalWindowPieces = windowPieces;
            ResourceAssets.globalRoofPieces = roofPieces;
        }
        
    }

    public static Sprite GetLevelPiece(int sceneIdx){
        int idxInList = sceneIdx % 4;
        switch (idxInList -= (sceneIdx % 4)){
            case 0:
                return ResourceAssets.globalDoorPieces[idxInList];
            case 1:
                return ResourceAssets.globalWallPieces[idxInList];
            case 2:
                return ResourceAssets.globalWindowPieces[idxInList];
            case 3:
                return ResourceAssets.globalRoofPieces[idxInList];
        }
        return ResourceAssets.globalDoorPieces[idxInList];
    }
}

