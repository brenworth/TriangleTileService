using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TriangleTileService.Models;

namespace TriangleTileService.Controllers
{
    public class TileCoordinatesController : ApiController
    {

        /// <summary>
        /// Gets tile vertice points for selected tile location
        /// </summary>
        /// <param name="tileLocation"></param>
        /// <returns></returns>
        public List<Point> Get(string tileLocation)
        {
            //Basic parameter validation
            if (tileLocation == null || tileLocation.Length < 2)
                throw new ArgumentException("Illegal parameter execption for " + tileLocation);

            string row = tileLocation.Substring(0, 1).ToUpper();
            string columnString = tileLocation.Remove(0, 1);
            int columnValue = -1;

            //Validate row value
            if (!Helper.ROW_LABELS.Contains(row))
                throw new ArgumentException("Illegal parameter execption for row value " + row + ". Row must be in A-F");

            //Validate column number format
            if (!int.TryParse(columnString, out columnValue))
                throw new ArgumentException("Illegal parameter execption for column value " + columnString + ". Not an integer value");

            //Validate column value is in valid range
            if (columnValue > 12 || columnValue < 1)
                throw new ArgumentException("Illegal parameter execption for column value" + columnValue + ". Is not in the valid range of 1-12");

            //Use helper method to generate tile vertices
            return Helper.ComputeTileVertices(row, columnValue);

        }

        
    }
}
