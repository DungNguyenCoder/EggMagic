using System.Collections.Generic;
using UnityEngine;

public class BFS
{
    public static List<(int row, int col, int dist)> LoangBFS(int sx, int sy, int _height, int _width, int[,] _eggTilesID)
    {
        List<(int, int, int)> result = new List<(int, int, int)>();
        bool[,] _visited = new bool[_height, _width]; ;

        int[] dx = { 0, -1, 0, 1 };
        int[] dy = { -1, 0, 1, 0 };

        Queue<(int, int, int)> q = new Queue<(int, int, int)>();

        q.Enqueue((sx, sy, 0));
        _visited[sx, sy] = true;

        while (q.Count > 0)
        {
            var (x, y, d) = q.Dequeue();
            result.Add((x, y, d));

            for (int k = 0; k < 4; k++)
            {
                int nx = x + dx[k];
                int ny = y + dy[k];

                if (nx >= 0 && nx < _height && ny >= 0 && ny < _width)
                {
                    if (!_visited[nx, ny] && _eggTilesID[nx, ny] == _eggTilesID[x, y])
                    {
                        _visited[nx, ny] = true;
                        q.Enqueue((nx, ny, d + 1));
                    }
                }
            }
        }
        result.Reverse();
        return result;
    }
    public static Vector2Int FindParentTowardTarget(int r, int c, int dist, Dictionary<(int,int), int> distMap, int _height, int _width)
    {
        int[] dr = { 0, -1, 0, 1 };
        int[] dc = { -1, 0, 1, 0 };
        for (int k = 0; k < 4; k++)
        {
            int nr = r + dr[k], nc = c + dc[k];
            if (nr < 0 || nr >= _height || nc < 0 || nc >= _width)
                continue;
            if (distMap.TryGetValue((nr, nc), out int nd) && nd == dist - 1)
                return new Vector2Int(nr, nc);
        }
        return new Vector2Int(r, c);
    }
}

