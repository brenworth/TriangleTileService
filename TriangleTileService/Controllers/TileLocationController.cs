using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TriangleTileService.Models;

namespace TriangleTileService.Controllers
{
    public class TileLocationController : ApiController
    {
        

        /// <summary>
        /// Compute the tile location based on point in 0-60 X dimension and 0-60 Y dimension area
        /// </summary>
        /// <param name="xCoordinate"></param>
        /// <param name="yCoordinate"></param>
        /// <returns></returns>
        public string Get(int xCoordinate, int yCoordinate)
        {
            //Verify that coordinates within valid range
            if(yCoordinate > Helper.ROW_LABELS.Count * Helper.Y_PIXEL_SIZE)
            {
                throw new ArgumentException("yCoordinate value " + yCoordinate + " greater than " + Helper.ROW_LABELS.Count * Helper.Y_PIXEL_SIZE);
            }
            else if(yCoordinate < 0)
            {
                throw new ArgumentException("yCoordinate value " + yCoordinate + " must be equal and greater than 0");
            }

            if (xCoordinate > Helper.MAX_COLUMNS * Helper.X_PIXEL_SIZE)
            {
                throw new ArgumentException("xCoordinate value " + xCoordinate + " greater than " + Helper.MAX_COLUMNS * Helper.X_PIXEL_SIZE);
            }
            else if (xCoordinate < 0)
            {
                throw new ArgumentException("xCoordinate value " + xCoordinate + " must be equal and greater than 0");
            }

            string result = Helper.GetTileLocation(xCoordinate, yCoordinate);
            
            return result;
        }

        // GET: api/TileLocation/5
        /// <summary>
        /// Get tile location based on 3 Points defining triangle region, in 0-60 X dimension and 0-60 Y dimension area
        /// </summary>
        /// <param name="vt1x"></param>
        /// <param name="vt1y"></param>
        /// <param name="vt2x"></param>
        /// <param name="vt2y"></param>
        /// <param name="vt3x"></param>
        /// <param name="vt3y"></param>
        /// <returns></returns>
        public string Get(int vt1x, int vt1y, int vt2x, int vt2y, int vt3x, int vt3y)
        {
            //Create vertices for finding mid point
             Point vertex1 = new Point() { X = vt1x, Y = vt1y };
             Point vertex2 = new Point() { X = vt2x, Y = vt2y };
             Point vertex3 = new Point() { X = vt3x, Y = vt3y };

             //Get mid point of triangle to find triangle tile region
             Point midPoint = Helper.GetMidPoint(new List<Point>() { vertex1, vertex2, vertex3 });

             return Get(midPoint.X, midPoint.Y);

        }

    }
}
