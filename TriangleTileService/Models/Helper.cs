using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TriangleTileService.Models
{
    public static class Helper
    {
        /// <summary>
        /// Triangle X Side length in pixels
        /// </summary>
        public const int X_PIXEL_SIZE = 10;

        /// <summary>
        /// Triangle Y Side length in pixels
        /// </summary>
        public const int Y_PIXEL_SIZE = 10;

        /// <summary>
        /// Max number of columns
        /// </summary>
        public const int MAX_COLUMNS = 6;

        /// <summary>
        /// Row Labels
        /// </summary>
        public static readonly List<string> ROW_LABELS = new List<string>() { "A", "B", "C", "D", "E", "F" };

        public static string GetTileLocation(int xCoordinate, int yCoordinate)
        {
            StringBuilder result = new StringBuilder();

            //Get remainder to compute angle to see which triangle partition the the point is in
            int columnRemainder = xCoordinate % X_PIXEL_SIZE;
            int rowRemainder = yCoordinate % Y_PIXEL_SIZE;
           
            //Get column/row base
            int startColumn = (xCoordinate - columnRemainder) / X_PIXEL_SIZE;
            int startRow = (yCoordinate - rowRemainder) / Y_PIXEL_SIZE;
            
            //Compute column base value
            int columnValue = startColumn * 2 + 1;
            double angle = 0;

            //Set the Row Label
            result.Append(ROW_LABELS[startRow]);
            
            //Non-zero modulus get angle between remainders
            if(rowRemainder != 0)
            {
                angle = System.Math.Atan((double)rowRemainder / (double)columnRemainder);
            }
            
            //Adjust column value for the next index
            if(angle < System.Math.Atan(1))
            {
                columnValue++;
            }

            //Set the Column label
            result.Append(columnValue);

            return result.ToString();
        }

        public static Point GetMidPoint(List<Point> vertices)
        {
            if(vertices == null || vertices.Count == 0) {

            }
            else if (vertices.Count == 1)
            {
                return vertices[0];
            }

            int totalX = 0, totalY = 0;
            foreach (Point p in vertices)
            {
                totalX += p.X;
                totalY += p.Y;
            }

            int centerX = totalX / vertices.Count;
            int centerY = totalY / vertices.Count;

            return new Point() { X = centerX, Y = centerY };
        }

        public static List<Point> ComputeTileVertices(string row, int columnValue)
        {
            //This could be improved by using a Dictionary to represent the row labels
            int rowIndex = -1;
            for (int i = 0; i < ROW_LABELS.Count; i++)
            {
                if (row.Equals(ROW_LABELS[i]))
                {
                    rowIndex = i;
                    break;
                }
            }

            //Compute the vertice locations

            int v1x, v1y, v2x, v2y, v3x, v3y = 0;

            if (columnValue % 2 == 0)
            {
                v1x = (columnValue / 2 - 1) * X_PIXEL_SIZE;
                v1y = rowIndex * Y_PIXEL_SIZE;

                //V2
                v2x = v1x + X_PIXEL_SIZE - 1;
                v2y = v1y;
            }
            else
            {
                v1x = ((columnValue + 1) / 2 - 1) * X_PIXEL_SIZE;
                v1y = rowIndex * Y_PIXEL_SIZE;

                //V2
                v2x = v1x;
                v2y = v1y + Y_PIXEL_SIZE - 1;
            }

            v3x = v1x + X_PIXEL_SIZE - 1;
            v3y = v1y + Y_PIXEL_SIZE - 1;

            return new List<Point>() { new Point() { X = v1x, Y = v1y }, new Point() { X = v2x, Y = v2y }, new Point() { X = v3x, Y = v3y } };
        }
    }
}