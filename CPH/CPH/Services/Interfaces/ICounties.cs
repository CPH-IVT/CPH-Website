///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  SOLUTION NAME HERE
//	File Name:         ICounties.cs
//	Description:       YOUR DESCRIPTION HERE
//	Course:            CSCI 2210 - Data Structures	
//	Author:           DESKTOP-FOTV38D\Joshua, trimmj@etsu.edu
//	Created:           10/7/2021
//	Copyright:         DESKTOP-FOTV38D\Joshua, 2021
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace CPH.Services.Interfaces
{
    using CPH.Models;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ICounties" />.
    /// </summary>
    public interface ICounties
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="counties">The counties<see cref="Counties"/>.</param>
        /// <returns>The <see cref="Counties"/>.</returns>
        Counties Create(Counties counties);

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Counties"/>.</returns>
        Counties Read(int id);

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{Counties}"/>.</returns>
        ICollection<Counties> ReadAll();

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="counties">The counties<see cref="Counties"/>.</param>
        /// <returns>The <see cref="Counties"/>.</returns>
        Counties Update(Counties counties);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="counties">The counties<see cref="Counties"/>.</param>
        void Delete(Counties counties);
    }
}
