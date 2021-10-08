///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         Years.cs
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
    /// Defines the <see cref="Years" />.
    /// </summary>
    public class Years : IYears
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="years">The years<see cref="Models.Years"/>.</param>
        /// <returns>The <see cref="Models.Years"/>.</returns>
        public Models.Years Create(Models.Years years)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="years">The years<see cref="Models.Years"/>.</param>
        public void Delete(Models.Years years)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Models.Years"/>.</returns>
        public Models.Years Read(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{Models.Years}"/>.</returns>
        public ICollection<Models.Years> ReadAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="years">The years<see cref="Models.Years"/>.</param>
        /// <returns>The <see cref="Models.Years"/>.</returns>
        public Models.Years Update(Models.Years years)
        {
            throw new NotImplementedException();
        }
    }
}
