///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         IRegionCounties.cs
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
    /// Defines the <see cref="IRegionCounties" />.
    /// </summary>
    public interface IRegionCounties
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="regionCounties">The regionCounties<see cref="RegionCounties"/>.</param>
        /// <returns>The <see cref="RegionCounties"/>.</returns>
        RegionCounties Create(RegionCounties regionCounties);

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="RegionCounties"/>.</returns>
        RegionCounties Read(int id);

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{RegionCounties}"/>.</returns>
        ICollection<RegionCounties> ReadAll();

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="regionCounties">The regionCounties<see cref="RegionCounties"/>.</param>
        /// <returns>The <see cref="RegionCounties"/>.</returns>
        RegionCounties Update(RegionCounties regionCounties);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="regionCounties">The regionCounties<see cref="RegionCounties"/>.</param>
        void Delete(RegionCounties regionCounties);
    }
}
