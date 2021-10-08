///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         RegionCounties.cs
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
    /// Defines the <see cref="RegionCounties" />.
    /// </summary>
    public class RegionCounties : IRegionCounties
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="regionCounties">The regionCounties<see cref="Models.RegionCounties"/>.</param>
        /// <returns>The <see cref="Models.RegionCounties"/>.</returns>
        public Models.RegionCounties Create(Models.RegionCounties regionCounties)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="regionCounties">The regionCounties<see cref="Models.RegionCounties"/>.</param>
        public void Delete(Models.RegionCounties regionCounties)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Models.RegionCounties"/>.</returns>
        public Models.RegionCounties Read(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{Models.RegionCounties}"/>.</returns>
        public ICollection<Models.RegionCounties> ReadAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="regionCounties">The regionCounties<see cref="Models.RegionCounties"/>.</param>
        /// <returns>The <see cref="Models.RegionCounties"/>.</returns>
        public Models.RegionCounties Update(Models.RegionCounties regionCounties)
        {
            throw new NotImplementedException();
        }
    }
}
