///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  SOLUTION NAME HERE
//	File Name:         SavedChartDatapoints.cs
//	Description:       YOUR DESCRIPTION HERE
//	Course:            CSCI 2210 - Data Structures	
//	Author:           DESKTOP-FOTV38D\Joshua, trimmj@etsu.edu
//	Created:           10/7/2021
//	Copyright:         DESKTOP-FOTV38D\Joshua, 2021
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace CPH.Models
{
    /// <summary>
    /// Defines the <see cref="SavedChartDatapoints" />.
    /// </summary>
    public class SavedChartDatapoints
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ChartId.
        /// </summary>
        public int ChartId { get; set; }

        /// <summary>
        /// Gets or sets the DatapointId.
        /// </summary>
        public int DatapointId { get; set; }
    }
}
