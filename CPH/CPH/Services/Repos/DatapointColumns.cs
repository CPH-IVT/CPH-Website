///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         DatapointColumns.cs
//	Description:       YOUR DESCRIPTION HERE
//	Course:            Capstone
//	Author:            Joshua Trimm, trimmj@etsu.edu
//	Created:           10/7/2021
//	Copyright:         Joshua Trimm, 2021
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace CPH.Services.Repos
{
    using CPH.Services.Interfaces;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="DatapointColumns" />.
    /// </summary>
    public class DatapointColumns : IDatapointColumns
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="datapointColumns">The datapointColumns<see cref="Models.DatapointColumns"/>.</param>
        /// <returns>The <see cref="Models.DatapointColumns"/>.</returns>
        public Models.DatapointColumns Create(Models.DatapointColumns datapointColumns)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="datapointColumns">The datapointColumns<see cref="Models.DatapointColumns"/>.</param>
        public void Delete(Models.DatapointColumns datapointColumns)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Models.DatapointColumns"/>.</returns>
        public Models.DatapointColumns Read(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{Models.DatapointColumns}"/>.</returns>
        public ICollection<Models.DatapointColumns> ReadAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="datapointColumns">The datapointColumns<see cref="Models.DatapointColumns"/>.</param>
        /// <returns>The <see cref="Models.DatapointColumns"/>.</returns>
        public Models.DatapointColumns Update(Models.DatapointColumns datapointColumns)
        {
            throw new NotImplementedException();
        }
    }
}
