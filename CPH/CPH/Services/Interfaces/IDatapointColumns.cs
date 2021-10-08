///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         IDatapointColumns.cs
//	Description:       YOUR DESCRIPTION HERE
//	Course:            Capstone
//	Author:            Joshua Trimm, trimmj@etsu.edu
//	Created:           10/7/2021
//	Copyright:         Joshua Trimm, 2021
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace CPH.Services.Interfaces
{
    using CPH.Models;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="IDatapointColumns" />.
    /// </summary>
    public interface IDatapointColumns
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="datapointColumns">The datapointColumns<see cref="DatapointColumns"/>.</param>
        /// <returns>The <see cref="DatapointColumns"/>.</returns>
        DatapointColumns Create(DatapointColumns datapointColumns);

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="DatapointColumns"/>.</returns>
        DatapointColumns Read(int id);

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{DatapointColumns}"/>.</returns>
        ICollection<DatapointColumns> ReadAll();

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="datapointColumns">The datapointColumns<see cref="DatapointColumns"/>.</param>
        /// <returns>The <see cref="DatapointColumns"/>.</returns>
        DatapointColumns Update(DatapointColumns datapointColumns);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="datapointColumns">The datapointColumns<see cref="DatapointColumns"/>.</param>
        void Delete(DatapointColumns datapointColumns);
    }
}
