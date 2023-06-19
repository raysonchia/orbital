using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualiser tilemapVisualiser)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.eightWayDirectionsList);
        var backgroundPositions = FindDungeonBackgrondPositions(floorPositions);

        foreach (var position in basicWallPositions)
        {
            tilemapVisualiser.PaintSingleBasicWall(position);
        }

        foreach (var position in backgroundPositions)
        {
            tilemapVisualiser.PaintSingleBackgroundTile(position);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> positions, List<Vector2Int> directions)
    {

        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();

        foreach (var position in positions)
        {
            foreach (var direction in directions)
            {
                var neighbourPosition = position + direction;
                if (positions.Contains(neighbourPosition) == false)
                {
                    wallPositions.Add(neighbourPosition);
                }
            }
        }

        return wallPositions;
    }

    private static List<Vector2Int> FindDungeonBackgrondPositions(HashSet<Vector2Int> positions)
    {
        int thickness = 7;

        List<Vector2Int> dungeonPositions = new List<Vector2Int>();
        BoundsInt mapSize = new BoundsInt();

        foreach (var position in positions)
        {
            if (position.x > mapSize.xMax)
            {
                mapSize.xMax = position.x;
            }
            else if (position.x < mapSize.xMin)
            {
                mapSize.xMin = position.x;
            }

            if (position.y > mapSize.yMax)
            {
                mapSize.yMax = position.y;
            }
            else if (position.y < mapSize.yMin)
            {
                mapSize.yMin = position.y;
            }
        }

        for (int x = mapSize.xMin - thickness; x <= mapSize.xMax + thickness; x++)
        {
            for (int y = mapSize.yMin - thickness; y <= mapSize.yMax + thickness; y++)
            {
                dungeonPositions.Add(new Vector2Int(x,y));
            }
        }

        return dungeonPositions;
    }
}
