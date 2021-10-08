///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         IKeywords.cs
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
    /// Defines the <see cref="IKeywords" />.
    /// </summary>
    public interface IKeywords
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="keyword">The keyword<see cref="Keywords"/>.</param>
        /// <returns>The <see cref="Keywords"/>.</returns>
        Keywords Create(Keywords keyword);

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Keywords"/>.</returns>
        Keywords Read(int id);

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{Keywords}"/>.</returns>
        ICollection<Keywords> ReadAll();

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="keyword">The keyword<see cref="Keywords"/>.</param>
        /// <returns>The <see cref="Keywords"/>.</returns>
        Keywords Update(Keywords keyword);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="keyword">The keyword<see cref="Keywords"/>.</param>
        void Delete(Keywords keyword);
    }
}
