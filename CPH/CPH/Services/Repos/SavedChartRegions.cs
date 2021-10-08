///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         SavedChartRegions.cs
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
    /// Defines the <see cref="SavedChartRegions" />.
    /// </summary>
    public class SavedChartRegions : ISavedChartRegions
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="savedChartRegions">The savedChartRegions<see cref="Models.SavedChartRegions"/>.</param>
        /// <returns>The <see cref="Models.SavedChartRegions"/>.</returns>
        public Models.SavedChartRegions Create(Models.SavedChartRegions savedChartRegions)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="savedChartRegions">The savedChartRegions<see cref="Models.SavedChartRegions"/>.</param>
        public void Delete(Models.SavedChartRegions savedChartRegions)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Models.SavedChartRegions"/>.</returns>
        public Models.SavedChartRegions Read(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{Models.SavedChartRegions}"/>.</returns>
        public ICollection<Models.SavedChartRegions> ReadAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="savedChartRegions">The savedChartRegions<see cref="Models.SavedChartRegions"/>.</param>
        /// <returns>The <see cref="Models.SavedChartRegions"/>.</returns>
        public Models.SavedChartRegions Update(Models.SavedChartRegions savedChartRegions)
        {
            throw new NotImplementedException();
        }
    }
}
