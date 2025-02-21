
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Model
{
    public class Minigame2048
    {
        public int SizeMap { get; set; }
        public int[,] Map { get; set; }

        public Minigame2048(int size)
        {
            SizeMap = size;
            Map = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Map[i, j] = 0;
                }
            }

            Gen22();
            Gen22();
        }

        // Sinh số mới vào ô trống ngẫu nhiên
        public void Gen22()
        {
            List<Vector2Int> emptyCells = new List<Vector2Int>();

            for (int i = 0; i < SizeMap; i++)
            {
                for (int j = 0; j < SizeMap; j++)
                {
                    if (Map[i, j] == 0)
                    {
                        emptyCells.Add(new Vector2Int(i, j));
                    }
                }
            }

            if (emptyCells.Count > 0)
            {
                Vector2Int cell = emptyCells[Random.Range(0, emptyCells.Count)];
                Map[cell.x, cell.y] = (Random.Range(0, 10) < 9) ? 2 : 4; // 90% là 2, 10% là 4
            }
        }

        // Dịch chuyển một hàng/cột về bên trái (chung cho mọi hướng)
        private int[] SlideLeft(int[] row)
        {
            List<int> result = row.Where(x => x != 0).ToList(); // Loại bỏ số 0
            while (result.Count < row.Length) result.Add(0);  // Bổ sung số 0 vào cuối
            return result.ToArray();
        }

        // Gộp các ô có giá trị giống nhau
        private int[] MergeLeft(int[] row)
        {
            row = SlideLeft(row);
            for (int i = 0; i < row.Length - 1; i++)
            {
                if (row[i] != 0 && row[i] == row[i + 1])
                {
                    row[i] *= 2;
                    row[i + 1] = 0;
                }
            }
            return SlideLeft(row);
        }

        // Xử lý di chuyển theo các hướng
        public void Move(Vector2 dir)
        {
            bool moved = false;

            if (dir == Vector2.left)
            {
                for (int i = 0; i < SizeMap; i++)
                {
                    int[] row = new int[SizeMap];
                    for (int j = 0; j < SizeMap; j++) row[j] = Map[i, j];

                    int[] newRow = MergeLeft(row);
                    if (!Enumerable.SequenceEqual(row, newRow)) moved = true;

                    for (int j = 0; j < SizeMap; j++) Map[i, j] = newRow[j];
                }
            }
            else if (dir == Vector2.right)
            {
                for (int i = 0; i < SizeMap; i++)
                {
                    int[] row = new int[SizeMap];
                    for (int j = 0; j < SizeMap; j++) row[j] = Map[i, SizeMap - 1 - j];

                    int[] newRow = MergeLeft(row);
                    if (!Enumerable.SequenceEqual(row, newRow)) moved = true;

                    for (int j = 0; j < SizeMap; j++) Map[i, SizeMap - 1 - j] = newRow[j];
                }
            }
            else if (dir == Vector2.up)
            {
                for (int j = 0; j < SizeMap; j++)
                {
                    int[] col = new int[SizeMap];
                    for (int i = 0; i < SizeMap; i++) col[i] = Map[i, j];

                    int[] newCol = MergeLeft(col);
                    if (!Enumerable.SequenceEqual(col, newCol)) moved = true;

                    for (int i = 0; i < SizeMap; i++) Map[i, j] = newCol[i];
                }
            }
            else if (dir == Vector2.down)
            {
                for (int j = 0; j < SizeMap; j++)
                {
                    int[] col = new int[SizeMap];
                    for (int i = 0; i < SizeMap; i++) col[i] = Map[SizeMap - 1 - i, j];

                    int[] newCol = MergeLeft(col);
                    if (!Enumerable.SequenceEqual(col, newCol)) moved = true;

                    for (int i = 0; i < SizeMap; i++) Map[SizeMap - 1 - i, j] = newCol[i];
                }
            }

            if (moved)
            {
                Gen22(); // Chỉ sinh số mới nếu có thay đổi trên bảng
            }
        }

        // Kiểm tra xem còn có thể đi tiếp không (thắng/thua)
        public bool CanMove()
        {
            for (int i = 0; i < SizeMap; i++)
            {
                for (int j = 0; j < SizeMap; j++)
                {
                    if (Map[i, j] == 0) return true; // Còn ô trống có thể di chuyển

                    // Kiểm tra gộp được ô không
                    if (j < SizeMap - 1 && Map[i, j] == Map[i, j + 1]) return true;
                    if (i < SizeMap - 1 && Map[i, j] == Map[i + 1, j]) return true;
                }
            }
            return false;
        }
    }
}

