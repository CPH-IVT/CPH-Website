///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         Counties.cs
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
    /// Defines the <see cref="Counties" />.
    /// </summary>
    public class Counties : ICounties
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="counties">The counties<see cref="Models.Counties"/>.</param>
        /// <returns>The <see cref="Models.Counties"/>.</returns>
        public Models.Counties Create(Models.Counties counties)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="counties">The counties<see cref="Models.Counties"/>.</param>
        public void Delete(Models.Counties counties)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Models.Counties"/>.</returns>
        public Models.Counties Read(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{Models.Counties}"/>.</returns>
        public ICollection<Models.Counties> ReadAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="counties">The counties<see cref="Models.Counties"/>.</param>
        /// <returns>The <see cref="Models.Counties"/>.</returns>
        public Models.Counties Update(Models.Counties counties)
        {
            throw new NotImplementedException();
        }
    }
}
