///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         IStates.cs
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
    /// Defines the <see cref="IStates" />.
    /// </summary>
    public interface IStates
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="states">The states<see cref="States"/>.</param>
        /// <returns>The <see cref="States"/>.</returns>
        States Create(States states);

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="States"/>.</returns>
        States Read(int id);

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{States}"/>.</returns>
        ICollection<States> ReadAll();

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="states">The states<see cref="States"/>.</param>
        /// <returns>The <see cref="States"/>.</returns>
        States Update(States states);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="states">The states<see cref="States"/>.</param>
        void Delete(States states);
    }
}
