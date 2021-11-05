///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         IRegion.cs
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
    /// Defines the <see cref="IRegion" />.
    /// </summary>
    public interface IRegion
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="region">The region<see cref="Region"/>.</param>
        /// <returns>The <see cref="Region"/>.</returns>
        Region Create(Region region);

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Region"/>.</returns>
        Region Read(int id);

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{Region}"/>.</returns>
        ICollection<Region> ReadAll();

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="region">The region<see cref="Region"/>.</param>
        /// <returns>The <see cref="Region"/>.</returns>
        Region Update(Region region);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="region">The region<see cref="Region"/>.</param>
        void Delete(Region region);
    }
}
