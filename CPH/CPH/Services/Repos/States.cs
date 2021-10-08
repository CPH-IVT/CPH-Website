///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         States.cs
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
    /// Defines the <see cref="States" />.
    /// </summary>
    public class States : IStates
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="states">The states<see cref="Models.States"/>.</param>
        /// <returns>The <see cref="Models.States"/>.</returns>
        public Models.States Create(Models.States states)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="states">The states<see cref="Models.States"/>.</param>
        public void Delete(Models.States states)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Models.States"/>.</returns>
        public Models.States Read(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{Models.States}"/>.</returns>
        public ICollection<Models.States> ReadAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="states">The states<see cref="Models.States"/>.</param>
        /// <returns>The <see cref="Models.States"/>.</returns>
        public Models.States Update(Models.States states)
        {
            throw new NotImplementedException();
        }
    }
}
