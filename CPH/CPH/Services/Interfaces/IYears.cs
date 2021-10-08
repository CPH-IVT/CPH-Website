///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         IYears.cs
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
    /// Defines the <see cref="IYears" />.
    /// </summary>
    public interface IYears
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="years">The years<see cref="Years"/>.</param>
        /// <returns>The <see cref="Years"/>.</returns>
        Years Create(Years years);

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Years"/>.</returns>
        Years Read(int id);

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{Years}"/>.</returns>
        ICollection<Years> ReadAll();

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="years">The years<see cref="Years"/>.</param>
        /// <returns>The <see cref="Years"/>.</returns>
        Years Update(Years years);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="years">The years<see cref="Years"/>.</param>
        void Delete(Years years);
    }
}
