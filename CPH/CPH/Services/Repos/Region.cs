///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         Region.cs
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
    /// Defines the <see cref="Region" />.
    /// </summary>
    public class Region : IRegion
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="region">The region<see cref="Models.Region"/>.</param>
        /// <returns>The <see cref="Models.Region"/>.</returns>
        public Models.Region Create(Models.Region region)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="region">The region<see cref="Models.Region"/>.</param>
        public void Delete(Models.Region region)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Models.Region"/>.</returns>
        public Models.Region Read(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{Models.Region}"/>.</returns>
        public ICollection<Models.Region> ReadAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="region">The region<see cref="Models.Region"/>.</param>
        /// <returns>The <see cref="Models.Region"/>.</returns>
        public Models.Region Update(Models.Region region)
        {
            throw new NotImplementedException();
        }
    }
}
