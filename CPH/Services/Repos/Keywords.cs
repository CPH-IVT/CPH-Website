///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         Keywords.cs
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
    /// Defines the <see cref="Keywords" />.
    /// </summary>
    public class Keywords : IKeywords
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="keyword">The keyword<see cref="Models.Keywords"/>.</param>
        /// <returns>The <see cref="Models.Keywords"/>.</returns>
        public Models.Keywords Create(Models.Keywords keyword)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="keyword">The keyword<see cref="Models.Keywords"/>.</param>
        public void Delete(Models.Keywords keyword)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Models.Keywords"/>.</returns>
        public Models.Keywords Read(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{Models.Keywords}"/>.</returns>
        public ICollection<Models.Keywords> ReadAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="keyword">The keyword<see cref="Models.Keywords"/>.</param>
        /// <returns>The <see cref="Models.Keywords"/>.</returns>
        public Models.Keywords Update(Models.Keywords keyword)
        {
            throw new NotImplementedException();
        }
    }
}
