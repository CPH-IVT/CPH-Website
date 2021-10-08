///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         SavedCharts.cs
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
    /// Defines the <see cref="SavedCharts" />.
    /// </summary>
    public class SavedCharts : ISavedCharts
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="savedCharts">The savedCharts<see cref="Models.SavedCharts"/>.</param>
        /// <returns>The <see cref="Models.SavedCharts"/>.</returns>
        public Models.SavedCharts Create(Models.SavedCharts savedCharts)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="savedCharts">The savedCharts<see cref="Models.SavedCharts"/>.</param>
        public void Delete(Models.SavedCharts savedCharts)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Models.SavedCharts"/>.</returns>
        public Models.SavedCharts Read(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{Models.SavedCharts}"/>.</returns>
        public ICollection<Models.SavedCharts> ReadAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="savedCharts">The savedCharts<see cref="Models.SavedCharts"/>.</param>
        /// <returns>The <see cref="Models.SavedCharts"/>.</returns>
        public Models.SavedCharts Update(Models.SavedCharts savedCharts)
        {
            throw new NotImplementedException();
        }
    }
}
